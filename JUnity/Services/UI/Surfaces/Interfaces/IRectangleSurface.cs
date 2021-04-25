using SharpDX;
using SharpDX.Direct2D1;

namespace JUnity.Services.UI.Surfaces.Interfaces
{
    public interface IRectangleSurface
    {
        void Draw(RenderTarget renderTarget, RectangleF rectangle);
    }
}
