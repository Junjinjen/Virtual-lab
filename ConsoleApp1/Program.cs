using JUnity;
using JUnity.Components;
using JUnity.Components.Rendering;
using JUnity.Services.Graphics;
using JUnity.Services.Graphics.Meshing;
using JUnity.Services.UI.Elements;
using JUnity.Utilities;
using SharpDX;
using System.Collections.Generic;
using System.Drawing;

namespace ConsoleApp1
{
    class fasdf : Script
    {
        Button tttt = new Button
        {
            Width = 0.5f,
            Height = 0.5f,
            Text = "Hello world",
        };

        Button tttt1 = new Button
        {
            Position = new Vector2(0.1f, 0.1f),
            Width = 0.5f,
            Height = 0.5f,
            Text = "Hello world",
        };

        public fasdf(GameObject obj)
            : base(obj)
        {
            tttt.Click += (o, x) => System.Console.WriteLine("Click");
            tttt1.Click += (o, x) => System.Console.WriteLine("Click1");
        }

        public override void Start()
        {
            Canvas.RegisterElement(tttt);
            Canvas.RegisterElement(tttt1);
        }

        public override void Update(double deltaTime)
        {
            Engine.Instance.GraphicsRenderer.LightManager.AddDirectionalLight(new JUnity.Services.Graphics.Lightning.DirectionalLight
            {
                Color = new Vector3(1f, 1f, 1f),
                Direction = new Vector3(1f)
            });
        }

        public override void FixedUpdate(double deltaTime)
        {
            Object.Rotation *= Quaternion.RotationAxis(Vector3.Right, 0.01f);
            Object.Rotation *= Quaternion.RotationAxis(Vector3.Up, 0.005f);
        }
    }

    class Go : IGameObjectCreator
    {
        public GameObject Create()
        {
            var vertexes = new VertexDescription[]
            {
                new VertexDescription // front 0
                    {
                        Position = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 0.0f, 0.5f),
                        TextureCoordinate = new Vector2(0.25f, 0.25f)
                    },
                    new VertexDescription // front 1
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 0.0f, 0.5f),
                        TextureCoordinate = new Vector2(0.25f, 0.5f)
                    },
                    new VertexDescription // front 2
                    {
                        Position = new Vector4(1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 0.0f, 0.5f),
                        TextureCoordinate = new Vector2(0.5f, 0.5f)
                    },
                    new VertexDescription // front 3
                    {
                        Position = new Vector4(1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 0.0f, 0.5f),
                        TextureCoordinate = new Vector2(0.5f, 0.25f)
                    },
                    new VertexDescription // right 4
                    {
                        Position = new Vector4(1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.25f)
                    },
                    new VertexDescription // right 5
                    {
                        Position = new Vector4(1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.5f)
                    },
                    new VertexDescription // right 6
                    {
                        Position = new Vector4(1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.75f, 0.5f)
                    },
                    new VertexDescription // right 7
                    {
                        Position = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.75f, 0.25f)
                    },
                    new VertexDescription // back 8
                    {
                        Position = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.75f, 0.25f)
                    },
                    new VertexDescription // back 9
                    {
                        Position = new Vector4(1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.75f, 0.5f)
                    },
                    new VertexDescription // back 10
                    {
                        Position = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(1.0f, 0.5f)
                    },
                    new VertexDescription // back 11
                    {
                        Position = new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(1.0f, 0.25f)
                    },
                    new VertexDescription // left 12
                    {
                        Position = new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.0f, 0.25f)
                    },
                    new VertexDescription // left 13
                    {
                        Position = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.0f, 0.5f)
                    },
                    new VertexDescription // left 14
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.5f)
                    },
                    new VertexDescription // left 15
                    {
                        Position = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.25f)
                    },
                    new VertexDescription // top 16
                    {
                        Position = new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.0f)
                    },
                    new VertexDescription // top 17
                    {
                        Position = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.25f)
                    },
                    new VertexDescription // top 18
                    {
                        Position = new Vector4(1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.25f)
                    },
                    new VertexDescription // top 19
                    {
                        Position = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.0f)
                    },
                    new VertexDescription // bottom 20
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.5f)
                    },
                    new VertexDescription // bottom 21
                    {
                        Position = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.75f)
                    },
                    new VertexDescription // bottom 22
                    {
                        Position = new Vector4(1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.75f)
                    },
                    new VertexDescription // bottom 23
                    {
                        Position = new Vector4(1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.5f)
                    }
            };
            var indeces = new uint[]
            {
                8, 9, 10,       10, 11, 8,
                12, 13, 14,     14, 15, 12,
                20, 21, 22,     22, 23, 20,
                0, 1, 2,        2, 3, 0,
                4, 5, 6,        6, 7, 4,
                16, 17, 18,     18, 19, 16
            };

            var mesh = new Mesh(vertexes, indeces, new Material());
            //mesh.Material.CullMode = SharpDX.Direct3D11.CullMode.None;

            //var bmp = new Bitmap("texture.png");
            //var buff = new List<SharpDX.Color>();

            //for (int j = 0; j < bmp.Height; j++)
            //{
            //    for (int i = 0; i < bmp.Width; i++)
            //    {
            //        var pixel = bmp.GetPixel(i, j);
            //        buff.Add(new SharpDX.Color
            //        {
            //            A = pixel.A,
            //            B = pixel.B,
            //            G = pixel.G,
            //            R = pixel.R,
            //        });
            //    }
            //}

            //mesh.Material.Texture = new Texture(buff.ToArray(), bmp.Width, bmp.Width);
            mesh.Material.Texture = new Texture("texture.png");

            mesh.Material.FillMode = SharpDX.Direct3D11.FillMode.Solid;
            mesh.Material.CullMode = SharpDX.Direct3D11.CullMode.None;

            var go = new GameObject();
            go.AddComponent<MeshRenderer>().Initialize(mesh, "vx1", "px1");
            go.AddScript<fasdf>();

            return go;
        }
    }

    class Init : ISceneInitializer
    {
        public void Seed(GameObjectCollection scene)
        {
            Engine.Instance.GraphicsRenderer.Camera.Fov = MathUtil.DegreesToRadians(90);
            Engine.Instance.GraphicsRenderer.Camera.DrawDistance = 100;
            Engine.Instance.GraphicsRenderer.Camera.NearDistance = 0.001f;
            Engine.Instance.GraphicsRenderer.Camera.Position = new Vector3(0, 0, -5);
            Engine.Instance.GraphicsRenderer.Camera.Rotation = Quaternion.RotationLookAtLH(Vector3.ForwardLH, Vector3.Up);

            GameObjectFactory.CreateAndRegister<Go>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine(new Init()))
            {
                engine.GraphicsSettings.BackgroundColor = SharpDX.Color.Gray;
                //engine.GraphicsSettings.VSyncEnabled = false;
                engine.Run();
            }
        }
    }
}
