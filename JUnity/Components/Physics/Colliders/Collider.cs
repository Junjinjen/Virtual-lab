using JUnity.Services.Graphics.Meshing;
using SharpDX;

namespace JUnity.Components.Physics.Colliders
{
    public abstract class Collider
    {
        protected Collider(string name = "default")
        {
            Name = name;
        }
        public string Name { get; private set; }



        internal Rigidbody Rigidbody { get; private set; }

        public abstract bool Intersects(ref Ray ray, out float distance);

        internal void Register(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;

            Rigidbody.Owner.OnScale += (s, e) => WorldMatrixChanged(Rigidbody.Owner.GetWorldMatrix());
            Rigidbody.Owner.OnTranslation += (s, e) => WorldMatrixChanged(Rigidbody.Owner.GetWorldMatrix());
        }

        internal void CheckCollision(Collider other)
        {
            ResolveCollision(other);
        }

        internal abstract void WorldMatrixChanged(Matrix worldMatrix);

        internal abstract void ResolveCollision(Collider other);

        internal abstract RenderOrder GenerateRenderOrder();
    }
}
