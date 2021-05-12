using JUnity.Components;
using JUnity.Components.Physics;
using JUnity.Services.Graphics;
using JUnity.Services.Graphics.Meshing;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUnity.Services.Input
{
    public static class MouseClicker
    {
        public static GameObject CurrentGameObject => _currentObject;
        private static GameObject _currentObject;
        private static Camera _camera;

        public static event EventHandler<EventClickArgs> OnClickObject;

        public static void SetCameraProvider(CameraProvider camera)
        {
            _camera = camera.Camera;
        }

        public static void InitMouseGrip()
        {
            Engine.Instance.InputManager.OnMouseBottonDown += OnClick;
        }

        private static void OnClick(Object obj, MouseClickEventArgs args)
        {
            if (args.Key == MouseKey.Left)
            {
                var coords = args.Position;
                var size = Engine.Instance.GraphicsRenderer.RenderForm.ClientRectangle;
                var ray = Ray.GetPickRay((int)(coords.X * size.Width), (int)(coords.Y * size.Height), new ViewportF(0, 0, size.Width, size.Height),
                    _camera.GetViewMatrixTema() * _camera.GetPojectionMatrix());

                Rigidbody.Intersects(ray, out var rigidbody, out var distance);
                if (rigidbody != null)
                {
                    OnClickObject?.Invoke(null, new EventClickArgs(rigidbody.Owner));
                    _currentObject = rigidbody.Owner;
                }
            }
        }
    }

    public class EventClickArgs : EventArgs
    {
        public GameObject GameObject { get; set; }

        public EventClickArgs(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}
