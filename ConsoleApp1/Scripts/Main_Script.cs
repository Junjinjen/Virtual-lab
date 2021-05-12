using JUnity;
using JUnity.Components;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Components.UI;
using JUnity.Services.Input;
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
        public bool TimerOn;
        private float water_temp = 20;
        private float object_temp = 20;
        private float water_volume = 1;
        private float C_tt = 540;
        private float A_tt = 0.05f;
        private float S_tt = 0.015f;
        private float T_end_body;
        private float M_body;
        private float p;

        private double temperature_end;
        private double s;
        private readonly Vector3 positionCup = new Vector3(-1.4f, -0.65f, 0);
        private readonly Vector3 positionCalorimeter = new Vector3(-4.6f, -1.9f, 0);
        UI_Script ui_script;
        Timer_Script timer_script;
        private GameObject temp1;
        private GameObject temp2;
        private GameObject volumeWater;
        private GameObject solidBody;
        private Vector3 startPositionBody;

        private bool isCoolDown = false;
        private float stepCoolDown;
        private float startTemperature;

        Collider currentColision = null;

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

        
        public void SetCurrentTemperature(GameObject gameObject, float currentTemperature, float step)
        {
            var scale = 100 + (currentTemperature + 23) * step;
            gameObject.Scale = new Vector3(100f, 100f, scale);
        }

        public void SetCurrentWater(GameObject gameObject, float currentVolume)
        {
            var scale = 3.5f + currentVolume * 5.1f;
            gameObject.Scale = new Vector3(1f, 1f, scale);
        }

        public void SetScaleBody(GameObject gameObject, float currentWeight, float step)
        {
            var scale = currentWeight * step + 1;
            gameObject.Scale = new Vector3(scale, scale, scale);
        }
        
        public override void Start()
        {
            #region StartFields
            ui_script = (UI_Script)Scene.Find("UI").Script;
            timer_script = (Timer_Script)Scene.Find("Timer").Script;
            temp1 = Scene.Find("ColumnWater");
            temp2 = Scene.Find("ColumnObject");
            volumeWater = Scene.Find("WaterCalorimeter");
            solidBody = Scene.Find("Object");
            startPositionBody = solidBody.Position;
            var rigidBody = solidBody.GetComponent<Rigidbody>();
            rigidBody.TriggerEnter += (o, e) =>
            {
                if (e.TriggeredCollider.Name == "Sol" && e.OtherCollider.Name == "CalorimeterCollider" &&
                currentColision != e.OtherCollider)
                {
                    currentColision = e.OtherCollider;
                    solidBody.Position = new Vector3(-1.4f, -0.65f, 0);
                    MouseGrip.OnEndClick(null, new MouseClickEventArgs(MouseKey.Left, Vector2.Zero));
                }
                if (e.TriggeredCollider.Name == "Sol" && e.OtherCollider.Name == "MeasuringCupCollider" &&
                currentColision != e.OtherCollider)
                {
                    currentColision = e.OtherCollider;
                    solidBody.Position = new Vector3(-4.6f, -1.9f, 0);
                    MouseGrip.OnEndClick(null, new MouseClickEventArgs(MouseKey.Left, Vector2.Zero));
                }
            };

            Canvas.RegisterElement(water_temperature);
            CreateStyleTextBox(water_temperature, 40f);
            Canvas.RegisterElement(temperature_label);
            CreateStyleTextBox(temperature_label, 40f);
            Canvas.RegisterElement(object_temperature);
            CreateStyleTextBox(object_temperature, 40f);
            Canvas.RegisterElement(temperature_label2);
            CreateStyleTextBox(temperature_label2, 40f);

            #endregion

            #region Water

            ui_script.Water_volume.ValueChanged += (o, e) =>
            {
                SetCurrentWater(volumeWater, e.Value);
                water_volume = e.Value;
                CalculateEndTemperature();
            };
            water_volume = ui_script.Water_volume.Value;
            SetCurrentWater(volumeWater, ui_script.Water_volume.Value);

            ui_script.Water_temperature.ValueChanged += (o, e) =>
            {
                water_temperature.Value = e.Value.ToString();
                SetCurrentTemperature(temp1, e.Value, 37f);
                water_temp = e.Value;
                CalculateEndTemperature();
            };
            SetCurrentTemperature(temp1, ui_script.Water_temperature.Value, 37f);
            water_temp = ui_script.Water_temperature.Value;
            #endregion

            #region SolidBody

            ui_script.Body_weight.ValueChanged += (o, e) =>
            {
                SetScaleBody(solidBody, e.Value, 0.3f);
                M_body = e.Value;
                CalculateAtt_Stt();
                CalculateEndTemperature();
            };
            M_body = ui_script.Body_weight.Value;
            SetScaleBody(solidBody, ui_script.Body_weight.Value, 0.3f);

            ui_script.Body_temperature.ValueChanged += (o, e) =>
            {
                SetCurrentTemperature(temp2, e.Value, 37f);
                object_temperature.Value = e.Value.ToString();
                object_temp = e.Value;
            };
            object_temp = ui_script.Body_temperature.Value;
            SetCurrentTemperature(temp2, object_temp, 37f);

            ui_script.Body_heating_temperature.ValueChanged += (o, e) =>
            {
                T_end_body = e.Value;
                CalculateEndTemperature();
            };

            T_end_body = ui_script.Body_heating_temperature.Value;
            #endregion

            SetupRadioButtonValue();
            CalculateAtt_Stt();
            CalculateEndTemperature();
        }

        private void CalculateEndTemperature()
        {
            temperature_end = (C_tt * 0.9 * T_end_body + 750 * 0.3 * 20 + 4200 * water_volume * water_temp) /
                (750 * 0.3 + 800 * M_body + 4200 * water_volume);
            CalculateTime();
        }

        private void CalculateAtt_Stt()
        {
            A_tt = (float)Math.Pow(M_body / p, (1 / 3));
            S_tt = A_tt * A_tt * 6;
            CalculateTime();
        }
        private void CalculateTime()
        {
            s = C_tt * M_body * (T_end_body - temperature_end) * A_tt * 0.01 /
                S_tt * 0.56;
        }


        public override void FixedUpdate(double deltaTime)
        {
            if(MouseGrip.CurrentGameObject != solidBody && solidBody.Position != positionCup &&
                solidBody.Position != positionCalorimeter)
            {
                solidBody.Position = startPositionBody;
                currentColision = null;
            }

            if(timer_script.TimerOn && solidBody.Position == positionCup)
            {
                if(object_temp < T_end_body)
                {
                    object_temp = ui_script.Body_temperature.Value + timer_script.Seconds;
                    SetCurrentTemperature(temp2, object_temp, 37f);
                    object_temperature.Value = object_temp.ToString("0.0");
                }
                if (object_temp > T_end_body)
                {
                    object_temp = T_end_body;
                    object_temperature.Value = object_temp.ToString("0.0");
                }
            }

            if (solidBody.Position == positionCalorimeter)
            {
                if (!isCoolDown)
                {
                    isCoolDown = true;
                    timer_script.Reset();
                    timer_script.StartTimer();
                    stepCoolDown = (float)((temperature_end - water_temp) / s);
                    startTemperature = water_temp;
                }
                if (isCoolDown && s > timer_script.Seconds)
                {
                    water_temp = startTemperature + timer_script.Seconds * stepCoolDown;
                    SetCurrentTemperature(temp1, water_temp, 37f);
                    water_temperature.Value = water_temp.ToString("0.0");
                }
                if(isCoolDown && s <= timer_script.Seconds)
                {
                    timer_script.StopTimer();
                }
            }
            else if(isCoolDown)
            {
                timer_script.Reset();
                isCoolDown = false;
            }
        }

        private void SetupRadioButtonValue()
        {
            ui_script.CastIron_btn.Check += (o, e) =>
            {
                C_tt = 540;
                p = 7100;
                CalculateAtt_Stt();
                CalculateEndTemperature();
            };
            ui_script.Glass_btn.Check += (o, e) =>
            {
                C_tt = 670;
                p = 2500;
                CalculateAtt_Stt();
                CalculateEndTemperature();
            };
            ui_script.Graphite_btn.Check += (o, e) =>
            {
                C_tt = 750;
                p = 2260;
                CalculateAtt_Stt();
                CalculateEndTemperature();
            };
            ui_script.Brick_btn.Check += (o, e) =>
            {
                C_tt = 850;
                p = 1800;
                CalculateAtt_Stt();
                CalculateEndTemperature();
            };
            ui_script.Marble_btn.Check += (o, e) =>
            {
                C_tt = 920;
                p = 2700;
                CalculateAtt_Stt();
                CalculateEndTemperature();
            };
            ui_script.Tree_btn.Check += (o, e) =>
            {
                C_tt = 1300;
                p = 520;
                CalculateAtt_Stt();
                CalculateEndTemperature();
            };
            ui_script.Textolite_btn.Check += (o, e) =>
            {
                C_tt = 1470;
                p = 1470;
                CalculateAtt_Stt();
                CalculateEndTemperature();
            };
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
