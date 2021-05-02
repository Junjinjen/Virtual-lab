using SharpDX.Direct3D11;
using System.Collections.Generic;

namespace JUnity.Services.Graphics.Meshing
{
    internal class RasterizerStateFactory
    {
        private readonly Device _device;
        private readonly Dictionary<RasterizerStateDescription, RasterizerState> _rasterizerStates = new Dictionary<RasterizerStateDescription, RasterizerState>();

        public RasterizerStateFactory(Device device)
        {
            _device = device;
        }

        public RasterizerState Create(RasterizerStateDescription rasterizerStateDescription)
        {
            if (!_rasterizerStates.TryGetValue(rasterizerStateDescription, out var rasterizerState))
            {
                rasterizerState = new RasterizerState(_device, rasterizerStateDescription);
                _rasterizerStates.Add(rasterizerStateDescription, rasterizerState);
                return rasterizerState;
            }

            return rasterizerState;
        }
    }
}
