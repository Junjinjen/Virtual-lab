using SharpDX;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JUnity.Services.Graphics.Meshing;
using JUnity.Services.Graphics.Lightning;
using JUnity.Services.Graphics.Utilities;
using JUnity.Services.Graphics.UI;

namespace JUnity.Services.Graphics
{
    internal sealed class GraphicsRenderer : IDisposable
    {
        private const int LightContainerSlot = 0;
        private const int MaterialDescriptionSlot = 1;
        private const int MeshMatricesSlot = 2;

        private Device _device;
        private SwapChainDescription _swapChainDescription;
        private SwapChain _swapChain;
        private Texture2D _depthBuffer;
        private DepthStencilView _depthView;
        private RenderTargetView _renderView;
        private RasterizerStateFactory _rasterizerStateFactory;

        private ConstantBuffer<LightContainer> _lightBuffer;
        private ConstantBuffer<MaterialDescription> _materialDescriptionBuffer;
        private ConstantBuffer<MeshMatrices> _meshMatricesBufferBuffer;

        private SampleDescription _sampleDescription;
        private Texture2DDescription _depthBufferDescription;
        private BlendStateDescription _blendStateDescription;
        private int _syncInterval;

        private readonly List<RenderOrder> _drawingQueue = new List<RenderOrder>();

        internal bool Minimized { get; set; }

        internal Device Device { get => _device; }

        internal UIRenderer UIRenderer { get; private set; }

        internal Texture2D BackBuffer { get; private set; }

        internal RenderForm RenderForm { get; private set; }

        internal LightManager LightManager { get; private set; }

        internal Camera Camera { get; private set; }

        public Color4 BackgroundColor { get; set; }

        public ReadOnlyDictionary<string, VertexShader> VertexShaders { get; private set; }

        public ReadOnlyDictionary<string, PixelShader> PixelShaders { get; private set; }

        public void Initialize(GraphicsSettings graphicsSettings)
        {
            CreateSharedFields(graphicsSettings);

            RenderForm = new RenderForm(graphicsSettings.WindowTitle);
            RenderForm.Resize += OnResize;
            UIRenderer = new UIRenderer();

            GraphicsInitializer.CreateDeviceWithSwapChain(graphicsSettings, RenderForm, _sampleDescription, out _swapChainDescription, out _swapChain, out _device);

            var factory = _swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(RenderForm.Handle, WindowAssociationFlags.IgnoreAll);
            factory.Dispose();

            CreateConstantBuffers();

            _rasterizerStateFactory = new RasterizerStateFactory(_device);

            GraphicsInitializer.InitializeShaders(graphicsSettings.ShadersMetaPath, out var inputSignature, out var vertexShaders, out var pixelShaders);
            VertexShaders = new ReadOnlyDictionary<string, VertexShader>(vertexShaders);
            PixelShaders = new ReadOnlyDictionary<string, PixelShader>(pixelShaders);

            var layout = new InputLayout(_device, inputSignature, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("NORMAL", 0, Format.R32G32B32A32_Float, 16, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 32, 0),
                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 48, 0),
                    });

            _device.ImmediateContext.InputAssembler.InputLayout = layout;

            var samplerState = GraphicsInitializer.CreateSamplerState(graphicsSettings.TextureSampling);
            _device.ImmediateContext.PixelShader.SetSampler(0, samplerState);

