using JUnity.Services.Input;
using JUnity.Services.UI;
using JUnity.Services.UI.Styling;
using JUnity.Services.UI.Surfaces;
using JUnity.Services.UI.Surfaces.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;

namespace JUnity.Components.UI
{
    public class RadioButton : UIElement
    {
        private const float DotMinimizingPercent = 0.2f;

        private static readonly Dictionary<string, List<RadioButton>> _groups = new Dictionary<string, List<RadioButton>>();
        private readonly string _myGroup;
        private bool _isPressed;
        private bool _active;
        private bool _checked;

        public RadioButton(string group)
        {
            _myGroup = group;

            Active = true;
            Style = new RadioButtonStyle
            {
                Background = new SolidColorRound
                {
                    Color = Color.White,
                },
                Dot = new SolidColorRound
                {
                    Color = new Color(51, 51, 51),
                },
                Border = new RoundBorder
                {
                    Color = new Color(51, 51, 51),
                    Width = 0.5f,
                },
            };

            if (_groups.TryGetValue(group, out var list))
            {
                list.Add(this);
            }
            else
            {
                _groups.Add(group, new List<RadioButton> { this });
            }
        }

        public event EventHandler Check;

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                _isPressed = false;
            }
        }

        public RadioButtonStyle Style { get; set; }

        public bool Checked
        {
            get => _checked;
            set
            {
                _groups[_myGroup].ForEach(x => x._checked = false);
                _checked = value;
                Check?.Invoke(this, EventArgs.Empty);
            }
        }

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
                Checked = true;
            }

            _isPressed = false;
        }

        protected internal override void Render(RenderTarget renderTarget)
        {
            var rect = new RectangleF(Position.X * renderTarget.Size.Width, Position.Y * renderTarget.Size.Height,
                   Width * renderTarget.Size.Width, Height * renderTarget.Size.Height);

            var min = Math.Min(rect.Width, rect.Height);
            Style.Background.Draw(renderTarget, rect.Center, min / 2.0f);
            Style.Border.Draw(renderTarget, rect.Center, min / 2.0f);
            if (Checked)
            {
                Style.Dot.Draw(renderTarget, rect.Center, (min / 2.0f) - (min * DotMinimizingPercent));
            }
        }

        public class RadioButtonStyle
        {
            public IRoundSurface Background { get; set; }

            public IRoundSurface Dot { get; set; }

            public RoundBorder Border { get; set; }
        }
    }
}
