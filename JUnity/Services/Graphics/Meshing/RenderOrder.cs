using SharpDX.Direct3D11;

namespace JUnity.Services.Graphics.Meshing
{
    internal struct RenderOrder
    {
        public Mesh Mesh;

        public GameObject GameObject;

        public VertexShader VertexShader;

        public PixelShader PixelShader;
    }
}
