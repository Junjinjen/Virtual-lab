using SharpDX;
using SharpDX.Direct2D1;
using System.Collections.Generic;

namespace JUnity.Services.Graphics.Utilities
{
    internal class SolidColorBrushFactory
    {
        private static SolidColorBrushFactory _instance;
        private RenderTarget _oldRenderTarget;
        private readonly Dictionary<Color, SolidColorBrush> _brushes = new Dictionary<Color, SolidColorBrush>();

        private SolidColorBrushFactory()
        {
        }

        public static SolidColorBrushFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SolidColorBrushFactory();
            }

            return _instance;
        }

        public SolidColorBrush Create(Color color)
        {
            if (!Engine.Instance.GraphicsRenderer.UIRenderer.RenderTarget.Equals(_oldRenderTarget))
            {
                Helper.DisposeDictionaryElements(_brushes);
                _brushes.Clear();
                _oldRenderTarget = Engine.Instance.GraphicsRenderer.UIRenderer.RenderTarget;
            }

            if (!_brushes.TryGetValue(color, out var brush))
            {
                brush = new SolidColorBrush(_oldRenderTarget, color);
                _brushes.Add(color, brush);
                return brush;
            }
            else
            {
                return brush;
            }
        }
    }
}
