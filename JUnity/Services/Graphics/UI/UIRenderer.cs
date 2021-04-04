using JUnity.Components.UI;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using Direct2DFactory = SharpDX.Direct2D1.Factory;
using DirectWriteFactory = SharpDX.DirectWrite.Factory;

namespace JUnity.Services.Graphics.UI
{
    internal sealed class UIRenderer : IDisposable
    {
        private Direct2DFactory _direct2DFactory;
        private DirectWriteFactory _directWriteFactory;

        private RenderTargetProperties _renderTargetProperties;
        private readonly List<UIElement> _drawOrder = new List<UIElement>();

        internal RenderTarget RenderTarget { get; private set; }

        internal DirectWriteFactory DirectWriteFactory { get => _directWriteFactory; }

        public void Initialize(RenderForm renderForm)
        {
            _direct2DFactory = new Direct2DFactory();
            _directWriteFactory = new DirectWriteFactory();
            renderForm.Resize += OnResize;

            _renderTargetProperties = new RenderTargetProperties
            {
                DpiX = 0,
                DpiY = 0,
                MinLevel = FeatureLevel.Level_10,
                PixelFormat = new PixelFormat(Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied),
                Type = RenderTargetType.Hardware,
                Usage = RenderTargetUsage.None,
            };

            OnResize(null, EventArgs.Empty);
        }

        private void OnResize(object sender, EventArgs e)
        {
            using (var surface = Engine.Instance.GraphicsRenderer.BackBuffer.QueryInterface<Surface>())
            {
                RenderTarget = new RenderTarget(_direct2DFactory, surface, _renderTargetProperties);
            }

            RenderTarget.AntialiasMode = AntialiasMode.PerPrimitive;
            RenderTarget.TextAntialiasMode = TextAntialiasMode.Cleartype;
        }

        public void AddElementToDrawOrder(UIElement element)
        {
            _drawOrder.Add(element);
        }

        public void RenderUI()
        {
            RenderTarget.BeginDraw();

            foreach (var element in _drawOrder)
            {
                element.Render(RenderTarget);
            }

            _drawOrder.Clear();
            RenderTarget.EndDraw();
        }

        public void Dispose()
        {
            RenderTarget.Dispose();
            _direct2DFactory.Dispose();
        }
    }
}
