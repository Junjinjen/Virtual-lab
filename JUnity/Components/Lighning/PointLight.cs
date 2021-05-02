using JUnity.Services.Graphics.Lightning;
using SharpDX;

namespace JUnity.Components.Lighning
{
    public class PointLight : GameComponent
    {
        private PointLightDescription _pointLightStruct;

        internal PointLight(GameObject owner)
            : base(owner)
        {
            Activated = true;
        }

        public bool Activated { get; set; }

        public Vector3 Offset { get => _pointLightStruct.Position - Owner.Position; set => _pointLightStruct.Position = Owner.Position + value; }

        public Color3 Color { get => _pointLightStruct.Color; set => _pointLightStruct.Color = value; }

        public Vector3 Attenuation { get => _pointLightStruct.Attenuation; set => _pointLightStruct.Attenuation = value; }

        internal override void CallComponent(double deltaTime)
        {
            if (Activated)
            {
                Engine.Instance.GraphicsRenderer.LightManager.AddPointLight(_pointLightStruct);
            }
        }
    }
}
