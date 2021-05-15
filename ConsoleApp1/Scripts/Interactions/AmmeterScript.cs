﻿using JUnity.Components;
using JUnity.Components.UI;
using Lab3.Scripts.UI;
using SharpDX;

namespace Lab3.Scripts.Interactions
{
    public class AmmeterScript : Script
    {
        private TextBox _currentAmperage;

        private const float RESISTANCE = 22f;
        private const float MIN_ANGLE = -MathUtil.Pi / 3;
        private const float MAX_ANGLE = MathUtil.Pi / 3;

        private const float MAX_VALUE = 10f;

        private Quaternion _baseRotation;

        public override void Start()
        {
            _baseRotation = Object.Rotation;
            var ui_script = (ElectricalСircuitPanelUI)Scene.Find("ElectricalСircuitUI").Script;
            _currentAmperage = ui_script.CurrentAmperage;
        }

        public void UpdatePointer(float voltage)
        {
            var amperage = voltage / RESISTANCE;

            if(amperage > MAX_VALUE)
            {
                amperage = MAX_VALUE;
            }
            else if(amperage < 0)
            {
                amperage = 0;
            }

            amperage += Lab3.Scene.Random.NextFloat(-0.0015f, 0.0015f) * amperage;

            var angle = MIN_ANGLE + amperage * (2 * MAX_ANGLE) / MAX_VALUE;
            Object.Rotation = _baseRotation * Quaternion.RotationAxis(-Vector3.UnitY, angle);
            _currentAmperage.Value = string.Format("{0:f1}", amperage);
        }
    }
}
