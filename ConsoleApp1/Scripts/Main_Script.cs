using JUnity;
using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Styling;
using JUnity.Services.UI.Surfaces;
using SharpDX;
using SharpDX.DirectInput;
using SharpDX.DirectWrite;
using System;

namespace Lab2.Scripts
{
    public class Main_Script : Script
    {
        const float WATER_TEMP_MIN = 17;
        const float WATER_TEMP_MAX = 30;
        const float OBJECT_TEMP_MIN = 17;
        const float OBJECT_TEMP_MAX = 100;
        TextBox water_temperature = new TextBox
        {
            Width = 0.06f,
            Height = 0.04f,
            Position = new Vector2(0.029f, 0.72f),
            Value = "20",
        };
        TextBox object_temperature = new TextBox
        {
            Width = 0.06f,
            Height = 0.04f,
            Position = new Vector2(0.64f, 0.72f),
            Value = "20",
        };

        TextBox temperature_label = new TextBox
        {
            Width = 0.05f,
            Height = 0.04f,
            Position = new Vector2(0.69f, 0.72f),
            Value = "°C",
        };

        TextBox temperature_label2 = new TextBox
        {
            Width = 0.05f,
            Height = 0.04f,
            Position = new Vector2(0.077f, 0.72f),
            Value = "°C",
        };

        UI_Script ui_script;

        private GameObject temp1;
        private GameObject temp2;
        private GameObject volumeWater;
        private GameObject gameObject;

        public void SetCurrentTemperature(GameObject gameObject, float currentTemperature, int step)
        {
            var scale = 100 + (currentTemperature + 23) * step;
            gameObject.Scale = new Vector3(100f, 100f, scale);
        }

        public void SetCurrentWater(GameObject gameObject, float currentVolume, int step)
        {
            var scale = 5 + (currentVolume + 23) * step;
            gameObject.Scale = new Vector3(100f, 100f, scale);
        }

        public void SetScaleBody(GameObject gameObject, float currentWeight, float step)
        {
            var scale = currentWeight * step + 1;
            gameObject.Scale = new Vector3(scale, scale, scale);
        }

