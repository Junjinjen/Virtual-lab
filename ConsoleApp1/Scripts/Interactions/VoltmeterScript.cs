using JUnity.Components;
using Lab3.Scripts.UI;
using SharpDX;

namespace Lab3.Scripts.Interactions
{
    public class VoltmeterScript : Script
    {
        private AmmeterScript _ammeterScript;

        private const float MIN_ANGLE = -MathUtil.Pi / 3;
        private const float MAX_ANGLE = MathUtil.Pi / 3;

        private const float MAX_VALUE = 200f;

        private Quaternion _baseRotation;

        public override void Start()
        {
            if (_baseRotation == Quaternion.Zero)
            {
                _baseRotation = Object.Rotation;
                Object.Rotation = _baseRotation * Quaternion.RotationAxis(-Vector3.UnitY, MIN_ANGLE);
            }

            var ui_script = (ElectricalСircuitPanelUI)Scene.Find("ElectricalСircuitUI").Script;
            _ammeterScript = (AmmeterScript)Scene.Find("Ammeter").Children[0].Script;

            var timer = (TimerScript)Scene.Find("Timer").Script;
            timer.OnTimerStarted += (o, e) => UpdatePointer(ui_script.VoltageInput.Value);
            timer.OnTimerReseted += (o, e) => UpdatePointer(0);
        }

        private void UpdatePointer(float value)
        {
            if(value > MAX_VALUE)
            {
                value = MAX_VALUE;
            }
            else if(value < 0)
            {
                value = 0;
            }

            var angle = MIN_ANGLE + value * (2 * MAX_ANGLE) / MAX_VALUE;
            Object.Rotation = _baseRotation * Quaternion.RotationAxis(-Vector3.UnitY, angle);
            _ammeterScript.UpdatePointer(value);
        }
    }
}
