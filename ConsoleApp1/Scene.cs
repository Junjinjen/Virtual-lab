using JUnity;
using JUnity.Components;
using JUnity.Utilities;
using Lab2.GameObjects;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            camera.Camera.Position = new Vector3(0, 0, -20);
            camera.Camera.Rotation = Quaternion.RotationLookAtLH(Vector3.ForwardLH, Vector3.Up);

            GameObjectFactory.CreateAndRegister(new UI_GameObject());
        }
    }
}
