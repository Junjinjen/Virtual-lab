using JUnity.Components;
using Lab3.Scripts.UI;
using SharpDX;
using System;

namespace Lab3.Scripts.Interactions
{
    public class HeatingProcessScript : Script
    {
        private const float SH_WATER = 4180.6f;

        private WaterPanelUI _waterUI;
        private ElectricalСircuitPanelUI _electricalСircuitUI;

        private TemparatureScript _temparatureScript;
        private TimerScript _timerScript;

        private bool _isProcessStarted;
        private bool _isProccesPaused;

        private float _startTemparature;
        private float _processStep;

        public override void Start()
        {
            _waterUI = (WaterPanelUI)Scene.Find("WaterUI").Script;
            _electricalСircuitUI = (ElectricalСircuitPanelUI)Scene.Find("ElectricalСircuitUI").Script;

            _temparatureScript = (TemparatureScript)Scene.Find("Thermometer").Children[2].Script;
            _timerScript = (TimerScript)Scene.Find("Timer").Script;
            _timerScript.OnTimerStarted += (o, e) => StartProcess();
            _timerScript.OnTimerPaused += (o, e) => StopProcess();
            _timerScript.OnTimerReseted += (o, e) => EndProcess();
        }

        public override void FixedUpdate(double deltaTime)
        {
            if(_isProcessStarted && !_isProccesPaused)
            {
                var temparature = _startTemparature + _processStep * _timerScript.Seconds;
                _temparatureScript.UpdateTemperature(temparature);
            }
        }

        private void StartProcess()
        {
            if (!_isProcessStarted)
            {
                _isProcessStarted = true;

                _startTemparature = _waterUI.WaterTemparatureInput.Value;

                var loss = new Random().NextFloat(1.00f, 1.01f);
                var m = _waterUI.WaterVolumeInput.Value;
                var U = _electricalСircuitUI.VoltageInput.Value;
                var I = Convert.ToSingle(_electricalСircuitUI.CurrentAmperage.Value);

                _processStep = (U * I) / (loss * m * SH_WATER);
            }

            if (_isProccesPaused)
            {
                _isProccesPaused = true;
            }
        }

        private void StopProcess()
        {
            _isProccesPaused = true;
        }

        private void EndProcess()
        {
            _isProcessStarted = false;
            _isProccesPaused = true;
        }
    }
}
