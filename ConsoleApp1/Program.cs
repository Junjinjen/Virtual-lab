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
                engine.Settings.BackgroundColor = SharpDX.Color.Gray;
                engine.Settings.Borderless = true;
                engine.Run();
            }
        }
    }
}