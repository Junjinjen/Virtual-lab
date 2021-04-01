
using JUnity;
using JUnity.Components;
using JUnity.Components.Lighning;
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
            Width = 0.3f,
            Height = 0.2f,
            Text = "Hello world",
        };

        public fasdf(GameObject obj)
            : base(obj)
        {
            tttt.Click += (o, x) => System.Console.WriteLine("Click");
        }

        public override void Start()
        {
            Canvas.RegisterElement(tttt);
        }

        public override void FixedUpdate(double deltaTime)
        {
            Object.Rotation *= Quaternion.RotationAxis(Vector3.Right, 0.01f);
            Object.Rotation *= Quaternion.RotationAxis(Vector3.Up, 0.01f);
        }
    }

    class Go : IGameObjectCreator
    {
        public GameObject Create()
        {
            var op = 0.4f;
            var vertexes = new VertexDescription[]
            {
                new VertexDescription // front 0
                    {
                        Position = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.25f, 0.25f)
                    },
                    new VertexDescription // front 1
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.25f, 0.5f)
                    },
                    new VertexDescription // front 2
                    {
                        Position = new Vector4(1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.5f, 0.5f)
                    },
                    new VertexDescription // front 3
                    {
                        Position = new Vector4(1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.5f, 0.25f)
                    },
                    new VertexDescription // right 4
                    {
                        Position = new Vector4(1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.5f, 0.25f)
                    },
                    new VertexDescription // right 5
                    {
                        Position = new Vector4(1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.5f, 0.5f)
                    },
                    new VertexDescription // right 6
                    {
                        Position = new Vector4(1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.75f, 0.5f)
                    },
                    new VertexDescription // right 7
                    {
                        Position = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.75f, 0.25f)
                    },
                    new VertexDescription // back 8
                    {
                        Position = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.75f, 0.25f)
                    },
                    new VertexDescription // back 9
                    {
                        Position = new Vector4(1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.75f, 0.5f)
                    },
                    new VertexDescription // back 10
                    {
                        Position = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(1.0f, 0.5f)
                    },
                    new VertexDescription // back 11
                    {
                        Position = new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(1.0f, 0.25f)
                    },
                    new VertexDescription // left 12
                    {
                        Position = new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.0f, 0.25f)
                    },
                    new VertexDescription // left 13
                    {
                        Position = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.0f, 0.5f)
                    },
                    new VertexDescription // left 14
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.25f, 0.5f)
                    },
                    new VertexDescription // left 15
                    {
                        Position = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.25f, 0.25f)
                    },
                    new VertexDescription // top 16
                    {
                        Position = new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.25f, 0.0f)
                    },
                    new VertexDescription // top 17
                    {
                        Position = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.25f, 0.25f)
                    },
                    new VertexDescription // top 18
                    {
                        Position = new Vector4(1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.5f, 0.25f)
                    },
                    new VertexDescription // top 19
                    {
                        Position = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.5f, 0.0f)
                    },
                    new VertexDescription // bottom 20
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.25f, 0.5f)
                    },
                    new VertexDescription // bottom 21
                    {
                        Position = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.25f, 0.75f)
                    },
                    new VertexDescription // bottom 22
                    {
                        Position = new Vector4(1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
                        TextureCoordinate = new Vector2(0.5f, 0.75f)
                    },
                    new VertexDescription // bottom 23
                    {
                        Position = new Vector4(1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 1.0f, op),
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

            return go;
        }
    }

    class Back : IGameObjectCreator
    {
        public GameObject Create()
        {
            var vertexes = new VertexDescription[]
            {
                new VertexDescription // front 0
                    {
                        Position = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Color4(1.0f, 0.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.25f)
                    },
                    new VertexDescription // front 1
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Color4(1.0f, 0.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.5f)
                    },
                    new VertexDescription // front 2
                    {
                        Position = new Vector4(1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Color4(1.0f, 0.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.5f)
                    },
                    new VertexDescription // front 3
                    {
                        Position = new Vector4(1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Color4(1.0f, 0.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.25f)
                    },
                    new VertexDescription // right 4
                    {
                        Position = new Vector4(1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(0.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.25f)
                    },
                    new VertexDescription // right 5
                    {
                        Position = new Vector4(1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(0.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.5f)
                    },
                    new VertexDescription // right 6
                    {
                        Position = new Vector4(1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(0.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.75f, 0.5f)
                    },
                    new VertexDescription // right 7
                    {
                        Position = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(0.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.75f, 0.25f)
                    },
                    new VertexDescription // back 8
                    {
                        Position = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Color4(0.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.75f, 0.25f)
                    },
                    new VertexDescription // back 9
                    {
                        Position = new Vector4(1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Color4(0.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.75f, 0.5f)
                    },
                    new VertexDescription // back 10
                    {
                        Position = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Color4(0.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(1.0f, 0.5f)
                    },
                    new VertexDescription // back 11
                    {
                        Position = new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                        Color = new Color4(0.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(1.0f, 0.25f)
                    },
                    new VertexDescription // left 12
                    {
                        Position = new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.0f, 0.25f)
                    },
                    new VertexDescription // left 13
                    {
                        Position = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.0f, 0.5f)
                    },
                    new VertexDescription // left 14
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.5f)
                    },
                    new VertexDescription // left 15
                    {
                        Position = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 1.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.25f)
                    },
                    new VertexDescription // top 16
                    {
                        Position = new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Color4(0.0f, 1.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.0f)
                    },
                    new VertexDescription // top 17
                    {
                        Position = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Color4(0.0f, 1.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.25f)
                    },
                    new VertexDescription // top 18
                    {
                        Position = new Vector4(1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Color4(0.0f, 1.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.25f)
                    },
                    new VertexDescription // top 19
                    {
                        Position = new Vector4(1.0f, 1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                        Color = new Color4(0.0f, 1.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.0f)
                    },
                    new VertexDescription // bottom 20
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.5f)
                    },
                    new VertexDescription // bottom 21
                    {
                        Position = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.75f)
                    },
                    new VertexDescription // bottom 22
                    {
                        Position = new Vector4(1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 0.0f, 1.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.75f)
                    },
                    new VertexDescription // bottom 23
                    {
                        Position = new Vector4(1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, -1.0f, 0.0f, 1.0f),
                        Color = new Color4(1.0f, 0.0f, 1.0f, 1.0f),
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

            var bmp = new Bitmap("texture.png");
            var buff = new List<SharpDX.Color>();

            for (int j = 0; j < bmp.Height; j++)
            {
                for (int i = 0; i < bmp.Width; i++)
                {
                    var pixel = bmp.GetPixel(i, j);
                    buff.Add(new SharpDX.Color(pixel.R, pixel.G, pixel.B, pixel.A));
                }
            }

            mesh.Material.Texture = new Texture(buff.ToArray(), bmp.Width, bmp.Width);
            //mesh.Material.Texture = new Texture("texture.png");

            var go = new GameObject();

            go.AddComponent<MeshRenderer>().Initialize(mesh, "vx1", "px1");

            return go;
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
            prov.Camera.Position = new Vector3(0, 0, -5);
            prov.Camera.Rotation = Quaternion.RotationLookAtLH(Vector3.ForwardLH, Vector3.Up);

            //var tmp = GameObjectFactory.CreateAndRegister<Back>();
            //tmp.AddScript<fasdf>();
            //tmp.Scale = new Vector3(50);
            //tmp.Position = Vector3.ForwardLH * 100;

            //var inner = GameObjectFactory.CreateAndRegister<Go>();
            //inner.Scale = new Vector3(0.5f, 1.8f, 0.5f);
            //inner.AddScript<fasdf>();

            //var colba = GameObjectFactory.CreateAndRegister<Go>();
            //colba.Scale = new Vector3(1.5f, 2, 1.5f);

            var tmp = MeshLoader.LoadScene(@"teapot.fbx");
            var go = new GameObject();
            go.AddComponent<MeshRenderer>().Initialize(tmp[0].NodeMeshes[1], "vx1", "px1");
            go.Children.Add(new GameObject());
            go.Children[0].AddComponent<MeshRenderer>().Initialize(tmp[0].NodeMeshes[0], "vx1", "px1");
            go.AddScript<fasdf>();

            scene.Add(go);
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
                engine.GraphicsSettings.Borderless = true;
                engine.Run();
            }
        }
    }
}
