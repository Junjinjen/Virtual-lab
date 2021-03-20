using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Collections.Generic;

namespace JUnity.Services.Graphics
{
    public sealed class GraphicsRenderer : IDisposable
    {
        private Device _device;
        private SwapChainDescription _swapChainDescription;
        private SwapChain _swapChain;
        private Texture2D _depthBuffer;
        private DepthStencilView _depthView;
        private Texture2D _backBuffer;
        private RenderTargetView _renderView;
        private SamplerState _samplerState;

        private Dictionary<string, VertexShader> _vertexShaders;
        private Dictionary<string, PixelShader> _pixelShaders;
        private SampleDescription _sampleDescription;
        private Texture2DDescription _depthBufferDescription;

        public Dictionary<string, VertexShader> VertexShaders { get => _vertexShaders; }

        public Dictionary<string, PixelShader> PixelShaders { get => _pixelShaders; }

        internal Device Device { get => _device; private set => _device = value; }

        internal RenderForm RenderForm { get; private set; }

        internal void RenderScene()
        {
            
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

            GraphicsInitializer.InitializeShaders(graphicsSettings.ShadersMetaPath, out var inputSignature, out _vertexShaders, out _pixelShaders);

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
        }

        private void CreateSharedFields(GraphicsSettings graphicsSettings)
        {
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
