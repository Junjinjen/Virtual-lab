using JUnity.Services.Graphics.UI.Styling;
using JUnity.Services.Graphics.UI.Surfaces;
using JUnity.Services.Graphics.UI.Surfaces.Interfaces;
using JUnity.Services.Graphics.Utilities;
using JUnity.Services.Input;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectInput;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JUnity.Components.UI
{
    public class TextBox : UIElement
    {
        private List<Key> _lastPressedKeys = new List<Key>();
        private bool _isInFocus;

        public TextBox()
        {
            Active = true;
            Style = new TextBoxStyle
            {
                TextStyle = new DisablingTextStyle
                {
                    TextFormat = new Services.Graphics.UI.Styling.TextFormat
                    {
                        FontFamily = "Consolas",
                        FontSize = 12.0f,
                        FontStretch = FontStretch.Normal,
                        FontStyle = FontStyle.Normal,
                        FontWeight = FontWeight.Normal,
                        ParagraphAlignment = ParagraphAlignment.Center,
                        TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                    },
                    Color = Color.Black,
                    DisabledColor = new Color(109, 109, 109, 255),
                },
                Border = new Border
                {
                    Color = new Color(122, 122, 122),
                    Width = 0.5f,
                },
                FocusedBorder = new Border
                {
                    Color = new Color(0, 120, 215),
                    Width = 0.5f,
                },
                DisabledBorder = new Border
                {
                    Color = new Color(204, 204, 204),
                    Width = 0.5f,
                },
                ActiveBackground = new SolidColorRectangle
                {
                    Color = Color.White,
                },
                DisabledBackground = new SolidColorRectangle
                {
                    Color = new Color(204, 204, 204, 255),
                },
            };
        }

        public event EventHandler Focus;
        public event EventHandler FocusLost;

        public bool Active { get; set; }

        public bool ReadOnly { get; set; }

        public TextBoxStyle Style { get; set; }

        public string Text { get; set; }

        internal override void HandleMouseDown(Vector2 mousePosition, MouseKey key)
        {
            if (Active && !ReadOnly)
            {
                Engine.Instance.UIController.SetFocus(this);
                _isInFocus = true;
                Focus?.Invoke(this, EventArgs.Empty);
            }
        }

        internal override void OnFocusLost()
        {
            _isInFocus = false;
            FocusLost?.Invoke(this, EventArgs.Empty);
        }

        internal override void HandleKeyboardInput(KeyboardState keyboardState)
        {
            var newKeys = keyboardState.PressedKeys.Except(_lastPressedKeys);

            var builder = new StringBuilder(Text);
            foreach (var key in newKeys)
            {
                switch (key)
                {
                    case Key.D1:
                        builder.Append(1);
                        break;
                    case Key.D2:
                        builder.Append(2);
                        break;
                    case Key.D3:
                        builder.Append(3);
                        break;
                    case Key.D4:
                        builder.Append(4);
                        break;
                    case Key.D5:
                        builder.Append(5);
                        break;
                    case Key.D6:
                        builder.Append(6);
                        break;
                    case Key.D7:
                        builder.Append(7);
                        break;
                    case Key.D8:
                        builder.Append(8);
                        break;
                    case Key.D9:
                        builder.Append(9);
                        break;
                    case Key.D0:
                        builder.Append(0);
                        break;
                    case Key.Back:
                        if (builder.Length > 0)
                        {
                            builder.Remove(builder.Length - 1, 1);
                        }
                        break;
                }
            }

            Text = builder.ToString();
            _lastPressedKeys = keyboardState.PressedKeys;
        }

        protected internal override void Render(RenderTarget renderTarget)
        {
            var rect = new RectangleF(Position.X * renderTarget.Size.Width, Position.Y * renderTarget.Size.Height,
                   Width * renderTarget.Size.Width, Height * renderTarget.Size.Height);

            if (Active)
            {
                Style.ActiveBackground.Draw(renderTarget, rect);
                DrawText(renderTarget, rect, Style.TextStyle.Color);

                if (_isInFocus)
                {
                    Style.FocusedBorder.Draw(renderTarget, rect);
                }
                else
                {
                    Style.Border.Draw(renderTarget, rect);
                }
            }
            else
            {
                Style.DisabledBackground.Draw(renderTarget, rect);
                DrawText(renderTarget, rect, Style.TextStyle.DisabledColor);
                Style.DisabledBorder.Draw(renderTarget, rect);
            }
        }

        private void DrawText(RenderTarget renderTarget, RectangleF rect, Color textColor)
        {
            var brush = UICommonFactory.GetInstance().CreateSolidColorBrush(textColor);
            var textFormat = UICommonFactory.GetInstance().CreateTextFormat(Style.TextStyle.TextFormat);

            renderTarget.DrawText(Text, textFormat, rect, brush);
        }
    }

    public class TextBoxStyle
    {
        public DisablingTextStyle TextStyle { get; set; }

        public Border Border { get; set; }

        public Border FocusedBorder { get; set; }

        public Border DisabledBorder { get; set; }

        public IRectangleSurface ActiveBackground { get; set; }

        public IRectangleSurface DisabledBackground { get; set; }
    }
}
