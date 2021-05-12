using JUnity.Components;
using JUnity.Components.Physics;
using JUnity.Components.UI;
using JUnity.Services.Input;
using Lab3.Scripts.UI;
using SharpDX;
using System;

namespace Lab3.Scripts.Interactions
{
    public class WeigherScript : Script
    {
        private MetalScript _metalScript;
        private PointMovement _moveMetalAnimation;
        private TextBox _currentWeight;

        public override void Start()
        {
            var ui_script = (MetalPanelUI)Scene.Find("MetalUI").Script;
            _currentWeight = ui_script.CurrentWeight;
            _metalScript = (MetalScript)Scene.Find("Metal").Script;

            _moveMetalAnimation = new PointMovement(_metalScript.Object, _metalScript.Object.Position);
            _moveMetalAnimation.Points.Add(
                new Vector3(_metalScript.Object.Position.X, Object.Position.Y + 1.5f, _metalScript.Object.Position.Z));
            _moveMetalAnimation.Points.Add(Object.Position + Vector3.UnitY * 1.5f);
            _moveMetalAnimation.Points.Add(Object.Position + Vector3.UnitY * 1.35f);
            _moveMetalAnimation.DefaultSpeed = 2f;
            _moveMetalAnimation.OnAnimationEnd += UpdateWeigherWithMetal;

            MouseGrip.OnLeftClickObject += (o, e) =>
            {
                if(e.Object?.Name == Object.Children[0].Name && _metalScript.IsSelected)
                {
                    _metalScript.IsMoving = true;
                    _moveMetalAnimation.Reset();              
                    _moveMetalAnimation.Start();
                }
            };

            MouseGrip.OnRightClickObject += (o, e) =>
            {
                _currentWeight.Value = "0,000";
            };
        }

        public override void Update(double deltaTime)
        {
            _moveMetalAnimation.Move((float)deltaTime);
        }

        private void UpdateWeigherWithMetal(object sender, EventArgs e)
        {
            _metalScript.IsWithOtherObject = true;
            _metalScript.IsMoving = false;

            _currentWeight.Value = string.Format("{0:f3}", _metalScript.Weight);
        }
    }
}
