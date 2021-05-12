using JUnity;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine(new Scene()))
            {
                engine.Settings.Borderless = true;
                engine.Run();
            }
        }
    }
}