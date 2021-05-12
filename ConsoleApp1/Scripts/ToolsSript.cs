using JUnity;
using JUnity.Components;
using SharpDX;
using System;

namespace App.Scripts
{
    public class ToolsSript : Script
    {
        GameObject plank;
        GameObject spring;
        GameObject ring;
        GameObject rope;
        UI scriptUI;

        float _coefStiffness = 0; 
        float _perimetrRing = 0;
        float _deltaX = 0;
        Vector3 startPositionPlank;
        Vector3 startPositionSpring;
        Vector3 startPositionRing;
        Vector3 startPositionRope;

        bool _detachment = false;

        public void Move(float deltaX)
        {
            var shift = new Vector3(0, deltaX * 4, 0);
            plank.Position = startPositionPlank + shift;
            spring.Scale = new Vector3(1f, 1f, 1f + deltaX / 60);
            spring.Position = startPositionSpring + shift;

            if (_deltaX < deltaX && !_detachment)
            {
                spring.Scale = Vector3.One;
                ring.Position = startPositionRing + shift;
                rope.Position = startPositionRope + shift;
                _detachment = true;
            }
            else if(_detachment)
            {
                spring.Scale = Vector3.One;
                ring.Position = startPositionRing + shift;
                rope.Position = startPositionRope + shift;
            }

            if (_detachment && deltaX == 0) _detachment = false;
        }

        public override void Start()
        {
            plank = Scene.Find("part1.001");
            spring = Scene.Find("Пружинка");
            rope = Scene.Find("Ниточки");
            ring = Scene.Find("Кольцо");
            startPositionPlank = plank.Position;
            startPositionSpring = spring.Position;
            startPositionRing = ring.Position;
            startPositionRope = rope.Position;
            scriptUI = (UI)Scene.Find("UI").Script;

            scriptUI.Dpr.ValueChanged += (o, e) =>
            {
                CalculateCoef(scriptUI.Dpr.Value, scriptUI.Dvit.Value);
                CalcuelateDeltaX();
            };

            scriptUI.Dvit.ValueChanged += (o, e) =>
            {
                CalculateCoef(scriptUI.Dpr.Value, scriptUI.Dvit.Value);
                CalcuelateDeltaX();
            };

            scriptUI.D1.ValueChanged += (o, e) =>
            {
                CalculatePerimetr(scriptUI.D1.Value, scriptUI.D2.Value);
                CalcuelateDeltaX();
            };

            scriptUI.D2.ValueChanged += (o, e) =>
            {
                CalculatePerimetr(scriptUI.D1.Value, scriptUI.D2.Value);
                CalcuelateDeltaX();
            };

            CalculateCoef(scriptUI.Dpr.Value, scriptUI.Dvit.Value);
            CalculatePerimetr(scriptUI.D1.Value, scriptUI.D2.Value);
            CalcuelateDeltaX();
        }

        public override void FixedUpdate(double deltaTime)
        {
            Move(scriptUI.Dx.Value);
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
            _deltaX = scriptUI.GetLiquidValue(out var _) * _perimetrRing * 1000 / _coefStiffness;
        }
    }
}
