using JUnity.Components;
using JUnity.Components.Audio;
using Lab3.Scripts.UI;
using SharpDX;
using System;

namespace Lab3.Scripts.Interactions
{
    public class HeatingProcessScript : Script
    {
        private const float SH_WATER = 4180.6f;

        private AudioPlayer _audioHeating;

        private WaterPanelUI _waterUI;
        private ElectricalСircuitPanelUI _electricalСircuitUI;

        private TemparatureScript _temparatureScript;
        private TimerScript _timerScript;

        private bool _isHeating;
        private bool _isProcessStarted;
        private bool _isProccesPaused;

        private float _startTemparature;
        private float _processStep;

        public override void Start()
        {
            _audioHeating = Object.GetComponent<AudioPlayer>();

            _waterUI = (WaterPanelUI)Scene.Find("WaterUI").Script;
            _electricalСircuitUI = (ElectricalСircuitPanelUI)Scene.Find("ElectricalСircuitUI").Script;
            var metal_script = (MetalScript)Scene.Find("Metal").Script;
            var metal_ui = (MetalPanelUI)Scene.Find("MetalUI").Script;

            _temparatureScript = (TemparatureScript)Scene.Find("Thermometer").Children[2].Script;
            _timerScript = (TimerScript)Scene.Find("Timer").Script;
            _timerScript.OnTimerStarted += (o, e) => StartProcess(metal_ui.MetalParameters, metal_script.IsInWater);
            _timerScript.OnTimerPaused += (o, e) => StopProcess();
            _timerScript.OnTimerReseted += (o, e) => EndProcess();
        }

        public override void FixedUpdate(double deltaTime)
        {
            if(_isProcessStarted && !_isProccesPaused)
            {
                var temparature = _startTemparature + _processStep * _timerScript.Seconds;
                _temparatureScript.UpdateTemperature(temparature);

                if(temparature > 100f && !_isHeating)
                {
                    _isHeating = true;
                    _audioHeating.Play();
                }
            }
        }

        private void StartProcess(MetalParameters metalParameters, bool isMetalInWater)
        {
            if (!_isProcessStarted)
            {
                _isProcessStarted = true;

                _startTemparature = _waterUI.WaterTemparatureInput.Value;

                var loss = Lab3.Scene.Random.NextFloat(1.00f, 1.01f);
                var mW = _waterUI.WaterVolumeInput.Value;
                var U = _electricalСircuitUI.VoltageInput.Value;
                var I = Convert.ToSingle(_electricalСircuitUI.CurrentAmperage.Value);
                if (isMetalInWater)
                {
                    var mM = metalParameters.Mass;
                    var shM = metalParameters.SpecificHeat;

                    _processStep = (U * I) / (loss * mW * SH_WATER + mM * shM);
                }
                else
                {                 
                    _processStep = (U * I) / (loss * mW * SH_WATER);
                }
            }

            if (_isProccesPaused)
            {
                _isProccesPaused = false;
                _isHeating = false;
            }
        }

        private void StopProcess()
        {
            _audioHeating.Pause();
            _isProccesPaused = true;
        }

        private void EndProcess()
        {
            _audioHeating.Stop();
            _isProcessStarted = false;
            _isProccesPaused = true;
        }
    }
}
