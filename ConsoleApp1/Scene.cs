using JUnity;
using JUnity.Components;
using JUnity.Components.Lighning;
using JUnity.Utilities;
using Lab3.Scripts.MutableObjects;
using Lab3.Scripts.UI;
using SharpDX;

namespace Lab3
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

            var obj = new GameObject();
            obj.AddComponent<DirectionLight>();
            obj.GetComponent<DirectionLight>().Direction = new Vector3(0f, -1f, 0.5f);
            obj.GetComponent<DirectionLight>().Direction.Normalize();
            obj.GetComponent<DirectionLight>().Color = Color3.White;
            scene.Add(obj);

            var file = @"Meshes/background.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Background"));
            obj.Position = new Vector3(0.0f, 0.0f, 7.5f);
            obj.Scale *= 6f;

            file = @"Meshes/table.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Table"));
            obj.Position = new Vector3(0.0f, -4.5f, 0.0f);
            obj.Scale *= 2f;

            file = @"Meshes/thermometer.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Thermometer"));
            obj.Position = new Vector3(-3.5f, 0.0f, -5.0f);
            obj.Rotation *= Quaternion.RotationAxis(Vector3.UnitZ, MathUtil.Pi / 6);
            obj.Children[2].AddScript<TemparatureScript>();
            
            file = @"Meshes/voltmeter.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Voltmeter"));
            obj.Position = new Vector3(0.0f, 0.0f, 2.5f);
            obj.Scale *= 1.4f;
            obj.Children[0].AddScript<VoltmeterScript>();

            file = @"Meshes/ammeter.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Ammeter"));
            obj.Position = new Vector3(-4.5f, -2.0f, 2.5f);
            obj.Rotation *= Quaternion.RotationAxis(Vector3.UnitZ, MathUtil.Pi / 8);
            obj.Scale *= 1.4f;

            file = @"Meshes/weigher.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Weigher"));
            obj.Position = new Vector3(3.5f, -4.0f, 0.0f);
            obj.Scale *= 0.5f;

            file = @"Meshes/timer.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Timer"));
            obj.Position = new Vector3(8.5f, -4.0f, 4.0f);

            file = @"Meshes/metal.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Metal"));
            obj.Position = new Vector3(6.5f, -4.0f, 0.0f);
            obj.Scale *= 0.75f;

            file = @"Meshes/device.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Device"));
            obj.Position = new Vector3(-3.5f, -4.0f, 0.0f);
            obj.Scale *= 1.2f;

            file = @"Meshes/heater.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Heater"));
            obj.Position = new Vector3(0.0f, -1.0f, 0.0f);
            obj.Scale *= 1.5f;

            file = @"Meshes/stand.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Stand"));
            obj.Position = new Vector3(0.0f, -4.0f, 0.0f);
            obj.Scale *= 1.5f;

            file = @"Meshes/water.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Water"));
            obj.Position = new Vector3(0.0f, -3.45f, 0.0f);
            obj.Scale *= 1.5f;
            obj.AddScript<WaterScript>();

            file = @"Meshes/flask.fbx";
            obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Flask"));
            obj.Position = new Vector3(0.0f, -4.0f, 0.0f);
            obj.Scale *= 1.5f;

            //file = @"Meshes/water.fbx";
            //obj.Children.Add(GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "WaterCalorimeter")));
            //obj.Children[4].Position = new Vector3(0f, 1.8f, 0f);
            //obj.Children[4].Scale = Vector3.One * 0.9f + Vector3.UnitZ * 5;
            //obj.Children[4].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            //GameObject tmp = obj.Children[1];
            //GameObject tmp2 = obj.Children[2];
            //GameObject tmp3 = obj.Children[4];
            //obj.Children[1] = tmp2;
            //obj.Children[2] = tmp3;
            //obj.Children[4] = tmp;


            //file = @"Meshes/measuringCup2.fbx";
            //obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Cup"));
            //obj.Position = new Vector3(-4.5f, -3f, 0f);
            //obj.Scale = Vector3.One * 1.5f;
            //obj.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi - 0.5f, 0, 0);
            //file = @"Meshes/water.fbx";
            //obj.Children.Add(GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Water")));
            //obj.Children[3].Position = new Vector3(0f, 0f, 0f);
            //obj.Children[3].Scale = Vector3.One + Vector3.UnitZ * 38;
            //obj.Children[3].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            //GameObject tmp4 = obj.Children[0];
            //GameObject tmp5 = obj.Children[2];
            //GameObject tmp6 = obj.Children[3];
            //obj.Children[0] = tmp5;
            //obj.Children[2] = tmp6;
            //obj.Children[3] = tmp4;

            //obj = new GameObject();
            //obj.AddScript<Timer_Script>();
            //scene.Add(obj);

            //obj = new GameObject();
            //obj.AddScript<Main_Script>();
            //scene.Add(obj);

            obj = new GameObject("MainUI");
            obj.AddScript<MainUI>();
            scene.Add(obj);

            obj = new GameObject("WaterUI");
            obj.AddScript<WaterPanelUI>();
            scene.Add(obj);

            obj = new GameObject("MetalUI");
            obj.AddScript<MetalPanelUI>();
            scene.Add(obj);

            obj = new GameObject("ElectricalСircuitUI");
            obj.AddScript<ElectricalСircuitPanelUI>();
            scene.Add(obj);

            obj = new GameObject("TimerUI");
            obj.AddScript<TimerPanelUI>();
            scene.Add(obj);

            //file = @"Meshes/table.fbx";
            //obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Table"));
            //obj.Position = new Vector3(0f, -3f, 0f);
            //obj.Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            //obj.Scale = Vector3.One * 1.5f;

            //file = @"Meshes/thermometer.fbx";
            //obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "WaterThermometer"));
            //obj.Position = new Vector3(-7.2f, -3f, 0f);
            //obj.Scale = Vector3.One / 50f;
            //obj.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi - 0.7f, 0, 0);
            //file = @"Meshes/column.fbx";
            //obj.Children.Add(GameObjectFactory.Create(new FbxObjectCreator(file, "ColumnWater")));
            //obj.Children[3].Position = new Vector3(10f, 80f, 0);
            //obj.Children[3].Scale = Vector3.One * 100f + Vector3.UnitZ * 4600;
            //obj.Children[3].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            ////obj.Script = new WaterScript(33.0f);
            ////obj.AddScript<TestScript>();

            //file = @"Meshes/thermometer.fbx";
            //obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "ObjectThermometer"));
            //obj.Position = new Vector3(2.9f, -3f, 0.0f);
            //obj.Scale = Vector3.One / 50f;
            //obj.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi + 0.2f, 0, 0);
            //file = @"Meshes/column.fbx";
            //obj.Children.Add(GameObjectFactory.Create(new FbxObjectCreator(file, "ColumnObject")));
            //obj.Children[3].Position = new Vector3(10f, 80f, 0);
            //obj.Children[3].Scale = Vector3.One * 100f + Vector3.UnitZ * 4550;
            //obj.Children[3].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            ////obj.Script = new WaterScript(33.0f);
            ////obj.AddScript<TestScript>();

            //file = @"Meshes/object.fbx";
            //obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Object"));
            //obj.Position = new Vector3(1f, -2f, 0f);
            //obj.Scale = Vector3.One * 1.5f;
            //obj.Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);

            //file = @"Meshes/background.fbx";
            //obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Background"));
            //obj.Position = new Vector3(-1f, 1f, 8f);
            //obj.Scale = Vector3.One * 1.7f;
            //obj.Rotation = Quaternion.RotationYawPitchRoll(0, 0, 0);

        }
    }
}
