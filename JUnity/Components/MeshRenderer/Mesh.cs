using JUnity.Services.Graphics.Material;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace JUnity.Components.MeshRenderer
{
    public sealed class Mesh
    {
        public VertexBufferBinding VertexBufferBinding { get; }

        public Buffer IndexBuffer { get; }

        public int IndicesCount { get; }

        public PrimitiveTopology PrimitiveTopology { get; }

        public Material Material { get; }
    }
}
