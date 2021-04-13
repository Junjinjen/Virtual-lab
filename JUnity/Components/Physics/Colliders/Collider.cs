using JUnity.Services.Graphics.Meshing;
using SharpDX;

namespace JUnity.Components.Physics.Colliders
{
    public abstract class Collider
    {
        private bool _isWorldMatrixChanged;

        protected Rigidbody Rigidbody { get; private set; }

        internal void Register(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;

            Rigidbody.Owner.OnRotation += (s, e) => _isWorldMatrixChanged = true;
            Rigidbody.Owner.OnScale += (s, e) => _isWorldMatrixChanged = true;
            Rigidbody.Owner.OnTranslation += (s, e) => _isWorldMatrixChanged = true;
            _isWorldMatrixChanged = true;
        }

        internal void CheckCollision(Collider other)
        {
            if (_isWorldMatrixChanged)
            {
                WorldMatrixChanged(Rigidbody.Owner.GetWorldMatrix());
            }

            ResolveCollision(other);
        }

        internal abstract void WorldMatrixChanged(Matrix worldMatrix);

        internal abstract void ResolveCollision(Collider other);

        internal abstract Mesh GenerateMesh();
    }
}
