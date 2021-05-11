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

        float k = 0; //Коэффициент жесткости витой цилиндрической пружины
        float l = 0;

        public override void Start()
        {
            plank = Scene.Find("part1.001");
            spring = Scene.Find("Пружинка");
            rope = Scene.Find("Ниточки");
            ring = Scene.Find("Кольцо");
            scriptUI = (UI)Scene.Find("UI").Script;

            scriptUI.Dpr.ValueChanged += (o, e) =>
            {
                CalculateK(scriptUI.Dpr.Value, scriptUI.Dvit.Value);
            };

            scriptUI.Dvit.ValueChanged += (o, e) =>
            {
                CalculateK(scriptUI.Dpr.Value, scriptUI.Dvit.Value);
            };

            scriptUI.D1.ValueChanged += (o, e) =>
            {
                CalculateL(scriptUI.D1.Value, scriptUI.D2.Value);
            };

            scriptUI.D2.ValueChanged += (o, e) =>
            {
                CalculateL(scriptUI.D1.Value, scriptUI.D2.Value);
            };

        }

        private void CalculateK(float dpr, float dvit)
        {
            k = 82 * (float)Math.Pow(10, 9) * (float)(Math.Pow(dpr, 4) / (8 * Math.Pow(dvit, 3) * 100));
        }

        private void CalculateL(float d1, float d2)
        {
            l = MathUtil.Pi * (d1 + d2) / 10000; 
        } 

        private void CalcuelateF(float a, float l)
        {

        }
    }
}
