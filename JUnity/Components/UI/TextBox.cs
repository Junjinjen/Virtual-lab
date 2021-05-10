using JUnity.Services.UI.KeybordHandlers;

namespace JUnity.Components.UI
{
    public class TextBox : TextBoxBase<TextBoxBaseStyle>
    {
        private string _value;

        public TextBox()
            : base(new EmptyTextHandler())
        {
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
