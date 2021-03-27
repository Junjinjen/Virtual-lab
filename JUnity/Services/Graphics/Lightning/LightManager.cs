using JUnity.Services.Graphics.Utilities;
using SharpDX;
using System;

namespace JUnity.Services.Graphics.Lightning
{
    internal class LightManager
    {
        private LightContainer _lightContainer = new LightContainer();

        public LightManager()
        {
            _lightContainer.DirectionalLights = new DirectionalLightDescription[GraphicsSettings.MaxLightsPerTypeCount];
            _lightContainer.PointLights = new PointLightDescription[GraphicsSettings.MaxLightsPerTypeCount];
            _lightContainer.SpotLights = new SpotLightDescription[GraphicsSettings.MaxLightsPerTypeCount];
        }

        public Vector3 CameraPosition
        {
            get
            {
                return _lightContainer.CameraPosition;
            }
            set
            {
                _lightContainer.CameraPosition = value;
            }
        }

        public Vector3 GlobalAmbient
        {
            get
            {
                return _lightContainer.GlobalAmbient;
            }
            set
            {
                _lightContainer.GlobalAmbient = value;
            }
        }

        public void AddDirectionalLight(DirectionalLightDescription light)
        {
            if (_lightContainer.ActiveDirectionalLights + 1 > GraphicsSettings.MaxLightsPerTypeCount)
            {
                throw new ArgumentOutOfRangeException(nameof(light), "Unable to add direction light. Light limit reached.");
            }

            _lightContainer.DirectionalLights[_lightContainer.ActiveDirectionalLights++] = light;
        }

        public void AddPointLight(PointLightDescription light)
        {
            if (_lightContainer.ActivePointLights + 1 > GraphicsSettings.MaxLightsPerTypeCount)
            {
                throw new ArgumentOutOfRangeException(nameof(light), "Unable to add point light. Light limit reached.");
            }

            _lightContainer.PointLights[_lightContainer.ActivePointLights++] = light;
        }

        public void AddSpotLight(SpotLightDescription light)
        {
            if (_lightContainer.ActiveSpotLights + 1 > GraphicsSettings.MaxLightsPerTypeCount)
            {
                throw new ArgumentOutOfRangeException(nameof(light), "Unable to add spot light. Light limit reached.");
            }

            _lightContainer.SpotLights[_lightContainer.ActiveSpotLights++] = light;
        }

        public void ResetLight()
        {
            _lightContainer.ActiveDirectionalLights = 0;
            _lightContainer.ActivePointLights = 0;
            _lightContainer.ActiveSpotLights = 0;
        }

        public LightContainer GetContainer()
        {
            return _lightContainer;
        }
    }
}
