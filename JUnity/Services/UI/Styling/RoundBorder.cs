using JUnity.Services.Graphics.Utilities;
using SharpDX;
using SharpDX.Direct2D1;

namespace JUnity.Services.UI.Styling
{
    public class RoundBorder
    {
        public Color Color { get; set; }

        public float Width { get; set; }

        public void Draw(RenderTarget renderTarget, SharpDX.Vector2 position, float radius)
        {
            var brush = UICommonFactory.GetInstance().CreateSolidColorBrush(Color);
            renderTarget.DrawEllipse(new Ellipse(position, radius, radius), brush);
        }
    }
}
