using JUnity.Components;
using JUnity.Components.Physics;
using JUnity.Services.Input;
using SharpDX;

namespace Lab3.Scripts.Interactions
{
    public class MetalScript : Script
    {
        public bool IsMoving { get; set; }

        public bool IsInWater { get; set; }

        public bool IsSelected { get; set; }

        public bool IsOnWeigher { get; set; }

        private Vector3 _startPosition;

        private PointMovement _liftingAnimation;
        private PointMovement _loweringAnimation;

        private bool _isTimerStarted;

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
                if (_isTimerStarted) return;

                if (e.Object?.Name == "Metal" && !IsOnWeigher)
                {
                    IsSelected = true;
                    _loweringAnimation.Stop();
                    _liftingAnimation.Start();
                }
            };

            MouseGrip.OnRightClickObject += (o, e) =>
            {
                if (_isTimerStarted) return;

                if (IsSelected && !IsOnWeigher)
                {
                    _liftingAnimation.Stop();
                    _loweringAnimation.Start();
                }
                else
                {
                    Object.Position = _startPosition;
                }
                IsSelected = false;
            };

            var timer_script = (TimerScript)Scene.Find("Timer").Script;
            timer_script.OnTimerStarted += (o, e) =>
            {
                if(IsMoving || IsOnWeigher)
                {
                    Object.Position = _startPosition;
                }    
                _isTimerStarted = true;
            };
            timer_script.OnTimerReseted += (o, e) =>
            {
                _isTimerStarted = false;
                IsSelected = false;
                IsOnWeigher = false;
                Object.Position = _startPosition;
            };
        }

        public override void Update(double deltaTime)
        {
            _liftingAnimation.Move((float)deltaTime);
            _loweringAnimation.Move((float)deltaTime);
        }
    }
}