            OnResize(null, EventArgs.Empty);
            UIRenderer.Initialize(RenderForm);
        }

        public void AddRenderOrder(RenderOrder order)
        {
            _drawingQueue.Add(order);
        }

        public void RenderScene()
        {
            if (!Minimized)
            {
                ClearBuffers();
                LightManager.CameraPosition = Camera.Position;

                UpdateLightning();
                var viewProjectionMatrix = Matrix.Multiply(Camera.GetViewMatrixTema(), Camera.GetPojectionMatrix());

                for (int i = 0; i < _drawingQueue.Count; i++)
                {
                    if (_drawingQueue[i].Mesh.Material != null)
                    {
                        UpdateMaterial(_drawingQueue[i].Mesh.Material);
                    }

                    UpdateMeshMatrices(ref viewProjectionMatrix, _drawingQueue[i].GameObject, _drawingQueue[i].Mesh.Scale);

                    _device.ImmediateContext.Rasterizer.State = _rasterizerStateFactory.Create(_drawingQueue[i].Mesh.Material.RasterizerState);

                    _device.ImmediateContext.InputAssembler.PrimitiveTopology = _drawingQueue[i].Mesh.PrimitiveTopology;
                    _device.ImmediateContext.InputAssembler.SetVertexBuffers(0, _drawingQueue[i].Mesh.VertexBufferBinding);
                    _device.ImmediateContext.InputAssembler.SetIndexBuffer(_drawingQueue[i].Mesh.IndexBuffer, Format.R32_UInt, 0);

                    _device.ImmediateContext.VertexShader.Set(_drawingQueue[i].VertexShader);
                    _device.ImmediateContext.PixelShader.Set(_drawingQueue[i].PixelShader);

                    _device.ImmediateContext.DrawIndexed(_drawingQueue[i].Mesh.IndicesCount, 0, 0);
                }

                UIRenderer.RenderUI();
                EndRender();
            }
        }

        private void UpdateMeshMatrices(ref Matrix viewProjectionMatrix, GameObject gameObject, Vector3 meshScale)
        {
            var worldMatrix = Matrix.Scaling(gameObject.Scale * meshScale) * Matrix.RotationQuaternion(gameObject.Rotation) * Matrix.Translation(gameObject.Position);

            var matrices = new MeshMatrices
            {
                WorldMatrix = worldMatrix,
                WorldViewProjectionMatrix = Matrix.Multiply(worldMatrix, viewProjectionMatrix),
                InverseWorldMatrix = Matrix.Invert(worldMatrix),
            };

            matrices.WorldMatrix.Transpose();
            matrices.WorldViewProjectionMatrix.Transpose();

            _meshMatricesBufferBuffer.Update(matrices);
            _device.ImmediateContext.VertexShader.SetConstantBuffer(MeshMatricesSlot, _meshMatricesBufferBuffer.Buffer);
        }

        private void UpdateMaterial(Material material)
        {
            _materialDescriptionBuffer.Update(material.Description);
            _device.ImmediateContext.PixelShader.SetConstantBuffer(MaterialDescriptionSlot, _materialDescriptionBuffer.Buffer);

            if (material.Texture != null)
            {
                _device.ImmediateContext.PixelShader.SetShaderResource(0, material.Texture.ShaderResourceView);
            }
        }

        private void UpdateLightning()
        {
            _lightBuffer.Update(LightManager.GetContainer());
            _device.ImmediateContext.PixelShader.SetConstantBuffer(LightContainerSlot, _lightBuffer.Buffer); // may cause problems, when using multiple shaders
        }

        private void CreateSharedFields(GraphicsSettings graphicsSettings)
        {
            BackgroundColor = graphicsSettings.BackgroundColor;
            Camera = new Camera();
            LightManager = new LightManager
            {
                GlobalAmbient = graphicsSettings.GlobalAmbientOcclusion
            };

            GraphicsInitializer.CreateSampleAndDepthBufferDescriptions(graphicsSettings, out _sampleDescription, out _depthBufferDescription);

            _blendStateDescription = GraphicsInitializer.CreateBlendStateDescription();

            _syncInterval = graphicsSettings.VSyncEnabled ? 1 : 0;
        }

        private void CreateConstantBuffers()
        {
            _lightBuffer = new ConstantBuffer<LightContainer>(_device);
            _materialDescriptionBuffer = new ConstantBuffer<MaterialDescription>(_device);
            _meshMatricesBufferBuffer = new ConstantBuffer<MeshMatrices>(_device);
        }

        private void OnResize(object sender, EventArgs args)
        {
            if (RenderForm.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                Minimized = true;
                return;
            }
            else
            {
                Minimized = false;
                
                _device.ImmediateContext.Flush();
                RenderForm.Invalidate();
                RenderForm.Update();
            }

            BackBuffer?.Dispose();
            _renderView?.Dispose();
            _depthBuffer?.Dispose();
            _depthView?.Dispose();
            UIRenderer.RenderTarget?.Dispose();

            _swapChain.ResizeBuffers(_swapChainDescription.BufferCount, RenderForm.ClientSize.Width, RenderForm.ClientSize.Height, Format.Unknown, SwapChainFlags.None);
            BackBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);
            _renderView = new RenderTargetView(_device, BackBuffer);

            _depthBufferDescription.Width = RenderForm.ClientSize.Width;
            _depthBufferDescription.Height = RenderForm.ClientSize.Height;
            _depthBuffer = new Texture2D(_device, _depthBufferDescription);

            _depthView = new DepthStencilView(_device, _depthBuffer);
            var blendState = new BlendState(_device, _blendStateDescription);

            _device.ImmediateContext.Rasterizer.SetViewport(new Viewport(0, 0, RenderForm.ClientSize.Width, RenderForm.ClientSize.Height, 0.0f, 1.0f));
            _device.ImmediateContext.OutputMerger.SetTargets(_depthView, _renderView);
            _device.ImmediateContext.OutputMerger.SetBlendState(blendState);

            Camera.UpdateAspectRatio();
        }

        private void ClearBuffers()
        {
            _device.ImmediateContext.ClearDepthStencilView(_depthView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1f, 0);
            _device.ImmediateContext.ClearRenderTargetView(_renderView, BackgroundColor);
        }

        private void EndRender()
        {
            _swapChain.Present(_syncInterval, PresentFlags.Restart);
            LightManager.ResetLight();
            _drawingQueue.Clear();
        }

        public void Dispose()
        {
            _lightBuffer.Dispose();
            _meshMatricesBufferBuffer.Dispose();
            _materialDescriptionBuffer.Dispose();

            Helper.DisposeDictionaryElements(PixelShaders);
            Helper.DisposeDictionaryElements(VertexShaders);

            _depthBuffer.Dispose();
            _depthView.Dispose();
            _renderView.Dispose();
            BackBuffer.Dispose();
            _swapChain.Dispose();
            _device.Dispose();

            RenderForm.Dispose();
        }
    }
}
