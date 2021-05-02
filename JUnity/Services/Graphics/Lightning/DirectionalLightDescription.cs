using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics.Lightning
{
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    internal struct DirectionalLightDescription
    {
        [FieldOffset(0)]
        public Vector3 Direction;

        [FieldOffset(16)]
        public Color3 Color;
    }
}
