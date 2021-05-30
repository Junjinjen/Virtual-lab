using JUnity;
using System;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine(new Scene()))
            {
                //engine.Settings.MultisamplerQuality = -1;
                //engine.Settings.MultisamplesPerPixel = 4;
                engine.Settings.BackgroundColor = SharpDX.Color.Gray;
                engine.Settings.Borderless = true;
                engine.Run();
            }
        }
    }
}