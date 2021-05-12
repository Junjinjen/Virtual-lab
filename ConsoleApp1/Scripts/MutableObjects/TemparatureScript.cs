using JUnity.Components;
using JUnity.Components.UI;
using Lab3.Scripts.UI;
using System;

namespace Lab3.Scripts.MutableObjects
{
    public class TemparatureScript : Script
    {
        private const float MAX_SIZE_Z = 24f;
        private const float MAX_TEMPARATURE = 100f;
        private const float MIN_TEMPARATURE = -20f;

        private float offsetSizeZ;

        private TextBox currentWaterTemperature;
        private TextBox currentWaterTemperatureTermometer;

        public override void Start()
        {
            offsetSizeZ = Object.Scale.Z;
            var ui_script = (WaterPanelUI)Scene.Find("WaterUI").Script;
            ui_script.WaterTemparatureInput.ValueChanged += ResizeTemperature;
            currentWaterTemperature = ui_script.CurrentWaterTemperature;
            currentWaterTemperatureTermometer = ui_script.CurrentWaterTemperatureTermometer;
        }

        private void ResizeTemperature(object sender, JUnity.Components.UI.FloatTextBoxValueChangedEventArgs e)
        {
            var value = e.Value;
            if (value > MAX_TEMPARATURE)
            {
                value = MAX_TEMPARATURE;
            }
            else if (value < MIN_TEMPARATURE)
            {
                value = MIN_TEMPARATURE;
            }

            var scale = Object.Scale;
            scale.Z = MAX_SIZE_Z * (value - MIN_TEMPARATURE) / (MAX_TEMPARATURE - MIN_TEMPARATURE);
            scale.Z += offsetSizeZ;
            Object.Scale = scale;
            currentWaterTemperature.Value = String.Format("{0:f1}",value);
            currentWaterTemperatureTermometer.Value = String.Format("{0:f1}", value);
        }
    }
}
