using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics.Lightning
{
    [StructLayout(LayoutKind.Explicit, Size = 48)]
    internal struct PointLightDescription
    {
        [FieldOffset(0)]
        public Vector3 Position;

        [FieldOffset(16)]
        public Color3 Color;

        [FieldOffset(32)]
        public Vector3 Attenuation;
    }
}
