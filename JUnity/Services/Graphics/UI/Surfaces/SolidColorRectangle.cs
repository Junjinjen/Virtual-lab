using JUnity.Services.Graphics.UI.Surfaces.Interfaces;
using JUnity.Services.Graphics.Utilities;
using SharpDX;
using SharpDX.Direct2D1;

namespace JUnity.Services.Graphics.UI.Surfaces
{
    public class SolidColorRectangle : IRectangleSurface
    {
        public Color4 Color { get; set; }

        public void Draw(RenderTarget renderTarget, RectangleF rectangle)
        {
            var brush = UICommonFactory.GetInstance().CreateSolidColorBrush(Color);
            renderTarget.FillRectangle(rectangle, brush);
        }
    }
}
