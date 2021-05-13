using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Surfaces;
using Lab3.Scripts.Interactions;
using SharpDX;

namespace Lab3.Scripts.UI
{
    public class WaterPanelUI : Script
    {
        TextBox WaterPanelLabel = new TextBox
        {
            Width = 0.2f,
            Height = 0.035f,
            Position = new Vector2(0.75f, 0.1f),
            Value = "Параметры воды"
        };

        public FloatTextBox WaterTemparatureInput = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.815f, 0.1525f),
            Value = 0.5f,
            MinValue = -20f,
            MaxValue = 100f,
        };

        TextBox startWaterTemperatureLabel = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.765f, 0.15f),
            Value = "t   = "
        };

        TextBox startWaterTemperatureLabelIndex = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.762f, 0.157f),
            Value = "0"
        };

        TextBox startWaterTemperatureMeasurement = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.890f, 0.15f),
            Value = "C"
        };

        TextBox startWaterTemperatureMeasurementIndex = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.883f, 0.143f),
            Value = "0"
        };

        public TextBox CurrentWaterTemperature = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.83f, 0.21f),
            Value = "19.8"
        };

        public TextBox CurrentWaterTemperatureTermometer = new TextBox
        {
            Width = 0.07f,
            Height = 0.07f,
            Position = new Vector2(0.05f, 0.76f),
            Value = "19.8"
        };

        TextBox currentWaterTemperatureTermometerIndex = new TextBox
        {
            Width = 0.02f,
            Height = 0.05f,
            Position = new Vector2(0.115f, 0.74f),
            Value = "o"
        };

        TextBox currentWaterTemperatureLabel = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.765f, 0.21f),
            Value = "t   = "
        };

        TextBox currentWaterTemperatureMeasurement = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.890f, 0.21f),
            Value = "C"
        };

        TextBox currentWaterTemperatureMeasurementIndex = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.883f, 0.203f),
            Value = "0"
        };

        public FloatTextBox WaterVolumeInput = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.815f, 0.2705f),
            Value = 0.5f,
            MinValue = 0.1f,
            MaxValue = 3f,
        };

        TextBox waterVolumeLabel = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.765f, 0.268f),
            Value = "V   = "
        };

        TextBox waterVolumeMeasurement = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.885f, 0.268f),
            Value = "л"
        };

        RectangleBackground boxBorder = new RectangleBackground
        {
            Width = 0.22f,
            Height = 0.26f,
            Position = new Vector2(0.74f, 0.08f),
            Background = new SolidColorRectangle
            {
                Color = new Color(141, 161, 152),
            },
            ZOrder = 500,
        };

        RectangleBackground box = new RectangleBackground
        {
            Width = 0.21f,
            Height = 0.24f,
            Position = new Vector2(0.745f, 0.09f),
            Background = new SolidColorRectangle
            {
                Color = new Color(223, 233, 230),
            },
            ZOrder = 400,
        };

        public override void Start()
        {
            Canvas.RegisterElement(WaterPanelLabel);
            WaterPanelLabel.CreateStyleTitle();

            Canvas.RegisterElement(WaterTemparatureInput);
            WaterTemparatureInput.Value = 18f;
            Canvas.RegisterElement(startWaterTemperatureLabel);
            startWaterTemperatureLabel.CreateStyleTextBox(24f);
            Canvas.RegisterElement(startWaterTemperatureLabelIndex);
            startWaterTemperatureLabelIndex.CreateStyleTextBox(16f);
            Canvas.RegisterElement(startWaterTemperatureMeasurement);
            startWaterTemperatureMeasurement.CreateStyleTextBox(24f);
            Canvas.RegisterElement(startWaterTemperatureMeasurementIndex);
            startWaterTemperatureMeasurementIndex.CreateStyleTextBox(16f);

            Canvas.RegisterElement(CurrentWaterTemperature);
            CurrentWaterTemperature.CreateStyleTitle();
            Canvas.RegisterElement(CurrentWaterTemperatureTermometer);
            CurrentWaterTemperatureTermometer.CreateStyleTitle(50f);
            Canvas.RegisterElement(currentWaterTemperatureTermometerIndex);
            currentWaterTemperatureTermometerIndex.CreateStyleTitle(40f);
            Canvas.RegisterElement(currentWaterTemperatureLabel);
            currentWaterTemperatureLabel.CreateStyleTextBox(24f);
            Canvas.RegisterElement(currentWaterTemperatureMeasurement);
            currentWaterTemperatureMeasurement.CreateStyleTextBox(24f);
            Canvas.RegisterElement(currentWaterTemperatureMeasurementIndex);
            currentWaterTemperatureMeasurementIndex.CreateStyleTextBox(16f);

            Canvas.RegisterElement(WaterVolumeInput);
            WaterVolumeInput.Value = 3f;
            Canvas.RegisterElement(waterVolumeLabel);
            waterVolumeLabel.CreateStyleTextBox(24f);
            Canvas.RegisterElement(waterVolumeMeasurement);
            waterVolumeMeasurement.CreateStyleTextBox(24f);

            Canvas.RegisterElement(box);
            Canvas.RegisterElement(boxBorder);

            var timer_script = (TimerScript)Scene.Find("Timer").Script;
            timer_script.OnTimerStarted += (o, e) =>
            {
                WaterTemparatureInput.Active = false;
                WaterVolumeInput.Active = false;
            };
            timer_script.OnTimerReseted += (o, e) =>
            {
                WaterTemparatureInput.Value = WaterTemparatureInput.Value;
                WaterTemparatureInput.Active = true;
                WaterVolumeInput.Active = true;
            };
        }
    }
}
