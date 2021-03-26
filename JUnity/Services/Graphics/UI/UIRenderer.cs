using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;

namespace JUnity.Services.Graphics.UI
{
    public sealed class UIRenderer : IDisposable
    {
        private SharpDX.Direct2D1.Factory _factory;
        private RenderTarget _renderTarget;

        private RenderTargetProperties _renderTargetProperties;

        internal RenderTarget RenderTarget { get; private set; }

        public void Initialize(RenderForm renderForm)
        {
            _factory = new SharpDX.Direct2D1.Factory();
            renderForm.UserResized += OnResize;

            _renderTargetProperties = new RenderTargetProperties
            {
                DpiX = 0,
                DpiY = 0,
                MinLevel = FeatureLevel.Level_10,
                PixelFormat = new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied),
                Type = RenderTargetType.Hardware,
                Usage = RenderTargetUsage.None,
            };

            OnResize(null, EventArgs.Empty);
        }

        private void OnResize(object sender, EventArgs e)
        {
            using (var surface = Engine.Instance.GraphicsRenderer.BackBuffer.QueryInterface<Surface>())
            {
                _renderTarget = new RenderTarget(_factory, surface, _renderTargetProperties);
            }

            _renderTarget.AntialiasMode = AntialiasMode.PerPrimitive;
            _renderTarget.TextAntialiasMode = TextAntialiasMode.Cleartype;
        }

        internal void RenderUI()
        {
            _renderTarget.BeginDraw();



            _renderTarget.EndDraw();
        }

        public void Dispose()
        {
            _renderTarget.Dispose();
            _factory.Dispose();
        }
    }
}
