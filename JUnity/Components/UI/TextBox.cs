using JUnity.Services.UI.KeybordHandlers;
using JUnity.Services.UI.Surfaces;
using SharpDX;
using SharpDX.Direct2D1;

namespace JUnity.Components.UI
{
    public class TextBox : TextBoxBase<TextBoxBaseStyle>
    {
        private string _value;

        public TextBox()
            : base(new EmptyTextHandler())
        {
            Style.ActiveBackground = SolidColorRectangle.GetInvisible();
            Style.FocusedBorder.Color = new Color(0, 0, 0, 0);
            Style.Border.Color = new Color(0, 0, 0, 0);
        }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                RawText = value;
            }
        }
    }
}
