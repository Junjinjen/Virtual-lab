using JUnity.Components;
using JUnity.Components.Audio;
using JUnity.Components.Physics;
using JUnity.Services.Input;
using Lab3.Scripts.UI;
using SharpDX;

namespace Lab3.Scripts.Interactions
{
    public class MetalScript : Script
    {
        public bool IsMoving { get; set; }

        public bool IsInWater { get; set; }

        public bool IsSelected { get; set; }

        public bool IsOnWeigher { get; set; }

        private AudioPlayer _audioChoice;

        private PointMovement _liftingAnimation;
        private PointMovement _loweringAnimation;

        private Vector3 _startPosition;

        public override void Start()
        {
            _audioChoice = Object.GetComponent<AudioPlayer>();

            _startPosition = Object.Position;

            _liftingAnimation = new PointMovement(Object, _startPosition);
            _liftingAnimation.Points.Add(_startPosition + Vector3.UnitY * 0.2f);
            _liftingAnimation.DefaultSpeed = 2f;

            _loweringAnimation = new PointMovement(Object, _startPosition + Vector3.UnitY * 0.2f);
            _loweringAnimation.Points.Add(_startPosition);
            _loweringAnimation.DefaultSpeed = 2f;

            var timer_script = (TimerScript)Scene.Find("Timer").Script;
            timer_script.OnTimerStarted += (o, e) =>
            {
                if (IsMoving || (IsOnWeigher && !IsInWater))
                {
                    Object.Position = _startPosition;
                }
            };
            timer_script.OnTimerReseted += (o, e) =>
            {
                ResetPosition();
            };

            MouseGrip.OnLeftClickObject += (o, e) =>
            {
                if (timer_script.IsTimerStarted) return;

                if (e.Object?.Name == "Metal" && !IsOnWeigher && !IsMoving)
                {
                    PlayVoice();
                    IsSelected = true;
                    _loweringAnimation.Stop();
                    _liftingAnimation.Start();
                }
            };

            MouseGrip.OnRightClickObject += (o, e) =>
            {
                if (timer_script.IsTimerStarted) return;

                PlayVoice();
                if (IsSelected && !IsOnWeigher && !IsMoving && !IsInWater)
                {
                    _liftingAnimation.Stop();
                    _loweringAnimation.Start();
                }
                else
                {
                    Object.Position = _startPosition;
                }
                IsSelected = false;
                IsMoving = false;
            };

            var metal_ui = (MetalPanelUI)Scene.Find("MetalUI").Script;
            metal_ui.MetalChanged += UpdateMetal;
        }

        public override void Update(double deltaTime)
        {
            _liftingAnimation.Move((float)deltaTime);
            _loweringAnimation.Move((float)deltaTime);
        }

        public void PlayVoice()
        {
            _audioChoice.Play();
        }

        private void UpdateMetal(object sender, MetalChangeedEventArgs e)
        {
            for(int i = 0; i < 4; i++)
            {
                Object.Children[i].IsActive = i == e.MetalID;
            }

            ResetPosition();
        }

        private void ResetPosition()
        {
            IsMoving = false;
            IsSelected = false;
            IsOnWeigher = false;
            Object.Position = _startPosition;
        }
    }
}
