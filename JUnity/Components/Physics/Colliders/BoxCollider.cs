using JUnity.Services.Graphics.Meshing;
using SharpDX;

namespace JUnity.Components.Physics.Colliders
{
    public class BoxCollider : Collider
    {
        private readonly Vector3 _minimum;
        private readonly Vector3 _maximum;
        private readonly Mesh _mesh;
        private Matrix _matrix = Matrix.Identity;
        private BoundingBox _boundingBox;

        public BoxCollider(string name, Vector3 minimum, Vector3 maximum) : base(name)
        {
            _minimum = minimum;
            _maximum = maximum;
            _boundingBox = new BoundingBox(_minimum, _maximum);
            _mesh = GenerateMesh();
        }

        public override bool Intersects(ref Ray ray, out float distance)
        {
            return _boundingBox.Intersects(ref ray, out distance);
        }

        internal override RenderOrder GenerateRenderOrder()
        {
            return new RenderOrder
            {
                Mesh = _mesh,
                WorldMatrix = _matrix,
                PixelShader = Engine.Instance.GraphicsRenderer.PixelShaders[Engine.Instance.Settings.DefaultPixelShader],
                VertexShader = Engine.Instance.GraphicsRenderer.VertexShaders[Engine.Instance.Settings.DefaultVertexShader],
            };
        }

        internal override void ResolveCollision(Collider other)
        {
            switch (other)
            {
                case BoxCollider otherBox:
                    var collision = _boundingBox.Intersects(otherBox._boundingBox);
                    if (collision)
                    {
                        Rigidbody.FireTriggerEvent(this, otherBox);
                    }
                    break;
                default:
                    break;
            }
        }

        internal override void WorldMatrixChanged(Matrix worldMatrix)
        {
            worldMatrix.Decompose(out var scale, out var _, out var pos);
            _matrix.TranslationVector = pos;
            _matrix.ScaleVector = scale;

            _boundingBox.Minimum = _minimum * scale + pos;
            _boundingBox.Maximum = _maximum * scale + pos;
        }

        private Mesh GenerateMesh()
        {
            var vertices = new[]
            {
                new VertexDescription
                {
                    Color = Color.LimeGreen,
                    Position = new Vector4(_minimum, 1),
                },
                new VertexDescription
                {
                    Color = Color.LimeGreen,
                    Position = new Vector4(_minimum.X, _minimum.Y, _maximum.Z, 1),
                },
                new VertexDescription
                {
                    Color = Color.LimeGreen,
                    Position = new Vector4(_minimum.X, _maximum.Y, _maximum.Z, 1),
                },
                new VertexDescription
                {
                    Color = Color.LimeGreen,
                    Position = new Vector4(_minimum.X, _maximum.Y, _minimum.Z, 1),
                },
                new VertexDescription
                {
                    Color = Color.LimeGreen,
                    Position = new Vector4(_maximum, 1),
                },
                new VertexDescription
                {
                    Color = Color.LimeGreen,
                    Position = new Vector4(_maximum.X, _maximum.Y, _minimum.Z, 1),
                },
                new VertexDescription
                {
                    Color = Color.LimeGreen,
                    Position = new Vector4(_maximum.X, _minimum.Y, _minimum.Z, 1),
                },
                new VertexDescription
                {
                    Color = Color.LimeGreen,
                    Position = new Vector4(_maximum.X, _minimum.Y, _maximum.Z, 1),
                },
            };

            var indeces = new uint[]
            {
                0, 1, 2, 0, 3, 2,
                1, 2, 4, 4, 7, 1,
                5, 4, 7, 5, 7, 6,
                3, 5, 0, 0, 6, 3,
                2, 3, 4, 4, 3, 5,
                0, 1, 7, 0, 7, 6,
            };

            var material = new Material
            {
                CullMode = SharpDX.Direct3D11.CullMode.None,
                FillMode = SharpDX.Direct3D11.FillMode.Wireframe,
            };

            return new Mesh(vertices, indeces, material);
        }
    }
}
