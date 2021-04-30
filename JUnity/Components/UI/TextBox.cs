using JUnity.Services.UI.KeybordHandlers;
using JUnity.Services.UI.Styling;
using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
