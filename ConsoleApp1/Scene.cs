using JUnity.Components;
using JUnity.Services.Input;
using JUnity.Utilities;
using Lab2.GameObjects;
using SharpDX;

namespace Lab2
{
    public class Scene : ISceneInitializer
    {
        public void Seed(GameObjectCollection scene)
        {
            var camera = new CameraProvider();

            camera.Camera.Fov = MathUtil.DegreesToRadians(90);
            camera.Camera.DrawDistance = 100;
            camera.Camera.NearDistance = 0.001f;
            camera.Camera.Position = new Vector3(0, 0, -10);
            camera.Camera.Rotation = Quaternion.RotationLookAtLH(Vector3.ForwardLH, Vector3.Up);

            MouseGrip.SetCameraProvider(camera);

            GameObjectFactory.CreateAndRegister(new SolidBody());
            GameObjectFactory.CreateAndRegister(new Calorimeter());
            GameObjectFactory.CreateAndRegister(new Fire());
            GameObjectFactory.CreateAndRegister(new MeasuringCup());
            GameObjectFactory.CreateAndRegister(new Table());
            GameObjectFactory.CreateAndRegister(new WaterThermometer());
            GameObjectFactory.CreateAndRegister(new ObjectThermometer());
            GameObjectFactory.CreateAndRegister(new Background());

            GameObjectFactory.CreateAndRegister(new UI_Object());
            GameObjectFactory.CreateAndRegister(new Main_Object());
            GameObjectFactory.CreateAndRegister(new Timer_Object());
        }
    }
}
