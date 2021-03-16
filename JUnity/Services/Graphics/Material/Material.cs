using SharpDX;
using SharpDX.Direct3D11;

namespace JUnity.Services.Graphics.Material
{
    public sealed class Material
    {
        private MaterialDescription _description;
        private Texture _texture;

        public VertexShader VertexShader { get; private set; }

        public void SetVertexShader(string shaderName)
        {
            VertexShader = Engine.Instance.GraphicsRenderer.VertexShaders[shaderName];
        }

        public PixelShader PixelShader { get; private set; }

        public void SetPixelShader(string shaderName)
        {
            PixelShader = Engine.Instance.GraphicsRenderer.PixelShaders[shaderName];
        }

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
