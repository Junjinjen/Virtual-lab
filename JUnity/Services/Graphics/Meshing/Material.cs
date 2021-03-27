using SharpDX;
using SharpDX.Direct3D11;

namespace JUnity.Services.Graphics.Meshing
{
    public sealed class Material
    {
        private RasterizerStateDescription _rasterizerStateDescription;
        private MaterialDescription _description;
        private Texture _texture;

        public Material()
        {
            _rasterizerStateDescription = new RasterizerStateDescription
            {
                CullMode = CullMode.Back,
                FillMode = FillMode.Solid,
                IsFrontCounterClockwise = true,
                IsMultisampleEnabled = true,
                IsAntialiasedLineEnabled = true,
                IsDepthClipEnabled = true,
            };
        }

        internal MaterialDescription Description { get => _description; }

        internal RasterizerStateDescription RasterizerState { get => _rasterizerStateDescription; }

        public CullMode CullMode { get => _rasterizerStateDescription.CullMode; set => _rasterizerStateDescription.CullMode = value; }

        public FillMode FillMode { get => _rasterizerStateDescription.FillMode; set => _rasterizerStateDescription.FillMode = value; }

        public Texture Texture
        {
            get => _texture;
            set
            {
                if (value != null)
                {
                    _description.IsTexturePresent = true;
                    _texture = value;
                }
                else if (_texture != null)
                {
                    _texture = null;
                }
            }
        }

        public Vector3 EmissivityCoefficient
        {
            get => _description.EmissivityCoefficient;
            set => _description.EmissivityCoefficient = value;
        }

        public Vector3 AmbientCoefficient
        {
            get => _description.AmbientCoefficient;
            set => _description.AmbientCoefficient = value;
        }

        public Vector3 DiffusionCoefficient
        {
            get => _description.DiffusionCoefficient;
            set => _description.DiffusionCoefficient = value;
        }

        public Vector3 SpecularCoefficient
        {
            get => _description.SpecularCoefficient;
            set => _description.SpecularCoefficient = value;
        }

        public float SpecularPower
        {
            get => _description.SpecularPower;
            set => _description.SpecularPower = value;
        }
    }
}
