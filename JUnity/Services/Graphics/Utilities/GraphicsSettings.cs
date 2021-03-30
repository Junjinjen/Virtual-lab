﻿using SharpDX;

namespace JUnity.Services.Graphics.Utilities
{
    public class GraphicsSettings
    {
        internal const int MaxLightsPerTypeCount = 8;

        internal static readonly GraphicsSettings Default = new GraphicsSettings
        {
            WindowTitle = "JUnity window",
            Borderless = false,
            ShadersMetaPath = "Shaders/ShadersMeta.xml",
            MultisamplesPerPixel = 1,
            MultisamplerQuality = 0,
            VSyncEnabled = true,
            GlobalAmbientOcclusion = new Vector3(0.02f),
        };

        public string WindowTitle { get; set; }

        public bool Borderless { get; set; }

        public string ShadersMetaPath { get; set; }

        public int MultisamplesPerPixel { get; set; }

        public int MultisamplerQuality { get; set; }

        public TextureSampling TextureSampling { get; set; }

        public Vector3 GlobalAmbientOcclusion { get; set; }

        public bool VSyncEnabled { get; set; }

        public Color4 BackgroundColor { get; set; }
    }

    public enum TextureSampling
    {
        Point,
        Linear,
        Anisotropic,
    }
}
