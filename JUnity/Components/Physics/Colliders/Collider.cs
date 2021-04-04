using JUnity.Services.Graphics;

namespace JUnity.Components.Physics.Colliders
{
    public abstract class Collider
    {
        protected Rigidbody Rigidbody { get; private set; }

        internal void Register(Rigidbody rigidbody)
        {
            Rigidbody = rigidbody;
        }

        internal abstract void ResolveCollision(Collider other);

        internal abstract void DrawCollider(GraphicsRenderer graphicsRenderer);
    }
}
