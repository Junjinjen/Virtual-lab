using JUnity.Services.Graphics.Utilities;
using JUnity.Services.UI.Surfaces.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;

namespace JUnity.Services.UI.Surfaces
{
    public class SolidColorRectangle : IRectangleSurface
    {
        public Color Color { get; set; }

        public void Draw(RenderTarget renderTarget, RectangleF rectangle)
        {
            var brush = UICommonFactory.GetInstance().CreateSolidColorBrush(Color);
            renderTarget.FillRectangle(rectangle, brush);
        }

        public static SolidColorRectangle GetInvisible()
        {
            return new SolidColorRectangle
            {
                Color = new Color(0, 0, 0, 0),
            };
        }
    }
}
