using SharpDX;
using SharpDX.Direct3D11;

namespace JUnity.Services.Graphics.Meshing
{
    internal struct RenderOrder
    {
        public Mesh Mesh;

        public Matrix WorldMatrix;

        public VertexShader VertexShader;

        public PixelShader PixelShader;
    }
}
