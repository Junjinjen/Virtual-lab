namespace JUnity.Services.Graphics
{
    public class GraphicsSettings
    {
        internal static readonly GraphicsSettings Default = new GraphicsSettings
        {
            WindowTitle = "JUnity game",
            IsWindowed = true,
            Numerator = 60,
            Denominator = 1,
            ShadersMetaPath = "Shaders/ShadersMeta.xml",
            MultisamplesPerPixel = 1,
            MultisamplerQuality = 0,
        };

        public string WindowTitle { get; set; }

        public bool IsWindowed { get; set; }

        public int Numerator { get; set; }

        public int Denominator { get; set; }

        public string ShadersMetaPath { get; set; }

        public int MultisamplesPerPixel { get; set; }

        public int MultisamplerQuality { get; set; }

        public TextureSampling TextureSampling { get; set; }
    }

    public enum TextureSampling
    {
        Point,
        Linear,
        Anisotropic,
    }
}
