using JUnity.Services.Graphics.Utilities;
using SharpDX;
using System.Runtime.InteropServices;

namespace JUnity.Services.Graphics.Lightning
{
    [StructLayout(LayoutKind.Explicit, Size = 48 + 64 * GraphicsSettings.MaxLightsPerTypeCount +
        48 * GraphicsSettings.MaxLightsPerTypeCount + 32 * GraphicsSettings.MaxLightsPerTypeCount)]
    internal struct LightContainer
    {
        [FieldOffset(0)]
        public Vector3 CameraPosition;

        [FieldOffset(12)]
        public int ActiveDirectionalLights;

        [FieldOffset(16)]
        public Vector3 GlobalAmbient;

        [FieldOffset(28)]
        public int ActivePointLights;

        [FieldOffset(32)]
        public int ActiveSpotLights;

        [FieldOffset(48), MarshalAs(UnmanagedType.ByValArray, SizeConst = GraphicsSettings.MaxLightsPerTypeCount)]
        public SpotLightDescription[] SpotLights;

        [FieldOffset(48 + 64 * GraphicsSettings.MaxLightsPerTypeCount), MarshalAs(UnmanagedType.ByValArray, SizeConst = GraphicsSettings.MaxLightsPerTypeCount)]
        public PointLightDescription[] PointLights;

        [FieldOffset(48 + 64 * GraphicsSettings.MaxLightsPerTypeCount + 48 * GraphicsSettings.MaxLightsPerTypeCount),
            MarshalAs(UnmanagedType.ByValArray, SizeConst = GraphicsSettings.MaxLightsPerTypeCount)]
        public DirectionalLightDescription[] DirectionalLights;
    }
}
