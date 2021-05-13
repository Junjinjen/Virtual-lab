using JUnity.Components;
using JUnity.Components.Physics;
using JUnity.Services.Input;
using SharpDX;

namespace Lab3.Scripts.Interactions
{
    public class FlaskScript : Script
    {
        private MetalScript _metalScript;
        private PointMovement _moveMetalAnimation;

        public override void Start()
        {
            _metalScript = (MetalScript)Scene.Find("Metal").Script;
            var water_script = (WaterScript)Scene.Find("Water").Script;
            
            var weigher = Scene.Find("Weigher");

            _moveMetalAnimation = new PointMovement(_metalScript.Object, weigher.Position + Vector3.UnitY * 1.35f);
            _moveMetalAnimation.Points.Add(weigher.Position + Vector3.UnitY * 3.8f);
            _moveMetalAnimation.Points.Add(Object.Position + Vector3.UnitY * 3.8f);
            _moveMetalAnimation.Points.Add(Object.Position + Vector3.UnitY);
            _moveMetalAnimation.DefaultSpeed = 2f;
            _moveMetalAnimation.OnAnimationEnd += (o, e) =>
            {
                _metalScript.IsMoving = false;
                _metalScript.IsInWater = true;
                water_script.AddExtraVolume();
            };

            MouseGrip.OnLeftClickObject += (o, e) =>
            {
                if (e.Object?.Name == Object.Name && _metalScript.IsOnWeigher)
                {
                    _metalScript.PlayVoice();
                    _metalScript.IsOnWeigher = false;
                    _metalScript.IsMoving = true;
                    _moveMetalAnimation.Reset();
                    _moveMetalAnimation.Start();
                }
            };
            MouseGrip.OnRightClickObject += (o, e) =>
            {
                water_script.RemoveExtraVolume();
                _metalScript.IsInWater = false;
                _moveMetalAnimation.Stop();
            };

            var timer_script = (TimerScript)Scene.Find("Timer").Script;
            timer_script.OnTimerStarted += (o, e) =>
            {
                _moveMetalAnimation.Stop();
            };
            timer_script.OnTimerReseted += (o, e) =>
            {
                _moveMetalAnimation.Stop();
            };
        }

        public override void Update(double deltaTime)
        {
            _moveMetalAnimation.Move((float)deltaTime);
        }
    }
}
