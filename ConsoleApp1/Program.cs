using JUnity;
using JUnity.Services.Graphics;
using JUnity.Utilities;
using SharpDX;

namespace ConsoleApp1
{
    class Init : ISceneInitializer
    {
        public void Seed(GameObjectCollection scene)
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tmp = new Camera();
            tmp.Fov = 90;
            tmp.AspectRatio = 1;
            tmp.Position = new Vector3(1, 3, 5);
            tmp.Rotation = Quaternion.RotationLookAtLH(Vector3.ForwardLH, Vector3.Up);

            using (var engine = new Engine(new Init()))
            {
                engine.Run();
            }
        }
    }
}
