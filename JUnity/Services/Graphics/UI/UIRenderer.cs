using JUnity.Services.UI.Elements;
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
        private RenderTarget _renderTarget;

        private RenderTargetProperties _renderTargetProperties;
        private readonly List<UIElement> _drawOrder = new List<UIElement>();

        internal RenderTarget RenderTarget { get; private set; }

        internal DirectWriteFactory DirectWriteFactory { get => _directWriteFactory; }

        public void Initialize(RenderForm renderForm)
        {
            _direct2DFactory = new Direct2DFactory();
            _directWriteFactory = new DirectWriteFactory();
            renderForm.ResizeEnd += RenderForm_ResizeEnd;
            renderForm.ResizeBegin += RenderForm_ResizeBegin;

            _renderTargetProperties = new RenderTargetProperties
            {
                DpiX = 0,
                DpiY = 0,
                MinLevel = FeatureLevel.Level_10,
                PixelFormat = new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied),
                Type = RenderTargetType.Hardware,
                Usage = RenderTargetUsage.None,
            };

            RenderForm_ResizeEnd(null, EventArgs.Empty);
        }

        private void RenderForm_ResizeBegin(object sender, EventArgs e)
        {
            _renderTarget?.Dispose();
        }

        private void RenderForm_ResizeEnd(object sender, EventArgs e)
        {
            using (var surface = Engine.Instance.GraphicsRenderer.BackBuffer.QueryInterface<Surface>())
            {
                _renderTarget = new RenderTarget(_direct2DFactory, surface, _renderTargetProperties);
            }

            _renderTarget.AntialiasMode = AntialiasMode.PerPrimitive;
            _renderTarget.TextAntialiasMode = TextAntialiasMode.Cleartype;
        }

        public void AddElementToDrawOrder(UIElement element)
        {
            _drawOrder.Add(element);
        }

        public void RenderUI()
        {
            _renderTarget.BeginDraw();

            foreach (var element in _drawOrder)
            {
                element.Render(_renderTarget);
            }

            _drawOrder.Clear();
            _renderTarget.EndDraw();
        }

        public void Dispose()
        {
            _renderTarget.Dispose();
            _direct2DFactory.Dispose();
        }
    }
}
