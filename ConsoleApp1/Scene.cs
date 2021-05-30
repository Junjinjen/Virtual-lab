using JUnity.Components;
using JUnity.Services.Input;
using JUnity.Utilities;
using Lab3.GameObjects;
using Lab3.GameObjects.Items;
using Lab3.GameObjects.UI;
using SharpDX;
using System;

namespace Lab3
{
    public class Scene : ISceneInitializer
    {
        public static Random Random { get; private set; } = new Random();

        public void Seed(GameObjectCollection scene)
        {
            var camera = new CameraProvider();

            camera.Camera.Fov = MathUtil.DegreesToRadians(90);
            camera.Camera.DrawDistance = 100;
            camera.Camera.NearDistance = 0.001f;
            camera.Camera.Position = new Vector3(0, 0, -10);
            camera.Camera.Rotation = Quaternion.RotationLookAtLH(Vector3.ForwardLH, Vector3.Up);

            MouseGrip.SetCameraProvider(camera);

            GameObjectFactory.CreateAndRegister(new LightObject());

            GameObjectFactory.CreateAndRegister(new Background());
            GameObjectFactory.CreateAndRegister(new Table());

            GameObjectFactory.CreateAndRegister(new Thermometer());
            GameObjectFactory.CreateAndRegister(new Voltmeter());
            GameObjectFactory.CreateAndRegister(new Ammeter());
            GameObjectFactory.CreateAndRegister(new Timer());
            GameObjectFactory.CreateAndRegister(new Metal());
            GameObjectFactory.CreateAndRegister(new Weigher());
            GameObjectFactory.CreateAndRegister(new Device());
            GameObjectFactory.CreateAndRegister(new Heater());
            GameObjectFactory.CreateAndRegister(new Stand());
            GameObjectFactory.CreateAndRegister(new Water());
            GameObjectFactory.CreateAndRegister(new Flask());

            GameObjectFactory.CreateAndRegister(new MainUI());
            GameObjectFactory.CreateAndRegister(new WaterUI());
            GameObjectFactory.CreateAndRegister(new MetalUI());
            GameObjectFactory.CreateAndRegister(new ElectricalСircuitUI());
            GameObjectFactory.CreateAndRegister(new TimerUI());
        }
    }
}
