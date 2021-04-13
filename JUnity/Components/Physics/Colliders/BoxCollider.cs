using JUnity.Services.Graphics;
using JUnity.Services.Graphics.Meshing;
using SharpDX;

namespace JUnity.Components.Physics.Colliders
{
    public class BoxCollider : Collider
    {
        private readonly Vector3 _minimum;
        private readonly Vector3 _maximum;
        private BoundingBox _boundingBox;

        public BoxCollider(Vector3 minimum, Vector3 maximum)
        {
            _minimum = minimum;
            _maximum = maximum;
        }

        public Vector3 Minimum { get; private set; }

        public Vector3 Maximum { get; private set; }

        internal override Mesh GenerateMesh()
        {
            throw new System.NotImplementedException();
        }

        internal override void ResolveCollision(Collider other)
        {
            throw new System.NotImplementedException();
        }

        internal override void WorldMatrixChanged(Matrix worldMatrix)
        {
            Minimum = Vector3.TransformCoordinate(_minimum, worldMatrix);
            Maximum = Vector3.TransformCoordinate(_maximum, worldMatrix);
            UpdateBoundingBox();
        }

        private void UpdateBoundingBox()
        {

        }
    }
}
