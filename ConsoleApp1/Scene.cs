using App.Objects;
using JUnity.Components;
using JUnity.Utilities;
using SharpDX;

namespace App
{
    public class Scene : ISceneInitializer
    {

        public void Seed(GameObjectCollection scene)
        {
            var prov = new CameraProvider();

            prov.Camera.Fov = MathUtil.DegreesToRadians(90);
            prov.Camera.DrawDistance = 100;
            prov.Camera.NearDistance = 0.001f;
            prov.Camera.Position = new Vector3(0, 0, -20);
            prov.Camera.Rotation = Quaternion.RotationLookAtLH(Vector3.ForwardLH, Vector3.Up);

            GameObjectFactory.CreateAndRegister(new ObjectUI());
            GameObjectFactory.CreateAndRegister(new TimerObject());
            GameObjectFactory.CreateAndRegister(new Tools());
            GameObjectFactory.CreateAndRegister(new Table());
            GameObjectFactory.CreateAndRegister(new Wall());
            GameObjectFactory.CreateAndRegister(new DirectionLightObject());

            GameObjectFactory.CreateAndRegister(new SoundObject("Sounds/fon.wav", "music"));
            GameObjectFactory.CreateAndRegister(new SoundObject("Sounds/dropInWater.wav", "drop"));
            GameObjectFactory.CreateAndRegister(new SoundObject("Sounds/rotate.wav", "rotate"));
            GameObjectFactory.CreateAndRegister(new SoundObject("Sounds/up.wav", "up"));
        }
    }
}
