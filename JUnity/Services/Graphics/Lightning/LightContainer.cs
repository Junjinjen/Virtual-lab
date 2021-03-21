using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics.Lightning
{
    [StructLayout(LayoutKind.Explicit, Size = 48 + 64 * GraphicsSettings.MaxLightsCount + 48 * GraphicsSettings.MaxLightsCount + 32 * GraphicsSettings.MaxLightsCount)]
    internal struct LightContainer
    {
        [FieldOffset(0)]
        public Vector4 CameraPosition;

        [FieldOffset(16)]
        public int ActiveDirectionalLights;

        [FieldOffset(20)]
        public int ActivePointLights;

        [FieldOffset(24)]
        public int ActiveSpotLights;

        [FieldOffset(32)]
        public Vector3 GlobalAmbient;

        [FieldOffset(48), MarshalAs(UnmanagedType.ByValArray, SizeConst = GraphicsSettings.MaxLightsCount)]
        public SpotLight[] SpotLights;

        [FieldOffset(48 + 64 * GraphicsSettings.MaxLightsCount), MarshalAs(UnmanagedType.ByValArray, SizeConst = GraphicsSettings.MaxLightsCount)]
        public PointLight[] PointLights;

        [FieldOffset(48 + 64 * GraphicsSettings.MaxLightsCount + 48 * GraphicsSettings.MaxLightsCount),
            MarshalAs(UnmanagedType.ByValArray, SizeConst = GraphicsSettings.MaxLightsCount)]
        public DirectionalLight[] DirectionalLights;
    }
}
