using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Surfaces;
using SharpDX;

namespace Lab3.Scripts.UI
{
    public class TimerPanelUI : Script
    {
        public TextBox CurrentTime = new TextBox
        {
            Width = 0.122f,
            Height = 0.062f,
            Position = new Vector2(0.785f, 0.659f),
            Value = "0,000"
        };

        private RectangleBackground timerBoxBorder = new RectangleBackground
        {
            Width = 0.126f,
            Height = 0.07f,
            Position = new Vector2(0.783f, 0.655f),
            Background = new SolidColorRectangle
            {
                Color = new Color(100, 141, 151),
            },
            ZOrder = 500,
        };

        private RectangleBackground timerBox = new RectangleBackground
        {
            Width = 0.122f,
            Height = 0.062f,
            Position = new Vector2(0.785f, 0.659f),
            Background = new SolidColorRectangle
            {
                Color = new Color(26, 29, 28),
            },
            ZOrder = 400,
        };

        public override void Start()
        {
            Canvas.RegisterElement(CurrentTime);
            CurrentTime.CreateStyleTextBox(46f);
            CurrentTime.ChangeColorStyleTextBox(new Color(46, 156, 182));
            CurrentTime.ChangeFontStyleTextBox(SharpDX.DirectWrite.FontStyle.Normal);

            Canvas.RegisterElement(timerBox);
            Canvas.RegisterElement(timerBoxBorder);
        }
    }
}
