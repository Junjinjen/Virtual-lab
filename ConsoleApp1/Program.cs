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
            Engine.Instance.GraphicsRenderer.Camera.Fov = 90;
            Engine.Instance.GraphicsRenderer.Camera.AspectRatio = 1;
            Engine.Instance.GraphicsRenderer.Camera.Position = new Vector3(0, 0, -1);
            Engine.Instance.GraphicsRenderer.Camera.Rotation = Quaternion.RotationLookAtLH(Vector3.ForwardLH, Vector3.Up);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine(new Init()))
            {
                engine.Run();
            }
        }
    }
}
