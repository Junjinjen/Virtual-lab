using JUnity.Services.Graphics.Utilities;
using JUnity.Services.Input;
using JUnity.Services.UI;
using JUnity.Services.UI.Styling;
using JUnity.Services.UI.Surfaces;
using JUnity.Services.UI.Surfaces.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using System;

namespace JUnity.Components.UI
{
    public class Button : UIElement
    {
        private bool _isPressed;
        private bool _active;

        public event EventHandler Click;

        public Button()
        {
            Active = true;
            Style = new ButtonStyle
            {
                TextStyle = new DisablingTextStyle
                {
                    TextFormat = new Services.UI.Styling.TextFormat
                    {
                        FontFamily = "Consolas",
                        FontSize = 12.0f,
                        FontStretch = FontStretch.Normal,
                        FontStyle = FontStyle.Normal,
                        FontWeight = FontWeight.Normal,
                        ParagraphAlignment = ParagraphAlignment.Center,
                        TextAlignment = TextAlignment.Center,
                    },
                    Color = Color.Black,
                    DisabledColor = new Color(160, 160, 160, 255),
                },
                ActiveBackground = new SolidColorRectangle
                {
                    Color = new Color(225, 225, 225, 255),
                },
                DisabledBackground = new SolidColorRectangle
                {
                    Color = new Color(204, 204, 204, 255),
                },
                PressedBackground = new SolidColorRectangle
                {
                    Color = new Color(204, 228, 247, 255),
                },
            };
        }

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                _isPressed = false;
            }
        }

        public string Text { get; set; }

        public ButtonStyle Style { get; }

        internal override void HandleMouseDown(Vector2 mousePosition, MouseKey key)
        {
            if (Active && key == MouseKey.Left)
            {
                _isPressed = true;
            }
        }

        internal override void HandleMouseUp(Vector2 mousePosition, MouseKey key)
        {
            if (Active && _isPressed && key == MouseKey.Left && UIHelper.IsCursorOverElement(mousePosition, this))
            {
                Click?.Invoke(this, EventArgs.Empty);
            }

            _isPressed = false;
        }

        protected internal override void Render(RenderTarget renderTarget)
        {
            var rect = new RectangleF(Position.X * renderTarget.Size.Width, Position.Y * renderTarget.Size.Height,
                   Width * renderTarget.Size.Width, Height * renderTarget.Size.Height);

            if (Active)
            {
                if (_isPressed)
                {
                    Style.PressedBackground.Draw(renderTarget, rect);
                }
                else
                {
                    Style.ActiveBackground.Draw(renderTarget, rect);
                }

                DrawText(renderTarget, rect, Style.TextStyle.Color);
            }
            else
            {
                Style.DisabledBackground.Draw(renderTarget, rect);
                DrawText(renderTarget, rect, Style.TextStyle.DisabledColor);
            }
        }

        private void DrawText(RenderTarget renderTarget, RectangleF rect, Color textColor)
        {
            var brush = UICommonFactory.GetInstance().CreateSolidColorBrush(textColor);
            var textFormat = UICommonFactory.GetInstance().CreateTextFormat(Style.TextStyle.TextFormat);

            renderTarget.DrawText(Text, textFormat, rect, brush);
        }

        public class ButtonStyle
        {
            public DisablingTextStyle TextStyle { get; set; }

            public IRectangleSurface ActiveBackground { get; set; }

            public IRectangleSurface DisabledBackground { get; set; }

            public IRectangleSurface PressedBackground { get; set; }
        }
    }
}
