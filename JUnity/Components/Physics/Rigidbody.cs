using JUnity.Components.Interfaces;
using JUnity.Components.Physics.Colliders;
using SharpDX;
using System;
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

        public event EventHandler<CollisionTriggerEventArgs> TriggerEnter;

        public bool UseGravity { get; set; }

        public float Mass { get; set; }

        public Vector3 Velocity { get; set; }

        public Vector3 Force { get; set; }

        public Vector3 AirResistance { get; set; }

        public void AddCollider(Collider collider)
        {
            collider.Register(this);
            _myColliders.Add(collider);
            _colliders.Add(collider);

            collider.WorldMatrixChanged(Owner.GetWorldMatrix());
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

            foreach (var collider in _myColliders)
            {
                foreach (var other in _colliders)
                {
                    if (!_myColliders.Contains(other))
                    {
                        collider.CheckCollision(other);
                    }
                }
            }

            if (Engine.Instance.Settings.DrawColliders)
            {
                DrawMyColliders();
            }
        }

        internal void FireTriggerEvent(Collider triggeredCollider, Collider otherCollider)
        {
            TriggerEnter?.Invoke(this, new CollisionTriggerEventArgs(triggeredCollider, otherCollider));
        }

        private void DrawMyColliders()
        {
            foreach (var collider in _myColliders)
            {
                Engine.Instance.GraphicsRenderer.AddOnTopRenderOrder(collider.GenerateRenderOrder());
            }
        }

        public override void Dispose()
        {
            foreach (var collider in _myColliders)
            {
                _colliders.Remove(collider);
            }
        }
    }

    public class CollisionTriggerEventArgs : EventArgs
    {
        public CollisionTriggerEventArgs(Collider triggeredCollider, Collider otherCollider)
        {
            TriggeredCollider = triggeredCollider;
            OtherCollider = otherCollider;
        }

        public Collider OtherCollider { get; }

        public Collider TriggeredCollider { get; }
    }
}
