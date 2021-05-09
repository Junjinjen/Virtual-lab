using JUnity;
using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Styling;
using JUnity.Services.UI.Surfaces;
using SharpDX;
using SharpDX.DirectWrite;

namespace Lab2.Scripts
{
    public class UI_Script : Script
    {
        //Water parameters
        public FloatTextBox Water_volume = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.85f, 0.15f),
            Value = 0.5f,
            MinValue = 0.2f,
            MaxValue = 5f,
        };

        TextBox Water_volume_label = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.81f, 0.15f),
            Value = "M   = "
        };

        TextBox Water_volume_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new Vector2(0.816f, 0.16f),
            Value = "в"
        };

        TextBox Water_volume_measurement = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.9f, 0.15f),
            Value = "кг"
        };


        public FloatTextBox Water_temperature = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.85f, 0.20f),
            Value = 20f,
            MinValue = 17f,
            MaxValue = 30f,
        };

        TextBox Water_temperature_label = new TextBox
        {
            Width = 0.04f,
            Height = 0.035f,
            Position = new Vector2(0.81f, 0.20f),
            Value = "T    = "
        };

        TextBox Water_temperature_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new Vector2(0.811f, 0.21f),
            Value = "в"
        };

        TextBox Water_temperature_measurement = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.9f, 0.20f),
            Value = "C"
        };

        TextBox Water_temperature_measurement_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new Vector2(0.911f, 0.197f),
            Value = "0"
        };


        //Body parameters
        public FloatTextBox Body_weight = new FloatTextBox
        {
            Width = 0.05f,
            Height = 0.04f,
            Position = new Vector2(0.79f, 0.35f),
            Value = 0.2f,
            MinValue = 0.1f,
            MaxValue = 1f,
        };
        TextBox Body_weight_label = new TextBox
        {
            Width = 0.05f,
            Height = 0.035f,
            Position = new Vector2(0.745f, 0.35f),
            Value = "M     = "
        };
        TextBox Body_weight_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new Vector2(0.753f, 0.36f),
            Value = "т"
        };
        TextBox Body_weight_measurement = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.82f, 0.35f),
            Value = "кг"
        };


        public FloatTextBox Body_temperature = new FloatTextBox
        {
            Width = 0.05f,
            Height = 0.04f,
            Position = new Vector2(0.79f, 0.4f),
            Value = 20f,
            MinValue = 17f,
            MaxValue = 30f,
        };
        TextBox Body_temperature_label = new TextBox
        {
            Width = 0.05f,
            Height = 0.035f,
            Position = new Vector2(0.745f, 0.4f),
            Value = "T      = "
        };

        TextBox Body_temperature_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new Vector2(0.751f, 0.41f),
            Value = "тг."
        };
        TextBox Body_temperature_measurement = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.82f, 0.4f),
            Value = "C"
        };

        TextBox Body_temperature_measurement_index = new TextBox
        {
            Width = 0.015f,
            Height = 0.015f,
            Position = new Vector2(0.84f, 0.397f),
            Value = "0"
        };


        public FloatTextBox Body_heating_temperature = new FloatTextBox
        {
            Width = 0.05f,
            Height = 0.04f,
            Position = new Vector2(0.79f, 0.45f),
            Value = 55f,
            MinValue = 20f,
            MaxValue = 100f,
        };
        TextBox Body_heating_temperature_label = new TextBox
        {
            Width = 0.05f,
            Height = 0.035f,
            Position = new Vector2(0.745f, 0.45f),
            Value = "T      = "
        };

        TextBox Body_heating_temperature_label_index = new TextBox
        {
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.751f, 0.46f),
            Value = "КОНтт"
        };
        TextBox Body_heating_temperature_measurement = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new Vector2(0.82f, 0.45f),
            Value = "C"
        };

        TextBox Body_heating_temperature_measurement_index = new TextBox
        {
            Width = 0.015f,
            Height = 0.015f,
            Position = new Vector2(0.84f, 0.447f),
            Value = "0"
        };


        TextBox main_label = new TextBox
        {
            Width = 0.9f,
            Height = 0.04f,
            Position = new Vector2(0.03f, 0.02f),
            Value = "ОПРЕДЕЛЕНИЕ УДЕЛЬНОЙ ТЕПЛОЕМКОСТИ ТВЕРДОГО ТЕЛА"
        };

        TextBox first_label = new TextBox
        {
            Width = 0.48f,
            Height = 0.05f,
            Position = new Vector2(0.625f, 0.09f),
            Value = "Параметры калориметра и воды"
        };

        TextBox second_label = new TextBox
        {
            Width = 0.1f,
            Height = 0.05f,
            Position = new Vector2(0.76f, 0.29f),
            Value = "Параметры тела"
        };

        TextBox third_label = new TextBox
        {
            Width = 0.2f,
            Height = 0.05f,
            Position = new Vector2(0.835f, 0.29f),
            Value = "Материал",
        };

        //Timer
        public FloatTextBox timer = new FloatTextBox
        {
            Width = 0.1f,
            Height = 0.08f,
            Position = new Vector2(0.76f, 0.85f),
            Value = 0f,
            ReadOnly = true
        };
        TextBox start = new TextBox
        {
            Width = 0.2f,
            Height = 0.05f,
            Position = new Vector2(0.783f, 0.9f),
            Value = "пуск",
        };
        TextBox pause = new TextBox
        {
            Width = 0.2f,
            Height = 0.05f,
            Position = new Vector2(0.825f, 0.9f),
            Value = "пауза",
        };
        TextBox reset = new TextBox
        {
            Width = 0.2f,
            Height = 0.05f,
            Position = new Vector2(0.867f, 0.9f),
            Value = "сброс",
        };
        Button start_btn = new Button
        {
            Height = 0.05f,
            Width = 0.035f,
            Position = new Vector2(0.865f, 0.865f),
            Text = "►",
        };
        Button pause_btn = new Button
        {
            Height = 0.05f,
            Width = 0.035f,
            Position = new Vector2(0.9075f, 0.865f),
            Text = "||",
        };
        Button reset_btn = new Button
        {
            Height = 0.05f,
            Width = 0.035f,
            Position = new Vector2(0.95f, 0.865f),
            Text = "█",
        };

        //Boxes
        RectangleBackground boxWater = new RectangleBackground
        {
            Width = 0.25f,
            Height = 0.16f,
            Position = new Vector2(0.745f, 0.09f),
            Background = new SolidColorRectangle
            {
                Color = new Color(149, 194, 174),
            },
            ZOrder = 400,
        };

        RectangleBackground boxWaterBorder = new RectangleBackground
        {
            Width = 0.267f,
            Height = 0.18f,
            Position = new Vector2(0.74f, 0.08f),
            Background = new SolidColorRectangle
            {
                Color = new Color(44, 66, 56),
            },
            ZOrder = 500,
        };

        RectangleBackground boxBody = new RectangleBackground
        {
            Width = 0.123f,
            Height = 0.26f,
            Position = new Vector2(0.745f, 0.29f),
            Background = new SolidColorRectangle
            {
                Color = new Color(149, 194, 174),
            },
            ZOrder = 400,
        };

        RectangleBackground boxMaterial = new RectangleBackground
        {
            Width = 0.123f,
            Height = 0.26f,
            Position = new Vector2(0.872f, 0.29f),
            Background = new SolidColorRectangle
            {
                Color = new Color(149, 194, 174),
            },
            ZOrder = 400,
        };

        RectangleBackground boxBodyBorder = new RectangleBackground
        {
            Width = 0.38f,
            Height = 0.28f,
            Position = new Vector2(0.74f, 0.28f),
            Background = new SolidColorRectangle
            {
                Color = new Color(44, 66, 56),
            },
            ZOrder = 500,
        };


        RectangleBackground innerBoxTimer = new RectangleBackground
        {
            Width = 0.24f,
            Height = 0.14f,
            Position = new Vector2(0.75f, 0.82f),
            Background = new SolidColorRectangle
            {
                Color = new Color(60, 82, 120),
            },
            ZOrder = 300,
        };

        RectangleBackground boxTimer = new RectangleBackground
        {
            Width = 0.25f,
            Height = 0.16f,
            Position = new Vector2(0.745f, 0.81f),
            Background = new SolidColorRectangle
            {
                Color = new Color(255, 255, 255),
            },
            ZOrder = 400,
        };

        RectangleBackground boxTimerBorder = new RectangleBackground
        {
            Width = 0.38f,
            Height = 0.18f,
            Position = new Vector2(0.74f, 0.8f),
            Background = new SolidColorRectangle
            {
                Color = new Color(44, 66, 56),
            },
            ZOrder = 500,
        };

        public override void Start()
        {
            
            Canvas.RegisterElement(Water_volume);
            Canvas.RegisterElement(Water_volume_label);
            CreateStyleTextBox(Water_volume_label, 13f);
            Canvas.RegisterElement(Water_volume_label_index);
            CreateStyleTextBox(Water_volume_label_index, 10f);
            Canvas.RegisterElement(Water_volume_measurement);
            CreateStyleTextBox(Water_volume_measurement, 14f);

            Canvas.RegisterElement(Water_temperature);
            Canvas.RegisterElement(Water_temperature_label);
            CreateStyleTextBox(Water_temperature_label, 13f);
            Canvas.RegisterElement(Water_temperature_label_index);
            CreateStyleTextBox(Water_temperature_label_index, 10f);
            Canvas.RegisterElement(Water_temperature_measurement_index);
            CreateStyleTextBox(Water_temperature_measurement_index, 10f);
            Canvas.RegisterElement(Water_temperature_measurement);
            CreateStyleTextBox(Water_temperature_measurement, 14f);

            //

            Canvas.RegisterElement(Body_weight);
            Canvas.RegisterElement(Body_weight_label);
            CreateStyleTextBox(Body_weight_label, 13f);
            Canvas.RegisterElement(Body_weight_label_index);
            CreateStyleTextBox(Body_weight_label_index, 10f);
            Canvas.RegisterElement(Body_weight_measurement);
            CreateStyleTextBox(Body_weight_measurement, 14f);

            Canvas.RegisterElement(Body_temperature);
            Canvas.RegisterElement(Body_temperature_label);
            CreateStyleTextBox(Body_temperature_label, 13f);
            Canvas.RegisterElement(Body_temperature_label_index);
            CreateStyleTextBox(Body_temperature_label_index, 10f);
            Canvas.RegisterElement(Body_temperature_measurement_index);
            CreateStyleTextBox(Body_temperature_measurement_index, 10f);
            Canvas.RegisterElement(Body_temperature_measurement);
            CreateStyleTextBox(Body_temperature_measurement, 14f);

            Canvas.RegisterElement(Body_heating_temperature);
            Canvas.RegisterElement(Body_heating_temperature_label);
            CreateStyleTextBox(Body_heating_temperature_label, 13f);
            Canvas.RegisterElement(Body_heating_temperature_label_index);
            CreateStyleTextBox(Body_heating_temperature_label_index, 10f);
            Canvas.RegisterElement(Body_heating_temperature_measurement_index);
            CreateStyleTextBox(Body_heating_temperature_measurement_index, 10f);
            Canvas.RegisterElement(Body_heating_temperature_measurement);
            CreateStyleTextBox(Body_heating_temperature_measurement, 14f);

            //Labels
            Canvas.RegisterElement(main_label);
            CreateStyleMainLabel(main_label);
            Canvas.RegisterElement(first_label);
            CreateStyleTextBoxLabel(first_label);

            Canvas.RegisterElement(second_label);
            CreateStyleTextBoxLabel(second_label);

            Canvas.RegisterElement(third_label);
            CreateStyleTextBoxLabel(third_label);

            //Timer
            Canvas.RegisterElement(timer);
            CreateStyleTimer(timer);
            Canvas.RegisterElement(start);
            CreateStyleButtonLabel(start);
            Canvas.RegisterElement(pause);
            CreateStyleButtonLabel(pause);
            Canvas.RegisterElement(reset);
            CreateStyleButtonLabel(reset);
            Canvas.RegisterElement(start_btn);
            Canvas.RegisterElement(pause_btn);
            Canvas.RegisterElement(reset_btn);
            //CreateStyleTextBoxLabel(third_label);

            //Boxes
            Canvas.RegisterElement(boxMaterial);
            Canvas.RegisterElement(boxWater);
            Canvas.RegisterElement(boxBody);
            Canvas.RegisterElement(boxWaterBorder);
            Canvas.RegisterElement(boxBodyBorder);
            Canvas.RegisterElement(boxTimerBorder);
            Canvas.RegisterElement(boxTimer);
            Canvas.RegisterElement(innerBoxTimer);
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
            if (Engine.Instance.InputManager.IsKeyJustPressed(SharpDX.DirectInput.Key.Escape))
            {
                Engine.Instance.Stop();
            }
        }

        private void CreateStyleButtonLabel(TextBox textBox)
        {
            textBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.TextStyle = new DisablingTextStyle()
            {
                Color = new Color(255, 255, 255, 255),
                TextFormat = new JUnity.Services.UI.Styling.TextFormat
                {
                    FontFamily = "TimesNewRoman",
                    FontSize = 14f,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Normal,
                    FontWeight = FontWeight.Bold,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                }
            };
        }

        private void CreateStyleTimer(FloatTextBox floatTextBox)
        {
            floatTextBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 255) };
            floatTextBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
            floatTextBox.Style.TextStyle = new DisablingTextStyle()
            {
                Color = new Color(39, 113, 242, 255),
                TextFormat = new JUnity.Services.UI.Styling.TextFormat
                {
                    FontFamily = "Tahoma",
                    FontSize = 25f,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Normal,
                    FontWeight = FontWeight.Bold,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                }
            };
        }

        private void CreateStyleTextBox(TextBox textBox, float frontSize)
        {
            textBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.TextStyle = new DisablingTextStyle()
            {
                Color = new Color(0, 0, 0, 255),
                TextFormat = new JUnity.Services.UI.Styling.TextFormat
                {
                    FontFamily = "TimesNewRoman",
                    FontSize = frontSize,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Normal,
                    FontWeight = FontWeight.Bold,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                }
            };
        }

        //public override void Update(double deltaTime)
        //{
        //    System.Console.WriteLine(Water_volume.Value);
        //}

        private void CreateStyleMainLabel(TextBox textBox)
        {
            textBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.TextStyle = new DisablingTextStyle()
            {
                Color = new Color(200, 0, 80, 255),
                TextFormat = new JUnity.Services.UI.Styling.TextFormat
                {
                    FontFamily = "TimesNewRoman",
                    FontSize = 28f,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Normal,
                    FontWeight = FontWeight.Bold,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                }
            };
        }
        private void CreateStyleTextBoxLabel(TextBox textBox)
        {
            textBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.TextStyle = new DisablingTextStyle()
            {
                Color = new Color(255, 0, 0, 255),
                TextFormat = new JUnity.Services.UI.Styling.TextFormat
                {
                    FontFamily = "TimesNewRoman",
                    FontSize = 18f,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Normal,
                    FontWeight = FontWeight.DemiBold,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                }
            };
        }

        //private void CreateStyleReadonlyBox(FloatTextBox textBox)
        //{
        //    textBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 0) };
        //    textBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
        //    textBox.Style.TextStyle = new DisablingTextStyle()
        //    {
        //        Color = new Color(255, 0, 0, 255),
        //        TextFormat = new JUnity.Services.UI.Styling.TextFormat
        //        {
        //            FontFamily = "TimesNewRoman",
        //            FontSize = 13.0f,
        //            FontStretch = FontStretch.Normal,
        //            FontStyle = FontStyle.Normal,
        //            FontWeight = FontWeight.Bold,
        //            ParagraphAlignment = ParagraphAlignment.Center,
        //            TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
        //        }
        //    };
        //}
    }
}
