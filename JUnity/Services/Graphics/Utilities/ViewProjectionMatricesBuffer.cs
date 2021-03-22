using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics.Utilities
{
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    internal struct ViewProjectionMatricesBuffer
    {
        [FieldOffset(0)]
        public Matrix ViewMatrix;

        [FieldOffset(64)]
        public Matrix ProjectionMatrix;
    }
}
