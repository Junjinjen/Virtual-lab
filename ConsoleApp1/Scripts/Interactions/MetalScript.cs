using JUnity.Components;
using JUnity.Components.Physics;
using JUnity.Services.Input;
using SharpDX;

namespace Lab3.Scripts.Interactions
{
    public class MetalScript : Script
    {
        public bool IsSelected { get; set; }

        public bool IsMoving { get; set; }

        public bool IsWithOtherObject { get; set; }

        public float Weight { get; set; } = 0.96f;

        private Vector3 _startPosition;

        private PointMovement _liftingAnimation;
        private PointMovement _loweringAnimation;

        public override void Start()
        {
            _startPosition = Object.Position;

            _liftingAnimation = new PointMovement(Object, _startPosition);
            _liftingAnimation.Points.Add(_startPosition + Vector3.UnitY * 0.2f);
            _liftingAnimation.DefaultSpeed = 2f;

            _loweringAnimation = new PointMovement(Object, _startPosition + Vector3.UnitY * 0.2f);
            _loweringAnimation.Points.Add(_startPosition);
            _loweringAnimation.DefaultSpeed = 2f;

            MouseGrip.OnLeftClickObject += (o, e) =>
            {
                if (e.Object?.Name == "Metal" && !IsWithOtherObject && !IsMoving)
                {
                    IsSelected = true;
                    _loweringAnimation.Stop();
                    _liftingAnimation.Start();
                }
            };

            MouseGrip.OnRightClickObject += (o, e) =>
            {
                if (IsSelected && !IsMoving)
                {
                    IsSelected = false;
                    _liftingAnimation.Stop();
                    _loweringAnimation.Start();
                }
                else
                {
                    IsWithOtherObject = false;
                    Object.Position = _startPosition;
                }
            };
        }

        public override void Update(double deltaTime)
        {
            _liftingAnimation.Move((float)deltaTime);
            _loweringAnimation.Move((float)deltaTime);
        }
    }
}
