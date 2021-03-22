using SharpDX;
using System;

namespace JUnity.Services.Graphics
{
    public class Camera
    {
        private Matrix? _pojectionMatrix;
        private float _fov;
        private float _aspectRatio;

        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public float Fov
        {
            get => _fov;
            set
            {
                _pojectionMatrix = null;
                _fov = value;
            }
        }

        public float AspectRatio
        {
            get => _aspectRatio;
            set
            {
                _pojectionMatrix = null;
                _aspectRatio = value;
            }
        }

        public float DrawDistance { get; set; }

        public float NearDistance { get; set; }

        internal Matrix GetPojectionMatrix()
        {
            if (_pojectionMatrix == null)
            {
                _pojectionMatrix = Matrix.PerspectiveFovLH(Fov / AspectRatio, AspectRatio, NearDistance, DrawDistance);
            }

            return _pojectionMatrix.Value;
        }

        internal Matrix GetViewMatrix()
        {
            var rotationMatrix = Matrix.RotationQuaternion(Rotation);
            var translationMatrix = Matrix.Translation(Position);
            return translationMatrix * rotationMatrix;
        }

        [Obsolete("Do not use it!")]
        internal Matrix GetViewMatrixTema()
        {
            Matrix rotation = Matrix.RotationQuaternion(Rotation);
            Vector3 viewTo = (Vector3)Vector4.Transform(Vector4.UnitZ, rotation);
            Vector3 viewUp = (Vector3)Vector4.Transform(Vector4.UnitY, rotation);
            return Matrix.LookAtLH(Position, Position + viewTo, viewUp);
        }
    }
}
