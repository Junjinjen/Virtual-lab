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
    public static class MouseGrip 
    {
        public static event EventHandler<OnClickEventArgs> OnLeftClickObject;
        public static event EventHandler<OnClickEventArgs> OnRightClickObject;

        public static GameObject _targetObject { get; private set; }
        private static Camera _camera;

        public static void SetCameraProvider(CameraProvider camera)
        {
            _camera = camera.Camera;
        }
        
        public static void InitMouseGrip()
        {
            Engine.Instance.InputManager.OnMouseBottonDown += OnClick;
            Engine.Instance.InputManager.OnMouseBottonUp += OnEndClick;
        }

        private static void OnClick(Object obj, MouseClickEventArgs args)
        {
            if(args.Key == MouseKey.Left)
            {
                var coords = args.Position;
                var size = Engine.Instance.GraphicsRenderer.RenderForm.ClientRectangle;
                var ray = Ray.GetPickRay((int)(coords.X * size.Width), (int)(coords.Y * size.Height), new ViewportF(0, 0, size.Width, size.Height),
                    _camera.GetViewMatrixTema() * _camera.GetPojectionMatrix());

                Rigidbody.Intersects(ray, out var rigidbody, out var distance);
                if(rigidbody != null)
                {
                    _targetObject = rigidbody.Owner;
                    OnLeftClickObject?.Invoke(null, new OnClickEventArgs(_targetObject));
                }
            }
        }

        private static void OnEndClick(Object obj, MouseClickEventArgs args)
        {
            if(args.Key == MouseKey.Right)
            {
                _targetObject = null;
                OnRightClickObject?.Invoke(null, new OnClickEventArgs(_targetObject));
            }
        }
    }

    public class OnClickEventArgs : EventArgs
    {
        public GameObject Object { get; private set; }

        public OnClickEventArgs(GameObject obj)
        {
            Object = obj;
        }
    }
}
