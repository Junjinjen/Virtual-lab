using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics
{
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    internal struct MaterialDescription
    {
        [FieldOffset(0)]
        public Vector3 EmissivityCoefficient;

        [FieldOffset(12)]
        public float SpecularPower;

        [FieldOffset(16)]
        public Vector3 AmbientCoefficient;

        [FieldOffset(32)]
        public Vector3 DiffusionCoefficient;

        [FieldOffset(48)]
        public Vector3 SpecularCoefficient;

        [FieldOffset(60)]
        public bool IsTexturePresent;
    }
}
