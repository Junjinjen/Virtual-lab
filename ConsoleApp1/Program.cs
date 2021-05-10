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

namespace App
{
    class TestScript : Script
    {
        TextBox text = new TextBox
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

    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine(new Scene()))
            {
                engine.Run();
            }
        }
    }
}