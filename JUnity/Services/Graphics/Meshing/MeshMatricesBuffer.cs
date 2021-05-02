using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics.Meshing
{
    [StructLayout(LayoutKind.Explicit, Size = 192)]
    internal struct MeshMatrices
    {
        [FieldOffset(0)]
        public Matrix WorldViewProjectionMatrix;

        [FieldOffset(64)]
        public Matrix WorldMatrix;

        [FieldOffset(128)]
        public Matrix InverseWorldMatrix;
    }
}
