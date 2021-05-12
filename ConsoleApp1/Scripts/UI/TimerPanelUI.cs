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
            Width = 0.14f,
            Height = 0.06f,
            Position = new Vector2(0.805f, 0.685f),
            Value = "0,000"
        };

        RectangleBackground timerBoxBorder = new RectangleBackground
        {
            Width = 0.15f,
            Height = 0.08f,
            Position = new Vector2(0.80f, 0.675f),
            Background = new SolidColorRectangle
            {
                Color = new Color(100, 141, 151),
            },
            ZOrder = 500,
        };

        RectangleBackground timerBox = new RectangleBackground
        {
            Width = 0.14f,
            Height = 0.06f,
            Position = new Vector2(0.805f, 0.685f),
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
