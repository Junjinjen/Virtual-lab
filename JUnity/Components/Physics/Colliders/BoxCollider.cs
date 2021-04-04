using JUnity.Services.Graphics;
using SharpDX;

namespace JUnity.Components.Physics.Colliders
{
    public class BoxCollider : Collider
    {
        private BoundingBox _boundingBox;

        public BoxCollider(Vector3 minimum, Vector3 maximum)
        {
        }

        public override void DrawCollider(GraphicsRenderer graphicsRenderer)
        {
            throw new System.NotImplementedException();
        }

        public override void ResolveCollision(Collider other)
        {
            throw new System.NotImplementedException();
        }
    }
}
