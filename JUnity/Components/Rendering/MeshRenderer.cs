using System;
using JUnity.Components.Interfaces;
using JUnity.Services.Graphics.Meshing;
using SharpDX;
using SharpDX.Direct3D11;

namespace JUnity.Components.Rendering
{
    public sealed class MeshRenderer : GameComponent, IUniqueComponent
    {
        private Mesh _mesh;
        private VertexShader _vertexShader;
        private PixelShader _pixelShader;

        internal MeshRenderer(GameObject owner)
            : base(owner)
        {
            Active = true;
        }

        public bool Active { get; set; }

        public Vector3 Scale { get => _mesh.Scale; set => _mesh.Scale = value; }

        public Material Material { get => _mesh.Material; set => _mesh.Material = value; }

        public void SetMesh(Mesh mesh)
        {
            _mesh = mesh;
        }

        public void SetPixelShader(string shaderName)
        {
            _pixelShader = Engine.Instance.GraphicsRenderer.PixelShaders[shaderName];
        }

        public void SetVertexShader(string shaderName)
        {
            _vertexShader = Engine.Instance.GraphicsRenderer.VertexShaders[shaderName];
        }

        public void Initialize(Mesh mesh, string vertexShaderName, string pixelShaderName)
        {
            SetMesh(mesh);
            SetVertexShader(vertexShaderName);
            SetPixelShader(pixelShaderName);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S112:General exceptions should never be thrown",
            Justification = "Most suitable exception class")]
        internal override void Start()
        {
            if (_mesh == null || _pixelShader == null || _vertexShader == null)
            {
                throw new NullReferenceException("Some of mesh renderer fields were null.");
            }
        }

        internal override void CallComponent(double deltaTime)
        {
            if (Active)
            {
                var order = new RenderOrder
                {
                    Mesh = _mesh,
                    GameObject = Owner,
                    PixelShader = _pixelShader,
                    VertexShader = _vertexShader,
                };

                Engine.Instance.GraphicsRenderer.AddRenderOrder(order);
            }
        }
    }
}
