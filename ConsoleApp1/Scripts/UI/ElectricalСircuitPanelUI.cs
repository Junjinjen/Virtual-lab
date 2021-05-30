using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Surfaces;
using Lab3.Scripts.Interactions;
using SharpDX;

namespace Lab3.Scripts.UI
{
    public class ElectricalСircuitPanelUI : Script
    {
        private TextBox electricalСircuitPanelLabel = new TextBox
        {
            Width = 0.21f,
            Height = 0.08f,
            Position = new Vector2(0.165f, 0.16f),
            Value = "Параметры тока в электрической цепи"
        };

        public FloatTextBox VoltageInput = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.235f, 0.2625f),
            Value = 110f,
            MinValue = 0.1f,
            MaxValue = 200f,
        };

        private TextBox voltageLabel = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.19f, 0.26f),
            Value = "U  = "
        };

        private TextBox voltageMeasurement = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.3f, 0.26f),
            Value = "B"
        };

        public TextBox CurrentAmperage = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.235f, 0.3225f),
            Value = "5.0"
        };

        private TextBox amperageLabel = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.19f, 0.32f),
            Value = "I  = "
        };

        private TextBox amperageMeasurement = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.3f, 0.32f),
            Value = "A"
        };

        private RectangleBackground boxBorder = new RectangleBackground
        {
            Width = 0.22f,
            Height = 0.26f,
            Position = new Vector2(0.16f, 0.15f),
            Background = new SolidColorRectangle
            {
                Color = new Color(141, 161, 152),
            },
            ZOrder = 500,
        };

        private RectangleBackground box = new RectangleBackground
        {
            Width = 0.21f,
            Height = 0.24f,
            Position = new Vector2(0.165f, 0.16f),
            Background = new SolidColorRectangle
            {
                Color = new Color(223, 233, 230),
            },
            ZOrder = 400,
        };

        public override void Start()
        {
            Canvas.RegisterElement(electricalСircuitPanelLabel);
            electricalСircuitPanelLabel.CreateStyleTitle();
            Canvas.RegisterElement(VoltageInput);
            Canvas.RegisterElement(voltageLabel);
            voltageLabel.CreateStyleTextBox();
            Canvas.RegisterElement(voltageMeasurement);
            voltageMeasurement.CreateStyleTextBox();

            Canvas.RegisterElement(CurrentAmperage);
            CurrentAmperage.CreateStyleTitle();
            Canvas.RegisterElement(amperageLabel);
            amperageLabel.CreateStyleTextBox();
            Canvas.RegisterElement(amperageMeasurement);
            amperageMeasurement.CreateStyleTextBox();

            Canvas.RegisterElement(box);
            Canvas.RegisterElement(boxBorder);

            var timer_script = (TimerScript)Scene.Find("Timer").Script;
            timer_script.OnTimerStarted += (o, e) =>
            {
                VoltageInput.Active = false;
            };
            timer_script.OnTimerReseted += (o, e) =>
            {
                VoltageInput.Active = true;
            };
        }
    }
}
