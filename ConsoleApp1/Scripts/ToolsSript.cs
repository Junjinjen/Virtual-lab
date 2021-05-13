using JUnity;
using JUnity.Components;
using JUnity.Components.Audio;
using SharpDX;
using System;

namespace App.Scripts
{
    public class ToolsSript : Script
    {
        private GameObject _plank;
        private GameObject _spring;
        private GameObject _ring;
        private GameObject _rope;
        private UIScript _scriptUI;
        private TimerScript _timerScript;

        private float _coefStiffness = 0;
        private float _perimetrRing = 0;
        private float _deltaX = 0;
        private Vector3 _startPositionPlank;
        private Vector3 _startPositionSpring;
        private Vector3 _startPositionRing;
        private Vector3 _startPositionRope;

        private AudioPlayer _music;
        private AudioPlayer _drop;
        private AudioPlayer _up;
        private AudioPlayer _rotate;

        private Random _rnd;

        private bool _detachment = false;

        public void Move(float deltaX)
        {
            var shift = new Vector3(0, deltaX * 4, 0);
            _plank.Position = _startPositionPlank + shift;
            _spring.Scale = new Vector3(1f, 1f, 1f + deltaX / 60);
            _spring.Position = _startPositionSpring + shift;

            if (_deltaX < deltaX && !_detachment)
            {
                _up.Play();
                _spring.Scale = Vector3.One;
                _ring.Position = _startPositionRing + shift;
                _rope.Position = _startPositionRope + shift;
                _detachment = true;
                if (_timerScript.TimerStarted) _timerScript.StopTimer();
                _scriptUI.Arroy.Active = false;
                _rotate.Stop();
            }
            else if(_detachment)
            {
                _spring.Scale = Vector3.One;
                _ring.Position = _startPositionRing + shift;
                _rope.Position = _startPositionRope + shift;
            }

            if (_detachment && deltaX == 0)
            {
                _drop.Play();
                _detachment = false;
            }
        }

        public override void Start()
        {
            _rnd = new Random();
            _plank = Scene.Find("part1.001");
            _spring = Scene.Find("Пружинка");
            _rope = Scene.Find("Ниточки");
            _ring = Scene.Find("Кольцо");
            _startPositionPlank = _plank.Position;
            _startPositionSpring = _spring.Position;
            _startPositionRing = _ring.Position;
            _startPositionRope = _rope.Position;
            _scriptUI = (UIScript)Scene.Find("UI").Script;
            _timerScript = (TimerScript)Scene.Find("Timer").Script;

            _timerScript.StopButton.Click += (o, e) =>
            {
                _scriptUI.Arroy.Active = true;
            };

            _timerScript.ResetButton.Click += (o, e) =>
            {
                _scriptUI.Dx.Value = 0f;
                _scriptUI.Arroy.Active = false;
            };

            _music = Scene.Find("music").GetComponent<AudioPlayer>();
            _music.Repeat = true;
            _music.Volume = 0.06f;
            _music.Play();

            _drop = Scene.Find("drop").GetComponent<AudioPlayer>();
            _up = Scene.Find("up").GetComponent<AudioPlayer>();
            _rotate = Scene.Find("rotate").GetComponent<AudioPlayer>();
            _rotate.Repeat = true;

            _scriptUI.Dpr.ValueChanged += (o, e) =>
            {
                CalculateCoef(_scriptUI.Dpr.Value, _scriptUI.Dvit.Value);
                CalcuelateDeltaX();
            };

            _scriptUI.Dvit.ValueChanged += (o, e) =>
            {
                CalculateCoef(_scriptUI.Dpr.Value, _scriptUI.Dvit.Value);
                CalcuelateDeltaX();
            };

            _scriptUI.D1.ValueChanged += (o, e) =>
            {
                CalculatePerimetr(_scriptUI.D1.Value, _scriptUI.D2.Value);
                CalcuelateDeltaX();
            };

            _scriptUI.D2.ValueChanged += (o, e) =>
            {
                CalculatePerimetr(_scriptUI.D1.Value, _scriptUI.D2.Value);
                CalcuelateDeltaX();
            };

            _scriptUI.CoefChanged += (o, e) =>
            {
                CalcuelateDeltaX();
            };

            CalculateCoef(_scriptUI.Dpr.Value, _scriptUI.Dvit.Value);
            CalculatePerimetr(_scriptUI.D1.Value, _scriptUI.D2.Value);
            CalcuelateDeltaX();
        }

        public override void FixedUpdate(double deltaTime)
        {
            if (_timerScript.TimerStarted)
            {
                _rotate.Play();
                _scriptUI.Dx.Value = _timerScript.Seconds;
                _scriptUI.Arroy.Active =  (int)(_timerScript.Seconds * 1.5) % 2 == 0;
            }

            Move(_scriptUI.Dx.Value);
        }

        private void CalculateCoef(float dpr, float dvit)
        {
            _coefStiffness = 82 * (float)Math.Pow(10, 9) * (float)(Math.Pow(dpr / 1000, 4) / (8 * Math.Pow(dvit / 1000, 3) * 100));
        }

        private void CalculatePerimetr(float d1, float d2)
        {
            _perimetrRing = MathUtil.Pi * (d1 / 1000 + d2 / 1000); 
        } 

        private void CalcuelateDeltaX()
        {
            _deltaX = _scriptUI.GetLiquidValue(out var _) * _perimetrRing * 1000 / _coefStiffness;
            _deltaX *= 1f + (float)((_rnd.NextDouble() - 0.5) / 100);
        }
    }
}
