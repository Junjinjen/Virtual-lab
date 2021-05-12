using JUnity;
using JUnity.Components;
using JUnity.Components.Lighning;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Components.Rendering;
using JUnity.Components.UI;
using JUnity.Services.Graphics;
namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine(new Scene()))
            {
                engine.Settings.BackgroundColor = SharpDX.Color.Gray;
                engine.Settings.MultisamplerQuality = 4;
                engine.Settings.MultisamplesPerPixel = 8;
                //engine.GraphicsSettings.VSyncEnabled = false;
                //engine.GraphicsSettings.Borderless = true;
                engine.Settings.DrawColliders = true;
                engine.Settings.MultisamplesPerPixel = 4;
                engine.Settings.MultisamplerQuality = -1;
                engine.Settings.Borderless = true;
                engine.Run();
            }
        }
    }
}