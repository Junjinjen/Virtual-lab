using JUnity.Components;
using Lab3.Scripts.UI;

namespace Lab3.Scripts.Interactions
{
    public class WaterScript : Script
    {
        private const float MAX_SIZE_Z = 11.3f;
        private const float MAX_VOLUME = 3f;

        public override void Start()
        {
            var ui_script = (WaterPanelUI)Scene.Find("WaterUI").Script;
            ui_script.WaterVolumeInput.ValueChanged += ResizeWater;
        }

        private void ResizeWater(object sender, JUnity.Components.UI.FloatTextBoxValueChangedEventArgs e)
        {
            var value = e.Value;
            if (value > MAX_VOLUME)
            {
                value = MAX_VOLUME;
            }
            else if (value < 0)
            {
                value = 0;
            }

            var scale = Object.Scale;
            scale.Z = MAX_SIZE_Z * value / MAX_VOLUME;
            Object.Scale = scale;
        }

    }
}
