using JUnity.Components;
using Lab3.Scripts.UI;
using SharpDX;

namespace Lab3.Scripts.MutableObjects
{
    public class VoltmeterScript : Script
    {
        private const float MIN_ANGLE = MathUtil.Pi / 3;
        private const float MAX_ANGLE = -MathUtil.Pi / 3;

        private const float MAX_VALUE = 200f;

        public override void Start()
        {
            var ui_script = (ElectricalСircuitPanelUI)Scene.Find("ElectricalСircuitUI").Script;
            ui_script.VoltageInput.ValueChanged += UpdatePointer;
        }

        private void UpdatePointer(object sender, JUnity.Components.UI.FloatTextBoxValueChangedEventArgs e)
        {
            var value = e.Value;

            Object.Rotation *= Quaternion.RotationAxis(Vector3.UnitY, MAX_ANGLE);
        }
    }
}
