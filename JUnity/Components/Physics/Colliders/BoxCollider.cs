using JUnity.Services.Graphics;
using JUnity.Services.Graphics.Meshing;
using SharpDX;

namespace JUnity.Components.Physics.Colliders
{
    public class BoxCollider : Collider
    {
        private readonly Vector3 _minimum;
        private readonly Vector3 _maximum;
        private Vector3 _worldMinimum;
        private Vector3 _worldMaximum;
        private BoundingBox _boundingBox;

        public BoxCollider(Vector3 minimum, Vector3 maximum)
        {
            _minimum = minimum;
            _maximum = maximum;
        }

        internal override Mesh GenerateMesh()
        {
            throw new System.NotImplementedException();
        }

        internal override void ResolveCollision(Collider other)
        {
            switch (other)
            {
                case BoxCollider otherBox:
                    break;
                default:
                    break;
            }
        }

        internal override void WorldMatrixChanged(Matrix worldMatrix)
        {
            _worldMinimum = Vector3.TransformCoordinate(_minimum, worldMatrix);
            _worldMaximum = Vector3.TransformCoordinate(_maximum, worldMatrix);
            UpdateBoundingBox();
        }

        private void UpdateBoundingBox()
        {

        }
    }
}
