using JUnity.Services.UI.KeybordHandlers;
using JUnity.Services.UI.Styling;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectInput;
using System;
using System.Globalization;

namespace JUnity.Components.UI
{
    public class FloatTextBox : TextBoxBase<FloatTextBoxStyle>
    {
        private float _value;

        public FloatTextBox()
            : base(new DigitalTextHandler())
        {
            Style.FormatErrorBorder = new Border
            {
                Color = new Color(214, 0, 68),
                Width = 0.5f,
            };
            Style.Border.Width = 3f;
            Style.DisabledBorder.Width = 3f;
            Style.FocusedBorder.Width = 3f;
            Style.FormatErrorBorder.Width = 3f;
        }

        public event EventHandler<FloatTextBoxValueChangedEventArgs> ValueChanged;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                RawText = value.ToString(CultureInfo.InvariantCulture);
                ValudateFormat();
                ValueChanged?.Invoke(this, new FloatTextBoxValueChangedEventArgs(_value));
            }
        }

        public float? MaxValue { get; set; }

        public float? MinValue { get; set; }

        public bool HasFormatError { get; private set; }

        internal override void HandleKeyboardInput(KeyboardState keyboardState)
        {
            base.HandleKeyboardInput(keyboardState);
            ValudateFormat();
        }

        protected internal override void Render(RenderTarget renderTarget)
        {
            var rect = new RectangleF(Position.X * renderTarget.Size.Width, Position.Y * renderTarget.Size.Height,
                   Width * renderTarget.Size.Width, Height * renderTarget.Size.Height);

            if (Active)
            {
                Style.ActiveBackground.Draw(renderTarget, rect);
                DrawText(renderTarget, rect, Style.TextStyle.Color);

                if (HasFormatError)
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

        private void ValudateFormat()
        {
            var text = RawText.Replace(',', '.');
            var previousValue = _value;
            HasFormatError = !float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out _value) ||
                (MinValue != null && _value < MinValue) ||
                (MaxValue != null && _value > MaxValue);

            if (!HasFormatError && _value != previousValue)
            {
                ValueChanged?.Invoke(this, new FloatTextBoxValueChangedEventArgs(_value));
            }
        }
    }

    public class FloatTextBoxStyle : TextBoxBaseStyle
    {
        public Border FormatErrorBorder { get; set; }
    }

    public class FloatTextBoxValueChangedEventArgs : EventArgs
    {
        public FloatTextBoxValueChangedEventArgs(float value)
        {
            Value = value;
        }

        public float Value { get; }
    }
}