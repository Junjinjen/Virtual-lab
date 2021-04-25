using SharpDX;
using SharpDX.Direct2D1;
using System.Collections.Generic;
using JUnityTextFormat = JUnity.Services.UI.Styling.TextFormat;
using DirectWriteTextFormat = SharpDX.DirectWrite.TextFormat;

namespace JUnity.Services.Graphics.Utilities
{
    internal class UICommonFactory
    {
        private static UICommonFactory _instance;
        private RenderTarget _oldRenderTarget;

        private readonly Dictionary<Color, SolidColorBrush> _brushes = new Dictionary<Color, SolidColorBrush>();
        private readonly Dictionary<JUnityTextFormat, DirectWriteTextFormat> _textFormats = new Dictionary<JUnityTextFormat, DirectWriteTextFormat>();

        private UICommonFactory()
        {
        }

        public static UICommonFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UICommonFactory();
            }

            return _instance;
        }

        public SolidColorBrush CreateSolidColorBrush(Color color)
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

        public DirectWriteTextFormat CreateTextFormat(JUnityTextFormat format)
        {
            if (!_textFormats.TryGetValue(format, out var textFormat))
            {
                textFormat = new DirectWriteTextFormat(Engine.Instance.GraphicsRenderer.UIRenderer.DirectWriteFactory,
                    format.FontFamily, format.FontWeight, format.FontStyle, format.FontStretch, format.FontSize)
                {
                    TextAlignment = format.TextAlignment,
                    ParagraphAlignment = format.ParagraphAlignment
                };

                _textFormats.Add(format, textFormat);
                return textFormat;
            }
            else
            {
                return textFormat;
            }
        }
    }
}
