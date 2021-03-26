using SharpDX;
using SharpDX.DirectWrite;

namespace JUnity.Services.Graphics.UI.Styling
{
    public class TextStyle
    {
        private string _fontFamily;
        private float _fontSize;
        private FontWeight _fontWeight;
        private FontStyle _fontStyle;
        private FontStretch _fontStretch;
        private TextAlignment _textAlignment;
        private ParagraphAlignment _paragraphAlignment;

        private TextFormat _textFormat;

        public Color TextColor { get; set; }

        public string FontFamily
        {
            get => _fontFamily;
            set
            {
                _fontFamily = value;
                _textFormat?.Dispose();
                _textFormat = null;
            }
        }

        public float FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                _textFormat?.Dispose();
                _textFormat = null;
            }
        }

        public FontWeight FontWeight
        {
            get => _fontWeight;
            set
            {
                _fontWeight = value;
                _textFormat?.Dispose();
                _textFormat = null;
            }
        }

        public FontStyle FontStyle
        {
            get => _fontStyle;
            set
            {
                _fontStyle = value;
                _textFormat?.Dispose();
                _textFormat = null;
            }
        }

        public FontStretch FontStretch
        {
            get => _fontStretch;
            set
            {
                _fontStretch = value;
                _textFormat?.Dispose();
                _textFormat = null;
            }
        }

        public TextAlignment TextAlignment
        {
            get => _textAlignment;
            set
            {
                _textAlignment = value;
                _textFormat?.Dispose();
                _textFormat = null;
            }
        }

        public ParagraphAlignment ParagraphAlignment
        {
            get => _paragraphAlignment;
            set
            {
                _paragraphAlignment = value;
                _textFormat?.Dispose();
                _textFormat = null;
            }
        }

        internal TextFormat TextFormat
        {
            get
            {
                if (_textFormat == null)
                {
                    _textFormat = new TextFormat(Engine.Instance.GraphicsRenderer.UIRenderer.DirectWriteFactory, FontFamily, FontWeight, FontStyle, FontStretch, FontSize);
                    _textFormat.TextAlignment = TextAlignment;
                    _textFormat.ParagraphAlignment = ParagraphAlignment;
                }

                return _textFormat;
            }
        }
    }
}
