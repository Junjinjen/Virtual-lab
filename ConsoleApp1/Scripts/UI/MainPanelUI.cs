using JUnity;
using JUnity.Components;
using JUnity.Components.UI;
using SharpDX;

namespace Lab3.Scripts.UI
{
    public class MainPanelUI : Script
    {
        private TextBox titleLabel = new TextBox
        {
            Width = 0.9f,
            Height = 0.04f,
            Position = new Vector2(0.03f, 0.02f),
            Value = "ОПРЕДЕЛЕНИЕ УДЕЛЬНОЙ ТЕПЛОЕМКОСТИ МЕТАЛЛОВ"
        };

        public override void Start()
        {
            Canvas.RegisterElement(titleLabel);
            titleLabel.CreateStyleMainLabel();
        }

        public override void Update(double deltaTime)
        {
            if (Engine.Instance.InputManager.IsKeyJustPressed(SharpDX.DirectInput.Key.Escape))
            {
                Engine.Instance.Stop();
            }
        }
    }
}
