using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.Input;
using Lab3.Scripts.UI;
using System.Diagnostics;

namespace Lab3.Scripts.Interactions
{
    public class TimerScript : Script
    {
        private Stopwatch _timer;
        private TextBox _currentTime;

        private bool _isUpdate = false;

        public override void Start()
        {
            _timer = new Stopwatch();
            var ui_script = (TimerPanelUI)Scene.Find("TimerUI").Script;
            _currentTime = ui_script.CurrentTime;

            MouseGrip.OnLeftClickObject += (o, e) =>
            {
                switch (e.Object?.Name)
                {
                    case "Play":
                        _timer.Start();
                        _isUpdate = true;
                        break;
                    case "Pause":
                        _timer.Stop();
                        break;
                    case "Stop":
                        _timer.Reset();
                        _isUpdate = false;
                        break;
                }
            };
        }

        public override void Update(double deltaTime)
        {
            if (_isUpdate)
                _currentTime.Value = string.Format("{0:f2}", _timer.ElapsedMilliseconds / 1000f);
            else
                _currentTime.Value = "0,00";
        }
    }
}
