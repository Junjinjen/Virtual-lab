using JUnity;
using JUnity.Components;
using JUnity.Components.Lighning;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Components.Rendering;
using JUnity.Components.UI;
using JUnity.Services.Graphics;
using JUnity.Services.Graphics.Meshing;
using JUnity.Utilities;
using SharpDX;
using SharpDX.DirectInput;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class TestScript : Script
    {
        TextBlock text = new TextBlock
        {
            Width = 0.2f,
            Height = 0.2f
        };
        //RadioButton r2 = new RadioButton("1");
        //RadioButton r3 = new RadioButton("3");

        public TestScript()
        {
        }

        public override void Start()
        {
            Canvas.RegisterElement(text);
            //AddComponent<Rigidbody>();
        }

        public override void FixedUpdate(double deltaTime)
        {
            //System.Console.WriteLine("1");
            //Object.Rotation *= Quaternion.RotationAxis(Vector3.Right, 0.01f);
            //Object.Rotation *= Quaternion.RotationAxis(Vector3.Up, 0.01f);
            var tmp = Object.Position;
            var tmp2 = Object.Rotation;

            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.W))
            {
                tmp.Z += 1;
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.S))
            {
                tmp.Z -= 1;
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.A))
            {
                tmp.X -= 1;
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.D))
            {
                tmp.X += 1;
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.Q))
            {
                tmp.Y += 1;
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.E))
            {
                tmp.Y -= 1;
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.Up))
            {
                tmp2 *= Quaternion.RotationAxis(Vector3.UnitX, MathUtil.DegreesToRadians(10));
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.Down))
            {
                tmp2 *= Quaternion.RotationAxis(Vector3.UnitX, -MathUtil.DegreesToRadians(10));
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.Left))
            {
                tmp2 *= Quaternion.RotationAxis(Vector3.UnitZ, MathUtil.DegreesToRadians(10));
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.Right))
            {
                tmp2 *= Quaternion.RotationAxis(Vector3.UnitZ, -MathUtil.DegreesToRadians(10));
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.Z))
            {
                tmp2 *= Quaternion.RotationAxis(Vector3.UnitY, -MathUtil.DegreesToRadians(10));
            }
            if (Engine.Instance.InputManager.IsKeyJustPressed(Key.X))
            {
                tmp2 *= Quaternion.RotationAxis(Vector3.UnitY, MathUtil.DegreesToRadians(10));
            }

            Object.Position = tmp;
            Object.Rotation = tmp2;
        }
    }

    class Init : ISceneInitializer
    {
        public void Seed(GameObjectCollection scene)
        {
            var prov = new CameraProvider();

            prov.Camera.Fov = MathUtil.DegreesToRadians(90);
            prov.Camera.DrawDistance = 100;
            prov.Camera.NearDistance = 0.001f;
            prov.Camera.Position = new Vector3(0, 0, -20);
            prov.Camera.Rotation = Quaternion.RotationLookAtLH(Vector3.ForwardLH, Vector3.Up);

            //var tmp = MeshLoader.LoadScene(@"Volkswagen.fbx");

            //var go = GameObjectFactory.Create(new FbxObjectCreator(@"3_cubes (3).fbx"));
            //var go = GameObjectFactory.Create(new FbxObjectCreator(@"ZF_YUP.fbx"));

            //var go = new GameObject();
            /*var tmp = MeshLoader.LoadScene(@"personfbx_-Y_Z.fbx");

            go.Children.Add(new GameObject());
            go.Children.Add(new GameObject());
            go.Children.Add(new GameObject());

            go.Children[0].AddComponent<MeshRenderer>().Initialize(tmp[0].NodeMeshes[0], "vx1", "px1");
            go.Children[1].AddComponent<MeshRenderer>().Initialize(tmp[1].NodeMeshes[0], "vx1", "px1");
            go.Children[2].AddComponent<MeshRenderer>().Initialize(tmp[2].NodeMeshes[0], "vx1", "px1");

            go.Children[0].Position = tmp[0].Position;
            go.Children[0].Rotation = tmp[0].Rotation;
            go.Children[0].Scale = tmp[0].Scale;

            go.Children[1].Position = tmp[1].Position;
            go.Children[1].Rotation = tmp[1].Rotation;
            go.Children[1].Scale = tmp[1].Scale;

            go.Children[2].Position = tmp[2].Position;
            go.Children[2].Rotation = tmp[2].Rotation;
            go.Children[2].Scale = tmp[2].Scale;*/
            //personfbx_-Y_Z.fbx
            var go = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(@"virt_ice_scene.fbx"));
            go.Scale = new Vector3(0.1f);
            go.AddScript<TestScript>();
            //go.GetComponent<Rigidbody>().TriggerEnter += (o, x) => System.Console.WriteLine("trigger");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine(new Init()))
            {
                engine.Settings.BackgroundColor = SharpDX.Color.Gray;
                //engine.GraphicsSettings.VSyncEnabled = false;
                //engine.GraphicsSettings.Borderless = true;
                engine.Settings.DrawColliders = true;
                engine.Settings.MultisamplesPerPixel = 4;
                engine.Settings.MultisamplerQuality = -1;
                engine.Run();
            }
        }
    }
}