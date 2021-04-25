using JUnity.Services.UI.Formatters;
using JUnity.Services.UI.KeybordHandlers;
using JUnity.Services.UI.Styling;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectInput;
using System.Globalization;

namespace JUnity.Components.UI
{
    public class FloatTextBox : TextBoxBase<FloatTextBoxStyle>
    {
        private bool _hasFormatError;
        private float _value;

        public FloatTextBox()
            : base(new DigitalTextHandler())
        {
            Style.FormatErrorBorder = new Border
            {
                Color = new SharpDX.Color(214, 0, 68),
                Width = 0.5f,
            };
        }

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                RawText = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        internal override void HandleKeyboardInput(KeyboardState keyboardState)
        {
            base.HandleKeyboardInput(keyboardState);
            var text = RawText.Replace(',', '.');

            _hasFormatError = !float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out _value);
        }

        protected internal override void Render(RenderTarget renderTarget)
        {
            var rect = new RectangleF(Position.X * renderTarget.Size.Width, Position.Y * renderTarget.Size.Height,
                   Width * renderTarget.Size.Width, Height * renderTarget.Size.Height);

            if (Active)
            {
                Style.ActiveBackground.Draw(renderTarget, rect);
                DrawText(renderTarget, rect, Style.TextStyle.Color);

                if (_hasFormatError)
                {
                    Style.FormatErrorBorder.Draw(renderTarget, rect);
                }
                else
                {
                    if (Focused)
                    {
                        Style.FocusedBorder.Draw(renderTarget, rect);
                    }
                    else
                    {
                        Style.Border.Draw(renderTarget, rect);
                    }
                }
            }
            else
            {
                Style.DisabledBackground.Draw(renderTarget, rect);
                DrawText(renderTarget, rect, Style.TextStyle.DisabledColor);
                Style.DisabledBorder.Draw(renderTarget, rect);
            }
        }
    }

    public class FloatTextBoxStyle : TextBoxBaseStyle
    {
        public Border FormatErrorBorder { get; set; }
    }
}
