using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace JUnity.Services.Graphics
{
    public sealed class Mesh
    {
        public Mesh(VertexDescription[] vertices, int[] indices, PrimitiveTopology primitiveTopology, Material material)
        {
            Material = material;
            IndicesCount = indices.Length;
            PrimitiveTopology = primitiveTopology;

            var verticesBuffer = Buffer.Create(Engine.Instance.GraphicsRenderer.Device, BindFlags.VertexBuffer,
                vertices, SharpDX.Utilities.SizeOf<VertexDescription>() * IndicesCount);

            VertexBufferBinding = new VertexBufferBinding(verticesBuffer, SharpDX.Utilities.SizeOf<VertexDescription>(), 0);
            IndexBuffer = Buffer.Create(Engine.Instance.GraphicsRenderer.Device, BindFlags.IndexBuffer, indices, SharpDX.Utilities.SizeOf<int>() * IndicesCount);
        }

        public VertexBufferBinding VertexBufferBinding { get; }

        public Buffer IndexBuffer { get; }

        public int IndicesCount { get; }

        public PrimitiveTopology PrimitiveTopology { get; }

        public Material Material { get; }
    }
}
