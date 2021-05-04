using JUnity;
using JUnity.Components;
using JUnity.Services.Input;
using JUnity.Utilities;
using Lab2.GameObjects;
using Lab2.Scripts;
using SharpDX;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class WaterScript : Script
    {
        private const float MAX_VOLUME = 6f;
        private float _maxSizeZ;

        public WaterScript(float maxSizeZ)
        {
            _maxSizeZ = maxSizeZ;
        }

        public override void Start()
        {
            var ui_script = (UI_Script)Scene["UI"].Script;
            ui_script.V_b.ValueChanged += ResizeWater;
            ui_script.V_b.ValueChanged += (o, x) => Console.WriteLine(x.Value);

            ResizeWater(null, new JUnity.Components.UI.FloatTextBoxValueChangedEventArgs(ui_script.V_b.Value));
        }

        private void ResizeWater(object sender, JUnity.Components.UI.FloatTextBoxValueChangedEventArgs e)
        {
            var value = e.Value;
            if (value > MAX_VOLUME)
            {
                value = MAX_VOLUME;
            }
            else if (value < 0)
            {
                value = 0;
            }
            else
            {
                // No action required
            }

            var scale = Object.Scale;
            scale.Z = _maxSizeZ * value / MAX_VOLUME;
            Object.Scale = scale;
        }
    }

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

            var file = @"Meshes/test_water.fbx";
            var go = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file));
            go.Position = new Vector3(-4.2f, -2.5f, 0.0f);
            go.Scale = Vector3.One * 1.5f;
            go.Script = new WaterScript(33.0f);

            file = @"Meshes/virt_scene.fbx";
            go = GameObjectFactory.CreateAndRegister( new FbxObjectCreator(file));
            go.Position = Vector3.Down * 3;
            go.Scale = Vector3.One * 1.5f;
            go.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi, 0, 0);

            GameObjectFactory.CreateAndRegister(new UI_GameObject());
        }
    }
}
