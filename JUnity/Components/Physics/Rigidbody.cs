using JUnity.Components.Interfaces;
using JUnity.Components.Physics.Colliders;
using SharpDX;
using System.Collections.Generic;

namespace JUnity.Components.Physics
{
    public sealed class Rigidbody : GameComponent, IFixedUpdatableComponent, IUniqueComponent
    {
        private const float GravityForce = 10.0f;
        private readonly static List<Collider> _colliders = new List<Collider>();
        private readonly List<Collider> _myColliders = new List<Collider>();
        private Vector3 _impulse;

        internal Rigidbody(GameObject owner)
            : base(owner)
        {
            UseGravity = true;
            AirResistance = Vector3.One;
            Mass = 1;
        }

        public bool UseGravity { get; set; }

        public float Mass { get; set; }

        public Vector3 Velocity { get; set; }

        public Vector3 Force { get; set; }

        public Vector3 AirResistance { get; set; }

        public void AddCollider(Collider collider)
        {
            collider.Register(this);
            _myColliders.Add(collider);
        }

        public void AddImpulse(Vector3 impulse)
        {
            _impulse += impulse;
        }

        internal override void CallComponent(double deltaTime)
        {
            var attachedForce = Force + _impulse;
            _impulse = Vector3.Zero;

            if (UseGravity)
            {
                attachedForce.Y -= GravityForce * Mass;
            }

            attachedForce -= AirResistance * Velocity;
            Velocity += attachedForce / Mass * (float)deltaTime;
            Owner.Position += Velocity * (float)deltaTime;
        }

        public override void Dispose()
        {
            foreach (var collider in _colliders)
            {
                _colliders.Remove(collider);
            }
        }
    }
}
