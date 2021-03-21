using JUnity.Services.Graphics.Utilities;
using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics.Lightning
{
    [StructLayout(LayoutKind.Explicit, Size = 48 + 64 * GraphicsSettings.MaxLightsPerTypeCount + 48 * GraphicsSettings.MaxLightsPerTypeCount + 32 * GraphicsSettings.MaxLightsPerTypeCount)]
    internal struct LightContainer
    {
        [FieldOffset(0)]
        public Vector3 CameraPosition;

        [FieldOffset(16)]
        public int ActiveDirectionalLights;

        [FieldOffset(20)]
        public int ActivePointLights;

        [FieldOffset(24)]
        public int ActiveSpotLights;

        [FieldOffset(32)]
        public Vector3 GlobalAmbient;

        [FieldOffset(48), MarshalAs(UnmanagedType.ByValArray, SizeConst = GraphicsSettings.MaxLightsPerTypeCount)]
        public SpotLight[] SpotLights;

        [FieldOffset(48 + 64 * GraphicsSettings.MaxLightsPerTypeCount), MarshalAs(UnmanagedType.ByValArray, SizeConst = GraphicsSettings.MaxLightsPerTypeCount)]
        public PointLight[] PointLights;

        [FieldOffset(48 + 64 * GraphicsSettings.MaxLightsPerTypeCount + 48 * GraphicsSettings.MaxLightsPerTypeCount),
            MarshalAs(UnmanagedType.ByValArray, SizeConst = GraphicsSettings.MaxLightsPerTypeCount)]
        public DirectionalLight[] DirectionalLights;
    }
}
