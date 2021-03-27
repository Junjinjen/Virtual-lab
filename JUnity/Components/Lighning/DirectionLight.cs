using JUnity.Services.Graphics.Lightning;
using SharpDX;

namespace JUnity.Components.Lighning
{
    public class DirectionLight : GameComponent
    {
        private DirectionalLightDescription _directionLightStruct;

        internal DirectionLight(GameObject owner)
            : base(owner)
        {
            Activated = true;
        }

        public bool Activated { get; set; }

        public Color3 Color { get => _directionLightStruct.Color; set => _directionLightStruct.Color = value; }

        public Vector3 Direction { get => _directionLightStruct.Direction; set => _directionLightStruct.Direction = value; }

        internal override void CallComponent(double deltaTime)
        {
            if (Activated)
            {
                Engine.Instance.GraphicsRenderer.LightManager.AddDirectionalLight(_directionLightStruct);
            }
        }
    }
}
