using JUnity.Services.UI.Surfaces;
using JUnity.Services.Graphics.Utilities;
using JUnity.Services.Input;
using JUnity.Services.UI.Styling;
using JUnity.Services.UI.Surfaces.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectInput;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using JUnity.Services.UI.KeybordHandlers;
using JUnity.Services.UI;

namespace JUnity.Components.UI
{
    public abstract class TextBoxBase<TStyle> : UIElement
        where TStyle : TextBoxBaseStyle, new()
    {
        private readonly IKeyboardHandler _keyboardHandler;
        private List<Key> _lastPressedKeys = new List<Key>();

        protected TextBoxBase(IKeyboardHandler keyboardHandler)
        {
            _keyboardHandler = keyboardHandler;
            Active = true;
            Style = new TStyle
            {
                TextStyle = new DisablingTextStyle
                {
                    TextFormat = new Services.UI.Styling.TextFormat
                    {
                        FontFamily = "TimesNewRoman",
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

        protected string RawText { get; set; }

        public event EventHandler Focus;
        public event EventHandler FocusLost;

        public bool Active { get; set; }

        public bool ReadOnly { get; set; }

        public bool Focused { get; private set; }

        public TStyle Style { get; set; }

        internal override void HandleMouseDown(Vector2 mousePosition, MouseKey key)
        {
            if (Active && !ReadOnly)
            {
                Engine.Instance.UIController.SetFocus(this);
                Focused = true;
                Focus?.Invoke(this, EventArgs.Empty);
            }
        }

        internal override void OnFocusLost()
        {
            Focused = false;
            FocusLost?.Invoke(this, EventArgs.Empty);
        }

        internal override void HandleKeyboardInput(KeyboardState keyboardState)
        {
            var newKeys = keyboardState.PressedKeys.Except(_lastPressedKeys);
            RawText = _keyboardHandler.HandleInput(RawText, newKeys, keyboardState.PressedKeys);
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

                if (Focused)
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

        protected void DrawText(RenderTarget renderTarget, RectangleF rect, Color textColor)
        {
            var brush = UICommonFactory.GetInstance().CreateSolidColorBrush(textColor);
            var textFormat = UICommonFactory.GetInstance().CreateTextFormat(Style.TextStyle.TextFormat);

            renderTarget.DrawText(RawText, textFormat, rect, brush, DrawTextOptions.Clip);
        }
    }

    public class TextBoxBaseStyle
    {
        public DisablingTextStyle TextStyle { get; set; }

        public Border Border { get; set; }

        public Border FocusedBorder { get; set; }

        public Border DisabledBorder { get; set; }

        public IRectangleSurface ActiveBackground { get; set; }

        public IRectangleSurface DisabledBackground { get; set; }
    }
}
