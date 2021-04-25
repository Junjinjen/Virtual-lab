﻿using SharpDX;
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
using System.Threading;
using JUnity.Utilities;
using JUnity.Services.UI;

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
        private DepthStencilView _depthStencilView;
        private RenderTargetView _renderTargetView;
        private RasterizerStateFactory _rasterizerStateFactory;

        private ConstantBuffer<LightContainer> _lightBuffer;
        private ConstantBuffer<MaterialDescription> _materialDescriptionBuffer;
        private ConstantBuffer<MeshMatrices> _meshMatricesBufferBuffer;

        private SampleDescription _sampleDescription;
        private Texture2DDescription _depthBufferDescription;
        private BlendStateDescription _blendStateDescription;
        private int _syncInterval;
        private bool _isMinimized;

        private readonly List<RenderOrder> _drawingQueue = new List<RenderOrder>();
        private readonly List<RenderOrder> _alwaysOnTopDrawingQueue = new List<RenderOrder>();

        public Device Device { get => _device; }

        public UIRenderer UIRenderer { get; private set; }

        public Texture2D BackBuffer { get; private set; }

        public RenderForm RenderForm { get; private set; }

        public LightManager LightManager { get; private set; }

        public Camera Camera { get; private set; }

        public Color4 BackgroundColor { get; set; }

        public ReadOnlyDictionary<string, VertexShader> VertexShaders { get; private set; }

        public ReadOnlyDictionary<string, PixelShader> PixelShaders { get; private set; }

        public void Initialize(Settings graphicsSettings)
        {
            CreateSharedFields(graphicsSettings);

            RenderForm = new RenderForm(graphicsSettings.WindowTitle);
            
            if (graphicsSettings.Borderless)
            {
                RenderForm.AllowUserResizing = false;
                RenderForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                RenderForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }

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

            RenderForm.Resize += OnResize;
            OnResize(null, EventArgs.Empty);

            UIRenderer.Initialize(RenderForm);
        }

        public void AddRenderOrder(RenderOrder order)
        {
            _drawingQueue.Add(order);
        }

        public void AddOnTopRenderOrder(RenderOrder order)
        {
            _alwaysOnTopDrawingQueue.Add(order);
        }

        public void RenderScene()
        {
            if (_isMinimized)
            {
                Thread.Sleep(33);
            }

            ClearBuffers();
            LightManager.CameraPosition = Camera.Position;

            UpdateLightning();
            var viewProjectionMatrix = Matrix.Multiply(Camera.GetViewMatrixTema(), Camera.GetPojectionMatrix());

            for (int i = 0; i < _drawingQueue.Count; i++)
            {
                Draw(_drawingQueue[i], ref viewProjectionMatrix);
            }

            for (int i = 0; i < _alwaysOnTopDrawingQueue.Count; i++)
            {
                Draw(_alwaysOnTopDrawingQueue[i], ref viewProjectionMatrix);
            }

            UIRenderer.RenderUI();
            EndRender();
        }

        private void Draw(RenderOrder renderOrder, ref Matrix viewProjectionMatrix)
        {
            if (renderOrder.Mesh.Material != null)
            {
                UpdateMaterial(renderOrder.Mesh.Material);
            }

            UpdateMeshMatrices(ref viewProjectionMatrix, ref renderOrder);

            _device.ImmediateContext.Rasterizer.State = _rasterizerStateFactory.Create(renderOrder.Mesh.Material.RasterizerState);

            _device.ImmediateContext.InputAssembler.PrimitiveTopology = renderOrder.Mesh.PrimitiveTopology;
            _device.ImmediateContext.InputAssembler.SetVertexBuffers(0, renderOrder.Mesh.VertexBufferBinding);
            _device.ImmediateContext.InputAssembler.SetIndexBuffer(renderOrder.Mesh.IndexBuffer, Format.R32_UInt, 0);

            _device.ImmediateContext.VertexShader.Set(renderOrder.VertexShader);
            _device.ImmediateContext.PixelShader.Set(renderOrder.PixelShader);

            _device.ImmediateContext.DrawIndexed(renderOrder.Mesh.IndicesCount, 0, 0);
        }

        private void UpdateMeshMatrices(ref Matrix viewProjectionMatrix, ref RenderOrder renderOrder)
        {
            var meshMatrices = new MeshMatrices
            {
                WorldMatrix = renderOrder.WorldMatrix,
                WorldViewProjectionMatrix = Matrix.Multiply(renderOrder.WorldMatrix, viewProjectionMatrix),
                InverseWorldMatrix = Matrix.Invert(renderOrder.WorldMatrix),
            };

            meshMatrices.WorldMatrix.Transpose();
            meshMatrices.WorldViewProjectionMatrix.Transpose();

            _meshMatricesBufferBuffer.Update(meshMatrices);
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

        private void CreateSharedFields(Settings graphicsSettings)
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
            BackBuffer?.Dispose();
            _renderTargetView?.Dispose();
            _depthBuffer?.Dispose();
            _depthStencilView?.Dispose();
            UIRenderer.RenderTarget?.Dispose();

            if (RenderForm.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                _isMinimized = true;
            }
            else if (_isMinimized)
            {
                _isMinimized = false;
            }

            _swapChain.ResizeBuffers(_swapChainDescription.BufferCount, 0, 0, Format.R8G8B8A8_UNorm, SwapChainFlags.None);
            BackBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);
            _renderTargetView = new RenderTargetView(_device, BackBuffer);

            _depthBufferDescription.Width = BackBuffer.Description.Width;
            _depthBufferDescription.Height = BackBuffer.Description.Height;
            _depthBuffer = new Texture2D(_device, _depthBufferDescription);

            _depthStencilView = new DepthStencilView(_device, _depthBuffer);
            var blendState = new BlendState(_device, _blendStateDescription);

            _device.ImmediateContext.Rasterizer.SetViewport(new Viewport(0, 0, BackBuffer.Description.Width,
                BackBuffer.Description.Height, 0.0f, 1.0f));

            _device.ImmediateContext.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);
            _device.ImmediateContext.OutputMerger.SetBlendState(blendState);

            Camera.UpdateAspectRatio();
        }

        private void ClearBuffers()
        {
            _device.ImmediateContext.ClearDepthStencilView(_depthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1f, 0);
            _device.ImmediateContext.ClearRenderTargetView(_renderTargetView, BackgroundColor);
        }

        private void EndRender()
        {
            _swapChain.Present(_syncInterval, PresentFlags.Restart);
            LightManager.ResetLight();
            _drawingQueue.Clear();
            _alwaysOnTopDrawingQueue.Clear();
        }

        public void Dispose()
        {
            _lightBuffer.Dispose();
            _meshMatricesBufferBuffer.Dispose();
            _materialDescriptionBuffer.Dispose();

            Helper.DisposeDictionaryElements(PixelShaders);
            Helper.DisposeDictionaryElements(VertexShaders);

            _depthBuffer.Dispose();
            _depthStencilView.Dispose();
            _renderTargetView.Dispose();
            BackBuffer.Dispose();
            _swapChain.Dispose();
            _device.Dispose();

            RenderForm.Dispose();
        }
    }
}
