using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics.Lightning
{
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    internal struct SpotLight
    {
        [FieldOffset(0)]
        public Vector3 Position;

        [FieldOffset(16)]
        public Vector3 Direction;

        [FieldOffset(28)]
        public float Angle;

        [FieldOffset(32)]
        public Vector3 Color;

        [FieldOffset(48)]
        public Vector3 Attenuation;
    }
}
