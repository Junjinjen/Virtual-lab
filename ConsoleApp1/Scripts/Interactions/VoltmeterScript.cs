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
            _baseRotation = Object.Rotation;
            var ui_script = (ElectricalСircuitPanelUI)Scene.Find("ElectricalСircuitUI").Script;
            ui_script.VoltageInput.ValueChanged += UpdatePointer;
            _ammeterScript = (AmmeterScript)Scene.Find("Ammeter").Children[0].Script;
        }

        private void UpdatePointer(object sender, JUnity.Components.UI.FloatTextBoxValueChangedEventArgs e)
        {
            var value = e.Value;
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
