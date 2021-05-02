using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using System;

namespace JUnity.Services.Graphics.Meshing
{
    public sealed class Mesh : IDisposable
    {
        public Mesh(VertexDescription[] vertices, uint[] indices, Material material, PrimitiveTopology primitiveTopology = PrimitiveTopology.TriangleList)
        {
            Material = material;
            IndicesCount = indices.Length;
            PrimitiveTopology = primitiveTopology;

            var verticesBuffer = SharpDX.Direct3D11.Buffer.Create(Engine.Instance.GraphicsRenderer.Device, BindFlags.VertexBuffer,
                vertices, SharpDX.Utilities.SizeOf<VertexDescription>() * vertices.Length);

            VertexBufferBinding = new VertexBufferBinding(verticesBuffer, SharpDX.Utilities.SizeOf<VertexDescription>(), 0);
            IndexBuffer = SharpDX.Direct3D11.Buffer.Create(Engine.Instance.GraphicsRenderer.Device, BindFlags.IndexBuffer, indices, sizeof(uint) * IndicesCount);
        }

        public VertexBufferBinding VertexBufferBinding { get; }

        public SharpDX.Direct3D11.Buffer IndexBuffer { get; }

        public int IndicesCount { get; }

        public PrimitiveTopology PrimitiveTopology { get; }

        public Material Material { get; set; }

        public void Dispose()
        {
            VertexBufferBinding.Buffer.Dispose();
            IndexBuffer.Dispose();
            Material.Dispose();
        }

        ~Mesh()
        {
            Dispose();
        }
    }
}
