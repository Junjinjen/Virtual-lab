using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics.Lightning
{
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    internal struct DirectionalLight
    {
        [FieldOffset(0)]
        public Vector3 Direction;

        [FieldOffset(16)]
        public Vector3 Color;
    }
}
