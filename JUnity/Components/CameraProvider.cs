using JUnity.Services.Graphics;

namespace JUnity.Components
{
    public sealed class CameraProvider
    {
        private readonly Camera _camera;

        public CameraProvider()
        {
            _camera = Engine.Instance.GraphicsRenderer.Camera;
        }

        public Camera Camera { get => _camera; }
    }
}
