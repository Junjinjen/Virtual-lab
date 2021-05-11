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
        private static GameObject _gripObject;
        private static Camera _camera;
        private static Vector3 _cameraDirection;
        private static float _distance;

        public static void SetCameraProvider(CameraProvider camera)
        {
            _camera = camera.Camera;
            _cameraDirection = Vector3.UnitZ;
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
                    _gripObject = rigidbody.Owner;
                    _distance = distance;
                }
            }
        }

        private static void OnEndClick(Object obj, MouseClickEventArgs args)
        {
            if(args.Key == MouseKey.Left)
            {
                _gripObject = null;
            }
        }

        internal static void UpdatePosition()
        {
            if (_gripObject != null)
            {
                var mouseOffset = Engine.Instance.InputManager.GetMouseOffset();

                Matrix worldMatrix = Matrix.Identity;
                Matrix scaling = Matrix.Identity;

                var parrent = _gripObject.Parent;

                while (parrent != null)
                {
                    worldMatrix *= Matrix.Orthogonalize(parrent.GetWorldMatrix());
                    scaling *= Matrix.Scaling(new Vector3(1 / parrent.Scale.X, 1 / parrent.Scale.Y, 1 / parrent.Scale.Z));
                    parrent = parrent.Parent;
                }

                _gripObject.Position += Vector3.TransformCoordinate(new Vector3(-mouseOffset, 0) / 1.5f, worldMatrix * scaling);
            }
        }


    }
}
