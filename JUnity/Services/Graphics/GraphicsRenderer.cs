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

namespace JUnity.Services.Graphics
{
    public sealed class GraphicsRenderer : IDisposable
    {
        private const int LightContainerSlot = 0;
        private const int MaterialDescriptionSlot = 1;
        private const int MeshMatricesSlot = 2;

        private Device _device;
        private SwapChainDescription _swapChainDescription;
        private SwapChain _swapChain;
        private Texture2D _depthBuffer;
        private DepthStencilView _depthView;
        private Texture2D _backBuffer;
        private RenderTargetView _renderView;
        private SamplerState _samplerState;

        private ConstantBuffer<LightContainer> _lightBuffer;
        private ConstantBuffer<MaterialDescription> _materialDescriptionBuffer;
        private ConstantBuffer<MeshMatrices> _meshMatricesBufferBuffer;

        private SampleDescription _sampleDescription;
        private Texture2DDescription _depthBufferDescription;
        private int _syncInterval;

        private readonly List<RenderOrder> _drawingQueue = new List<RenderOrder>();

        internal Device Device { get => _device; private set => _device = value; }

        internal RenderForm RenderForm { get; private set; }

        internal LightManager LightManager { get; private set; }

        internal Camera Camera { get; private set; }

        public Color BackgroundColor { get; set; }

        public ReadOnlyDictionary<string, VertexShader> VertexShaders { get; private set; }

        public ReadOnlyDictionary<string, PixelShader> PixelShaders { get; private set; }

        internal void AddMeshToDrawingQueue(RenderOrder order)
        {
            _drawingQueue.Add(order);
        }

        internal void RenderScene()
        {
            ClearBuffers();
            LightManager.CameraPosition = Camera.Position;

            UpdateLightning();
            var viewProjectionMatrix = Matrix.Multiply(Camera.GetViewMatrixTema(), Camera.GetPojectionMatrix());

            foreach (var order in _drawingQueue)
            {
                UpdateMaterial(order.Mesh.Material);

                var worldMatrix = Matrix.RotationQuaternion(order.GameObject.Rotation) *
                    Matrix.Translation(order.GameObject.Position);

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

                _device.ImmediateContext.InputAssembler.PrimitiveTopology = order.Mesh.PrimitiveTopology;
                _device.ImmediateContext.InputAssembler.SetVertexBuffers(0, order.Mesh.VertexBufferBinding);
                _device.ImmediateContext.InputAssembler.SetIndexBuffer(order.Mesh.IndexBuffer, Format.R32_UInt, 0);

                _device.ImmediateContext.VertexShader.Set(order.Mesh.Material.VertexShader);
                _device.ImmediateContext.PixelShader.Set(order.Mesh.Material.PixelShader);
            }

            EndRender();
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

        internal void Initialize(GraphicsSettings graphicsSettings)
        {
            CreateSharedFields(graphicsSettings);

            RenderForm = new RenderForm(graphicsSettings.WindowTitle);
            RenderForm.UserResized += OnResize;

            GraphicsInitializer.CreateDeviceWithSwapChain(graphicsSettings, RenderForm, _sampleDescription, out _swapChainDescription, out _swapChain, out _device);

            var factory = _swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(RenderForm.Handle, WindowAssociationFlags.IgnoreAll);
            factory.Dispose();

            CreateConstantBuffers();

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
            _samplerState = GraphicsInitializer.CreateSamplerState(graphicsSettings.TextureSampling);
            
            OnResize(null, EventArgs.Empty);

            _device.ImmediateContext.PixelShader.SetSampler(0, _samplerState);
        }

        private void CreateSharedFields(GraphicsSettings graphicsSettings)
        {
            Camera = new Camera();
            LightManager = new LightManager();
            LightManager.GlobalAmbient = graphicsSettings.GlobalAmbientOcclusion;

            _sampleDescription = new SampleDescription(graphicsSettings.MultisamplesPerPixel, graphicsSettings.MultisamplerQuality);
            _depthBufferDescription = new Texture2DDescription()
            {
                Format = Format.D32_Float_S8X24_UInt,
                ArraySize = 1,
                MipLevels = 1,
                Width = 0,
                Height = 0,
                SampleDescription = _sampleDescription,
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };

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
            SharpDX.Utilities.Dispose(ref _backBuffer);
            SharpDX.Utilities.Dispose(ref _renderView);
            SharpDX.Utilities.Dispose(ref _depthBuffer);
            SharpDX.Utilities.Dispose(ref _depthView);

            _swapChain.ResizeBuffers(_swapChainDescription.BufferCount, RenderForm.ClientSize.Width, RenderForm.ClientSize.Height, Format.Unknown, SwapChainFlags.None);
            _backBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);
            _renderView = new RenderTargetView(_device, _backBuffer);

            _depthBufferDescription.Width = RenderForm.ClientSize.Width;
            _depthBufferDescription.Height = RenderForm.ClientSize.Height;
            _depthBuffer = new Texture2D(_device, _depthBufferDescription);

            _depthView = new DepthStencilView(_device, _depthBuffer);

            _device.ImmediateContext.Rasterizer.SetViewport(new Viewport(0, 0, RenderForm.ClientSize.Width, RenderForm.ClientSize.Height, 0.0f, 1.0f));
            _device.ImmediateContext.OutputMerger.SetTargets(_depthView, _renderView);
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
            _samplerState.Dispose();
            _device.ImmediateContext.InputAssembler.InputLayout.Dispose();
            _depthBuffer.Dispose();
            _depthView.Dispose();
            _renderView.Dispose();
            _backBuffer.Dispose();
            _device.ImmediateContext.ClearState();
            _device.ImmediateContext.Flush();
            _device.ImmediateContext.Dispose();
            _device.Dispose();
            _swapChain.Dispose();
        }
    }
}
