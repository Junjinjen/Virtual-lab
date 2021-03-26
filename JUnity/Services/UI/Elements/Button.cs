using JUnity.Services.Graphics.UI.Styling;
using JUnity.Services.Graphics.Utilities;
using JUnity.Services.Input;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using System;

namespace JUnity.Services.UI.Elements
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
                TextStyle = new ButtonTextStyle
                {
                    TextColor = Color.Black,
                    DisabledTextColor = new Color(175, 160, 175, 255),
                    FontFamily = "Consolas",
                    FontSize = 12.0f,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Normal,
                    FontWeight = FontWeight.Normal,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                },
                ActiveBackground = new SurfaceStyle
                {
                    Color = new Color(225, 225, 225, 255),
                    Opacity = 1.0f,
                },
                DisabledBackground = new SurfaceStyle
                {
                    Color = new Color(204, 204, 204, 255),
                    Opacity = 1.0f,
                },
                PressedBackground = new SurfaceStyle
                {
                    Color = new Color(204, 228, 247, 255),
                    Opacity = 1.0f,
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
            if (Active)
            {
                if (_isPressed)
                {
                    DrawBackground(renderTarget, Style.PressedBackground);
                }
                else
                {
                    DrawBackground(renderTarget, Style.ActiveBackground);
                }

                DrawText(renderTarget, Style.TextStyle.TextColor);
            }
            else
            {
                DrawBackground(renderTarget, Style.DisabledBackground);
                DrawText(renderTarget, Style.TextStyle.DisabledTextColor);
            }
        }

        private void DrawText(RenderTarget renderTarget, Color textColor)
        {
            var rect = new RectangleF(Position.X * renderTarget.Size.Width, Position.Y * renderTarget.Size.Height,
                Width * renderTarget.Size.Width, Height * renderTarget.Size.Height);
            var brush = SolidColorBrushFactory.GetInstance().Create(textColor);
            renderTarget.DrawText(Text, Style.TextStyle.TextFormat, rect, brush);
        }

        private void DrawBackground(RenderTarget renderTarget, SurfaceStyle backgroundStyle)
        {
            var rect = new RectangleF(Position.X * renderTarget.Size.Width, Position.Y * renderTarget.Size.Height,
                Width * renderTarget.Size.Width, Height * renderTarget.Size.Height);
            if (backgroundStyle.Texture != null)
            {
                renderTarget.DrawBitmap(backgroundStyle.Texture.Bitmap, rect,
                    backgroundStyle.Opacity, BitmapInterpolationMode.Linear);
            }
            else
            {
                var brush = SolidColorBrushFactory.GetInstance().Create(backgroundStyle.Color);
                renderTarget.FillRectangle(rect, brush);
            }
        }

        public class ButtonStyle
        {
            public ButtonTextStyle TextStyle { get; set; }

            public SurfaceStyle ActiveBackground { get; set; }

            public SurfaceStyle DisabledBackground { get; set; }

            public SurfaceStyle PressedBackground { get; set; }
        }
    }
}
