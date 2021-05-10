using JUnity;
using JUnity.Components;
using JUnity.Utilities;
using Lab2.GameObjects;
using Lab2.Scripts;
using SharpDX;

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
            //var ui_script = (UI_Script)Scene["UI"].Script;
            //ui_script.Water_volume.ValueChanged += ResizeWater;
            //ui_script.Water_volume.ValueChanged += (o, x) => Console.WriteLine(x.Value);

            //ResizeWater(null, new JUnity.Components.UI.FloatTextBoxValueChangedEventArgs(ui_script.Water_volume.Value));
        }

        //private void ResizeWater(object sender, JUnity.Components.UI.FloatTextBoxValueChangedEventArgs e)
        //{
        //    var value = e.Value;
        //    if (value > MAX_VOLUME)
        //    {
        //        value = MAX_VOLUME;
        //    }
        //    else if (value < 0)
        //    {
        //        value = 0;
        //    }
        //    else
        //    {
        //        // No action required
        //    }

        //    var scale = Object.Scale;
        //    scale.Z = _maxSizeZ * value / MAX_VOLUME;
        //    Object.Scale = scale;
        //}
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

            var file = @"Meshes/calorimeter2.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Calorimeter"));
            obj.Position = new Vector3(-1.5f, -3f, 0.0f);
            obj.Scale = Vector3.One * 1.5f;
  
            file = @"Meshes/water.fbx";
            obj.Children.Add(GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "WaterCalorimeter")));
            obj.Children[4].Position = new Vector3(0f, 5f, 0f);
            obj.Children[4].Scale = Vector3.One * 0.9f + Vector3.UnitZ * 30;
            obj.Children[4].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            GameObject tmp = obj.Children[1];
            GameObject tmp2 = obj.Children[2];
            GameObject tmp3 = obj.Children[4];
            obj.Children[1] = tmp2;
            obj.Children[2] = tmp3;
            obj.Children[4] = tmp;


            file = @"Meshes/measuringCup2.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Cup"));
            obj.Position = new Vector3(-4.5f, -3f, 0f);
            obj.Scale = Vector3.One * 1.5f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi, 0, 0);
            file = @"Meshes/water.fbx";
            obj.Children.Add(GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Water")));
            obj.Children[3].Position = new Vector3(0f, 0f, 0f);
            obj.Children[3].Scale = Vector3.One + Vector3.UnitZ * 38;
            obj.Children[3].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            obj.Script = new WaterScript(33.0f);

            obj = new GameObject();
            obj.AddScript<Timer_Script>();
            scene.Add(obj);

            obj = new GameObject();
            obj.AddScript<Main_Script>();
            scene.Add(obj);

            obj = new GameObject("UI");
            obj.AddScript<UI_Script>();
            scene.Add(obj);

            file = @"Meshes/table.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Table"));
            obj.Position = new Vector3(0f, -3f, 0f);
            obj.Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            obj.Scale = Vector3.One * 1.5f;

            file = @"Meshes/thermometer.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "WaterThermometer"));
            obj.Position = new Vector3(-7.2f, -3f, 0f);
            obj.Scale = Vector3.One / 50f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi - 0.7f, 0, 0);
            file = @"Meshes/column.fbx";
            obj.Children.Add(GameObjectFactory.Create(new FbxObjectCreator(file, "ColumnWater")));
            obj.Children[3].Position = new Vector3(10f, 80f, 0);
            obj.Children[3].Scale = Vector3.One * 100f + Vector3.UnitZ * 4600;
            obj.Children[3].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            //obj.Script = new WaterScript(33.0f);
            //obj.AddScript<TestScript>();

            file = @"Meshes/thermometer.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "ObjectThermometer"));
            obj.Position = new Vector3(2.9f, -3f, 0.0f);
            obj.Scale = Vector3.One / 50f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi + 0.2f, 0, 0);
            file = @"Meshes/column.fbx";
            obj.Children.Add(GameObjectFactory.Create(new FbxObjectCreator(file, "ColumnObject")));
            obj.Children[3].Position = new Vector3(10f, 80f, 0);
            obj.Children[3].Scale = Vector3.One * 100f + Vector3.UnitZ * 4550;
            obj.Children[3].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            //obj.Script = new WaterScript(33.0f);
            //obj.AddScript<TestScript>();

            file = @"Meshes/object.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Object"));
            obj.Position = new Vector3(1f, -2f, 0f);
            obj.Scale = Vector3.One * 1.5f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);

            file = @"Meshes/background.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Background"));
            obj.Position = new Vector3(-1f, 1f, 8f);
            obj.Scale = Vector3.One * 1.7f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(0, 0, 0);

        }
    }
}
