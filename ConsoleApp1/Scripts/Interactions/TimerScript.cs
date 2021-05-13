using JUnity.Components;
using JUnity.Components.Physics;
using JUnity.Components.UI;
using JUnity.Services.Input;
using Lab3.Scripts.UI;
using SharpDX;
using System;
using System.Diagnostics;

namespace Lab3.Scripts.Interactions
{
    public class TimerScript : Script
    {
        public event EventHandler<EventArgs> OnTimerStarted;
        public event EventHandler<EventArgs> OnTimerPaused;
        public event EventHandler<EventArgs> OnTimerReseted;

        public float Seconds { get; private set; }

        private PointMovement _playButtonAnimation;
        private PointMovement _pauseButtonAnimation;
        private PointMovement _stopButtonAnimation;

        private Stopwatch _timer;
        private TextBox _currentTime;

        private bool _isUpdate = false;

        public override void Start()
        {
            _timer = new Stopwatch();
            var ui_script = (TimerPanelUI)Scene.Find("TimerUI").Script;
            _currentTime = ui_script.CurrentTime;

            _playButtonAnimation = new PointMovement(Object.Children[0], Object.Children[0].Position);
            _playButtonAnimation.Points.Add(Object.Children[0].Position + Vector3.UnitY * 0.1f);
            _playButtonAnimation.Points.Add(Object.Children[0].Position);
            _playButtonAnimation.DefaultSpeed = 0.5f;

            _pauseButtonAnimation = new PointMovement(Object.Children[1], Object.Children[1].Position);
            _pauseButtonAnimation.Points.Add(Object.Children[1].Position + Vector3.UnitY * 0.1f);
            _pauseButtonAnimation.Points.Add(Object.Children[1].Position);
            _pauseButtonAnimation.DefaultSpeed = 0.5f;

            _stopButtonAnimation = new PointMovement(Object.Children[2], Object.Children[2].Position);
            _stopButtonAnimation.Points.Add(Object.Children[2].Position + Vector3.UnitY * 0.1f);
            _stopButtonAnimation.Points.Add(Object.Children[2].Position);
            _stopButtonAnimation.DefaultSpeed = 0.5f;

            MouseGrip.OnLeftClickObject += HandleButtons;
        }

        public override void FixedUpdate(double deltaTime)
        {
            _playButtonAnimation.Move((float)deltaTime);
            _pauseButtonAnimation.Move((float)deltaTime);
            _stopButtonAnimation.Move((float)deltaTime);

            if (_isUpdate)
            {
                Seconds = _timer.ElapsedMilliseconds / 1000f;
                _currentTime.Value = string.Format("{0:f2}", Seconds);
            }
            else
            {
                _currentTime.Value = "0,00";
            }
        }

        private void HandleButtons(object sender, OnClickEventArgs e)
        {
            switch (e.Object?.Name)
            {
                case "Play":
                    _timer.Start();
                    _playButtonAnimation.Reset();
                    _playButtonAnimation.Start();
                    _isUpdate = true;
                    OnTimerStarted?.Invoke(this, null);
                    break;
                case "Pause":
                    _timer.Stop();
                    _pauseButtonAnimation.Reset();
                    _pauseButtonAnimation.Start();
                    OnTimerPaused?.Invoke(this, null);
                    break;
                case "Stop":
                    _timer.Reset();
                    _stopButtonAnimation.Reset();
                    _stopButtonAnimation.Start();
                    _isUpdate = false;
                    OnTimerReseted?.Invoke(this, null);
                    break;
            }
        }
    }
}
