using JUnity.Services.Graphics.Utilities;
using JUnity.Services.UI.Surfaces.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;

namespace JUnity.Services.UI.Surfaces
{
    public class SolidColorRound : IRoundSurface
    {
        public Color Color { get; set; }

        public void Draw(RenderTarget renderTarget, SharpDX.Vector2 position, float radius)
        {
            var brush = UICommonFactory.GetInstance().CreateSolidColorBrush(Color);
            renderTarget.FillEllipse(new Ellipse(position, radius, radius), brush);
        }
    }
}
