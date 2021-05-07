using JUnity.Services.UI;
using JUnity.Services.UI.Surfaces;
using JUnity.Services.UI.Surfaces.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;

namespace JUnity.Components.UI
{
    public class RectangleBackground : UIElement
    {
        public RectangleBackground()
        {
            Background = new SolidColorRectangle
            {
                Color = Color.Gray,
            };
        }

        public IRectangleSurface Background { get; set; }

        protected internal override void Render(RenderTarget renderTarget)
        {
            var rect = new RectangleF(Position.X * renderTarget.Size.Width, Position.Y * renderTarget.Size.Height,
                   Width * renderTarget.Size.Width, Height * renderTarget.Size.Height);

            Background.Draw(renderTarget, rect);
        }
    }
}
