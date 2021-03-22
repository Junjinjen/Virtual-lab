using JUnity;
using JUnity.Services.Graphics;
using JUnity.Services.Graphics.Meshing;
using JUnity.Utilities;
using SharpDX;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApp1
{
    class Init : ISceneInitializer
    {
        public void Seed(GameObjectCollection scene)
        {
            Engine.Instance.GraphicsRenderer.Camera.Fov = MathUtil.PiOverFour;
            Engine.Instance.GraphicsRenderer.Camera.AspectRatio = 1;
            Engine.Instance.GraphicsRenderer.Camera.Position = new Vector3(0, 0, -5);
            Engine.Instance.GraphicsRenderer.Camera.Rotation = Quaternion.RotationLookAtLH(Vector3.ForwardLH, Vector3.Up);

            var vertexes = new VertexDescription[]
            {
                new VertexDescription // front 0
                    {
                        Position = new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.25f)
                    },
                    new VertexDescription // front 1
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.25f, 0.5f)
                    },
                    new VertexDescription // front 2
                    {
                        Position = new Vector4(1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                        TextureCoordinate = new Vector2(0.5f, 0.5f)
                    },
                    new VertexDescription // front 3
                    {
                        Position = new Vector4(1.0f, 1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(0.0f, 0.0f, -1.0f, 1.0f),
                        Color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
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
                        Color = new Vector4(1.0f, 1.0f, 0.0f, 0.5f),
                        TextureCoordinate = new Vector2(0.0f, 0.25f)
                    },
                    new VertexDescription // left 13
                    {
                        Position = new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Vector4(1.0f, 1.0f, 0.0f, 0.5f),
                        TextureCoordinate = new Vector2(0.0f, 0.5f)
                    },
                    new VertexDescription // left 14
                    {
                        Position = new Vector4(-1.0f, -1.0f, -1.0f, 1.0f),
                        Normal = new Vector4(-1.0f, 0.0f, 0.0f, 1.0f),
                        Color = new Vector4(1.0f, 1.0f, 0.0f, 0.5f),
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

            mesh.Material.SetPixelShader("px1");
            mesh.Material.SetVertexShader("vx1");

            var bmp = new Bitmap("texture.png");
            var rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            if (bmp.PixelFormat != PixelFormat.Format32bppArgb)
            {
                bmp = bmp.Clone(rect, PixelFormat.Format32bppArgb);
            }
            var data = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            mesh.Material.Texture = new Texture(data);
            bmp.UnlockBits(data);

            var go = new GameObject();
            go.Rotation = Quaternion.RotationLookAtLH(new Vector3(0.5f), Vector3.Up);
            Engine.Instance.GraphicsRenderer.AddMeshToDrawingQueue(new RenderOrder
            {
                GameObject = go,
                Mesh = mesh,
            });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine(new Init()))
            {
                engine.Run();
            }
        }
    }
}
