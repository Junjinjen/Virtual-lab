using JUnity.Components;
using JUnity.Components.Audio;
using JUnity.Components.Physics;
using JUnity.Services.Input;
using Lab3.Scripts.UI;
using SharpDX;
using System;

namespace Lab3.Scripts.Interactions
{
    public class WeigherScript : Script
    {
        private AudioPlayer _audioWeigher;
        private MetalPanelUI _metalUI;
        private MetalScript _metalScript;
        private PointMovement _moveMetalAnimation;

        public override void Start()
        {
            _audioWeigher = Object.GetComponent<AudioPlayer>();

            _metalUI = (MetalPanelUI)Scene.Find("MetalUI").Script;
            _metalScript = (MetalScript)Scene.Find("Metal").Script;

            _moveMetalAnimation = new PointMovement(_metalScript.Object, _metalScript.Object.Position);
            _moveMetalAnimation.Points.Add(
                new Vector3(_metalScript.Object.Position.X, Object.Position.Y + 1.5f, _metalScript.Object.Position.Z));
            _moveMetalAnimation.Points.Add(Object.Position + Vector3.UnitY * 1.5f);
            _moveMetalAnimation.Points.Add(Object.Position + Vector3.UnitY * 1.4f);
            _moveMetalAnimation.DefaultSpeed = 2f;
            _moveMetalAnimation.OnAnimationEnd += UpdateWeigherWithMetal;

            var timer_script = (TimerScript)Scene.Find("Timer").Script;
            timer_script.OnTimerStarted += (o, e) =>
            {
                if (_metalScript.IsMoving || _metalScript.IsOnWeigher)
                {
                    _metalUI.CurrentWeight.Value = "0,000";
                }
                _moveMetalAnimation.Stop();
            };
            timer_script.OnTimerReseted += (o, e) =>
            {
                _moveMetalAnimation.Stop();
            };

            MouseGrip.OnLeftClickObject += (o, e) =>
            {
                if (timer_script.IsTimerStarted) return;

                if (e.Object?.Name == Object.Children[0].Name && _metalScript.IsSelected && !_metalScript.IsOnWeigher)
                {
                    _metalScript.PlayVoice();
                    _metalScript.IsMoving = true;
                    _moveMetalAnimation.Reset();              
                    _moveMetalAnimation.Start();
                }
            };

            MouseGrip.OnRightClickObject += (o, e) =>
            {
                if (timer_script.IsTimerStarted) return;

                _metalScript.IsOnWeigher = false;
                _moveMetalAnimation.Stop();
                _metalUI.CurrentWeight.Value = "0,000";
            };

            _metalUI.MetalChanged += (o, e) =>
            {
                _moveMetalAnimation.Stop();
                _metalUI.CurrentWeight.Value = "0,000";
            };
        }

        public override void Update(double deltaTime)
        {
            _moveMetalAnimation.Move((float)deltaTime);
        }

        private void UpdateWeigherWithMetal(object sender, EventArgs e)
        {
            _audioWeigher.Play();

            _metalScript.IsMoving = false;
            _metalScript.IsOnWeigher = true;

            var value = _metalUI.MetalParameters.Mass;
            value += Lab3.Scene.Random.NextFloat(-0.0015f, 0.0015f) * value;
            _metalUI.CurrentWeight.Value = string.Format("{0:f3}", value);
        }
    }
}
