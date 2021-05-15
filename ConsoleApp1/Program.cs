using JUnity;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine(new Scene()))
            {
                engine.Settings.BackgroundColor = SharpDX.Color.Gray;
                engine.Settings.MultisamplerQuality = -1;
                engine.Settings.MultisamplesPerPixel = 8;
                engine.Settings.VSyncEnabled = true;
                engine.Settings.Borderless = true;
                engine.Settings.DrawColliders = false;
                engine.Run();
            }
        }
    }
}