using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Styling;
using JUnity.Services.UI.Surfaces;
using SharpDX;
using SharpDX.DirectWrite;

namespace Lab3.Scripts.UI
{
    public class MainUI : Script
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
    }
}
