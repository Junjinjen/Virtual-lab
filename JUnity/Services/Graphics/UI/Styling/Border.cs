using JUnity.Services.Graphics.Utilities;
using SharpDX;
using SharpDX.Direct2D1;

namespace JUnity.Services.Graphics.UI.Styling
{
    public class Border
    {
        public Color Color { get; set; }

        public float Width { get; set; }

        public void Draw(RenderTarget renderTarget, RectangleF rectangle)
        {
            var brush = UICommonFactory.GetInstance().CreateSolidColorBrush(Color);
            renderTarget.DrawRectangle(rectangle, brush, Width);
        }
    }
}
