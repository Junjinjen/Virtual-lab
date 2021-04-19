using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace JUnity.Services.Graphics.Meshing
{
    public sealed class Mesh
    {
        public Mesh(VertexDescription[] vertices, uint[] indices, Material material, PrimitiveTopology primitiveTopology = PrimitiveTopology.TriangleList)
        {
            Material = material;
            IndicesCount = indices.Length;
            PrimitiveTopology = primitiveTopology;

            var verticesBuffer = Buffer.Create(Engine.Instance.GraphicsRenderer.Device, BindFlags.VertexBuffer,
                vertices, SharpDX.Utilities.SizeOf<VertexDescription>() * vertices.Length);

            VertexBufferBinding = new VertexBufferBinding(verticesBuffer, SharpDX.Utilities.SizeOf<VertexDescription>(), 0);
            IndexBuffer = Buffer.Create(Engine.Instance.GraphicsRenderer.Device, BindFlags.IndexBuffer, indices, sizeof(uint) * IndicesCount);
        }

        public VertexBufferBinding VertexBufferBinding { get; }

        public Buffer IndexBuffer { get; }

        public int IndicesCount { get; }

        public PrimitiveTopology PrimitiveTopology { get; }

        public Material Material { get; set; }
    }
}
