using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics.Meshing
{
    [StructLayout(LayoutKind.Explicit, Size = 80)]
    internal struct MaterialDescription
    {
        [FieldOffset(0)]
        public Color4 EmissivityCoefficient;

        [FieldOffset(16)]
        public Color4 AmbientCoefficient;

        [FieldOffset(32)]
        public Color4 DiffusionCoefficient;

        [FieldOffset(48)]
        public Color4 SpecularCoefficient;

        [FieldOffset(64)]
        public float SpecularPower;

        [FieldOffset(68)]
        public bool IsTexturePresent;
    }
}
