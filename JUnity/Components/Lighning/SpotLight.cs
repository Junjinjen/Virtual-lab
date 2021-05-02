using JUnity.Services.Graphics.Lightning;
using SharpDX;

namespace JUnity.Components.Lighning
{
    public class SpotLight : GameComponent
    {
        private SpotLightDescription _spotLightDescription;

        internal SpotLight(GameObject owner)
            : base(owner)
        {
            Activated = true;
        }

        public bool Activated { get; set; }

        public Vector3 Offset { get => _spotLightDescription.Position - Owner.Position; set => _spotLightDescription.Position = Owner.Position + value; }

        public Color3 Color { get => _spotLightDescription.Color; set => _spotLightDescription.Color = value; }

        public Vector3 Attenuation { get => _spotLightDescription.Attenuation; set => _spotLightDescription.Attenuation = value; }

        public Vector3 Direction { get => _spotLightDescription.Direction; set => _spotLightDescription.Direction = value; }

        public float Angle { get => _spotLightDescription.Angle; set => _spotLightDescription.Angle = value; }

        internal override void CallComponent(double deltaTime)
        {
            if (Activated)
            {
                Engine.Instance.GraphicsRenderer.LightManager.AddSpotLight(_spotLightDescription);
            }
        }
    }
}
