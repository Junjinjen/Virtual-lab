using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexDescription
    {
        public Vector4 Position;
        public Vector4 Normal;
        public Vector4 Color;
        public Vector2 TextureCoordinate;
    }
}
