using JUnity.Components;
using JUnity.Services.Input;
using Lab3.Scripts.UI;

namespace Lab3.Scripts.Interactions
{
    public class WaterScript : Script
    {
        private const float MAX_SIZE_Z = 11.3f;
        private const float MAX_VOLUME = 3f;
        private const float EXTRA_VOLUME = 0.1f;

        private bool _isProcessStarted;
        private bool _isAddExtra;
        private float _oldVolume;

        public override void Start()
        {
            var ui_script = (WaterPanelUI)Scene.Find("WaterUI").Script;
            ui_script.WaterVolumeInput.ValueChanged += (o, e) => UpdateWater(e.Value);

            MouseGrip.OnRightClickObject += (o, e) =>
            {  
                RemoveExtraVolume();
            };

            var metal_UI = (MetalPanelUI)Scene.Find("MetalUI").Script;
            metal_UI.MetalChanged += (o, e) =>
            {
                RemoveExtraVolume();
            };

            var timer_script = (TimerScript)Scene.Find("Timer").Script;
            timer_script.OnTimerStarted += (o, e) => _isProcessStarted = true;
            timer_script.OnTimerReseted += (o, e) =>
            {
                _isProcessStarted = false;
                RemoveExtraVolume();
            };
        }
        private void UpdateWater(float value)
        {
            if (value > MAX_VOLUME)
            {
                value = MAX_VOLUME;
            }
            else if (value < 0)
            {
                value = 0;
            }

            if (_isAddExtra)
            {
                value += EXTRA_VOLUME;
            }
            _oldVolume = value;

            var scale = Object.Scale;
            scale.Z = MAX_SIZE_Z * value / MAX_VOLUME;
            Object.Scale = scale;
        }

        public void AddExtraVolume()
        {
            _isAddExtra = true;
            UpdateWater(_oldVolume);
        }

        public void RemoveExtraVolume()
        {
            if (!_isProcessStarted)
            {
                if (_isAddExtra)
                    _oldVolume -= EXTRA_VOLUME;
                _isAddExtra = false;
                UpdateWater(_oldVolume);
            }
        }
    }
}
