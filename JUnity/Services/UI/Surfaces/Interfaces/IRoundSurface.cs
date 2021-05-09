using SharpDX;
using SharpDX.Direct2D1;

namespace JUnity.Services.UI.Surfaces.Interfaces
{
    public interface IRoundSurface
    {
        void Draw(RenderTarget renderTarget, Vector2 position, float radius);
    }
}
