using App.Objects;
using App.Scripts;
using JUnity;
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

            var gameObjectUI = new GameObject("UI");
            gameObjectUI.AddScript<UI>();
            scene.Add(gameObjectUI);

            GameObjectFactory.CreateAndRegister(new Tools());

        }
    }
}
