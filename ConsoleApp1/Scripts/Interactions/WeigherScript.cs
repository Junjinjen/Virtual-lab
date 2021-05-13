using JUnity.Components;
using JUnity.Components.Physics;
using JUnity.Services.Input;
using Lab3.Scripts.UI;
using SharpDX;
using System;

namespace Lab3.Scripts.Interactions
{
    public class WeigherScript : Script
    {
        private MetalPanelUI _metalUI;
        private MetalScript _metalScript;
        private PointMovement _moveMetalAnimation;

        public override void Start()
        {
            _metalUI = (MetalPanelUI)Scene.Find("MetalUI").Script;
            _metalScript = (MetalScript)Scene.Find("Metal").Script;

            _moveMetalAnimation = new PointMovement(_metalScript.Object, _metalScript.Object.Position);
            _moveMetalAnimation.Points.Add(
                new Vector3(_metalScript.Object.Position.X, Object.Position.Y + 1.5f, _metalScript.Object.Position.Z));
            _moveMetalAnimation.Points.Add(Object.Position + Vector3.UnitY * 1.5f);
            _moveMetalAnimation.Points.Add(Object.Position + Vector3.UnitY * 1.35f);
            _moveMetalAnimation.DefaultSpeed = 2f;
            _moveMetalAnimation.OnAnimationEnd += UpdateWeigherWithMetal;

            MouseGrip.OnLeftClickObject += (o, e) =>
            {
                if(e.Object?.Name == Object.Children[0].Name && _metalScript.IsSelected && !_metalScript.IsOnWeigher)
                {
                    _moveMetalAnimation.Reset();              
                    _moveMetalAnimation.Start();
                }
            };

            MouseGrip.OnRightClickObject += (o, e) =>
            {
                _metalScript.IsOnWeigher = false;
                _moveMetalAnimation.Stop();
                _metalUI.CurrentWeight.Value = "0,000";
            };

            var timer_script = (TimerScript)Scene.Find("Timer").Script;
            timer_script.OnTimerReseted += (o, e) =>
            {
                _moveMetalAnimation.Stop();
            };
        }

        public override void Update(double deltaTime)
        {
            _moveMetalAnimation.Move((float)deltaTime);
        }

        private void UpdateWeigherWithMetal(object sender, EventArgs e)
        {
            _metalScript.IsOnWeigher = true;

            var value = _metalUI.MetalParameters.Mass;
            value += Lab3.Scene.Random.NextFloat(-0.0015f, 0.0015f) * value;
            _metalUI.CurrentWeight.Value = string.Format("{0:f3}", value);
        }
    }
}