        private float water_temp = 20;
        private float object_temp = 20;
        private float water_volume = 1;
        private float C_tt = 540;
        private float A_tt = 0.05f;
        private float S_tt = 0.015f;
        private float T_end_body;
        private float M_water;
        private float T_water;
        private float M_body;
        public override void Start()
        {
            ui_script = (UI_Script)Scene.Find("UI").Script;
            temp1 = Scene.Find("ColumnWater");
            temp2 = Scene.Find("ColumnObject");
            volumeWater = Scene.Find("Water");
            gameObject = Scene.Find("Object");
            Canvas.RegisterElement(water_temperature);
            CreateStyleTextBox(water_temperature, 40f);
            Canvas.RegisterElement(temperature_label);
            CreateStyleTextBox(temperature_label, 40f);
            Canvas.RegisterElement(object_temperature);
            CreateStyleTextBox(object_temperature, 40f);
            Canvas.RegisterElement(temperature_label2);
            CreateStyleTextBox(temperature_label2, 40f);


            ui_script.Body_weight.ValueChanged += (o, e) =>
            {
                SetScaleBody(gameObject, e.Value, 0.3f);
            };

            SetScaleBody(gameObject, ui_script.Body_weight.Value, 0.3f);
        }

        
        public override void FixedUpdate(double deltaTime)
        {
            water_temp = ui_script.Water_temperature.Value;
            object_temp = ui_script.Body_temperature.Value;
            T_end_body = ui_script.Body_heating_temperature.Value;
            M_water = ui_script.Water_volume.Value;
            T_water = ui_script.Water_temperature.Value;
            M_body = ui_script.Body_weight.Value;
            water_volume = ui_script.Water_volume.Value;
            Console.WriteLine(M_body);

            //var temperature_end = (C_tt * 0.9 * T_end_body + 750 * 0.3 * 20 + 4200 * M_water * T_water) /
            //    (750 * 0.3 + 800 * M_body + 4200 * M_water);

            //var s = C_tt * M_body * (T_end_body - temperature_end) * A_tt * 0.01 /
            //    S_tt * 0.56;

            //System.Console.WriteLine(s);
            //if body in calorimeter
            //while (water_temp < temperature_end)
            //{
            //    water_temp += 0.1f;// * (float)s;
            //    water_temperature.Value = ((float)Math.Round(water_temp, 1)).ToString();
            //}

            //while (object_temp < T_end_body)
            //{
            //    object_temp += 0.1f; //* (float)s;
            //    object_temperature.Value = ((float)Math.Round(object_temp, 1)).ToString();
            //}



            //if (Engine.Instance.InputManager.IsKeyPressed(Key.A))
            //{
            //    water_volume -= 0.5f;
            //}
            //if (Engine.Instance.InputManager.IsKeyPressed(Key.Q))
            //{
            //    water_volume += 0.5f;
            //}
            //System.Console.WriteLine(water_volume);
            //if (Engine.Instance.InputManager.IsKeyPressed(Key.S))
            //{
            //    object_temp -= 0.1f;
            //    object_temperature.Value = ((float)Math.Round(object_temp, 1)).ToString();
            //}
            //if (Engine.Instance.InputManager.IsKeyPressed(Key.W))
            //{
            //    object_temp += 0.1f;
            //    object_temperature.Value = ((float)Math.Round(object_temp, 1)).ToString();
            //}


            //if (Engine.Instance.InputManager.IsKeyPressed(Key.A))
            //{
            //    water_temp -= 0.1f;
            //    water_temperature.Value = ((float)Math.Round(water_temp, 1)).ToString();
            //}
            //if (Engine.Instance.InputManager.IsKeyPressed(Key.Q))
            //{
            //    water_temp += 0.1f;
            //    water_temperature.Value = ((float)Math.Round(water_temp, 1)).ToString();
            //}

            //if (Engine.Instance.InputManager.IsKeyPressed(Key.S))
            //{
            //    object_temp -= 0.1f;
            //    object_temperature.Value = ((float)Math.Round(object_temp, 1)).ToString();
            //}
            //if (Engine.Instance.InputManager.IsKeyPressed(Key.W))
            //{
            //    object_temp += 0.1f;
            //    object_temperature.Value = ((float)Math.Round(object_temp, 1)).ToString();
            //}
            SetCurrentTemperature(temp1, water_temp, 37);
            SetCurrentTemperature(temp2, object_temp, 37);
            //SetCurrentWater(volumeWater, water_volume, 100);
            //SetScaleBody(gameObject, weight_body, 1);
            //System.Console.WriteLine(weight_body);
            RadioButtonValue();

        }
        private void RadioButtonValue()
        {
            if(ui_script.CastIron_btn.Checked)
            {
                C_tt = 540;
                A_tt = 0.05f;
                S_tt = 0.015f;
            }
            if(ui_script.Glass_btn.Checked)
            {
                C_tt = 670;
                A_tt = 0.071f;
                S_tt = 0.03f;
            }
            if (ui_script.Graphite_btn.Checked)
            {
                C_tt = 750;
                A_tt = 0.074f;
                S_tt = 0.032f;
            }
            if (ui_script.Brick_btn.Checked)
            {
                C_tt = 850;
                A_tt = 0.079f;
                S_tt = 0.038f;
            }
            if (ui_script.Marble_btn.Checked)
            {
                C_tt = 920;
                A_tt = 0.069f;
                S_tt = 0.029f;
            }
            if (ui_script.Tree_btn.Checked)
            {
                C_tt = 1300;
                A_tt = 0.12f;
                S_tt = 0.086f;
            }
            if (ui_script.Textolite_btn.Checked)
            {
                C_tt = 1470;
                A_tt = 0.085f;
                S_tt = 0.043f;
            }
        }

        private void CreateStyleTextBox(TextBox textBox, float frontSize)
        {
            textBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.FocusedBorder = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.TextStyle = new DisablingTextStyle()
            {
                Color = new Color(0, 0, 0, 255),
                TextFormat = new JUnity.Services.UI.Styling.TextFormat
                {
                    FontFamily = "TimesNewRoman",
                    FontSize = frontSize,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Normal,
                    FontWeight = FontWeight.DemiBold,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                }
            };
        }
    }
}
