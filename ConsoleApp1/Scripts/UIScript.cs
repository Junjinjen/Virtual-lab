using JUnity;
using JUnity.Components;
using JUnity.Components.Audio;
using JUnity.Components.Rendering;
using JUnity.Components.UI;
using JUnity.Services.Graphics.Meshing;
using JUnity.Services.UI.Surfaces;
using SharpDX;
using SharpDX.DirectWrite;
using System;
using static JUnity.Components.UI.Button;

namespace App.Scripts
{
    public class UIScript : Script
    {
        private const int BackgroundBorderZOrder = 102;
        private const int BackgroundZOrder = 101;

        #region FirstTable

        private TextBox table1Header = new TextBox
        {
            Value = "Параметры кольца и пружины",
            Width = 0.4f,
            Height = 0.07f,
            Position = new Vector2(0.57f, 0.2f),
        };

        private FloatTextBox floatTextBoxD1 = new FloatTextBox
        {
            Value = 48,
            MaxValue = 50,
            MinValue = 20,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.87f, 0.28f),
        };

        private TextBox tablePrefD1 = new TextBox
        {
            Value = "Внешний диаметр кольца",
            Width = 0.22f,
            Height = 0.04f,
            Position = new Vector2(0.63f, 0.28f),
        };

        private TextBox tableSufD1 = new TextBox
        {
            Value = "мм",
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.94f, 0.28f),
        };
        
        public FloatTextBox D1 => floatTextBoxD1;

       
        private FloatTextBox floatTextBoxD2 = new FloatTextBox
        {
            Value = 46,
            MaxValue = 50,
            MinValue = 20,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.87f, 0.35f),
        };

        private TextBox tablePrefD2 = new TextBox
        {
            Value = "Внутренний диаметр кольца",
            Width = 0.22f,
            Height = 0.04f,
            Position = new Vector2(0.63f, 0.35f),
        };

        private TextBox tableSufD2 = new TextBox
        {
            Value = "мм",
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.94f, 0.35f),
        };

        public FloatTextBox D2 => floatTextBoxD2;


        private FloatTextBox floatTextBoxDpr = new FloatTextBox
        {
            Value = 2f,
            MaxValue = 3f,
            MinValue = 1f,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.87f, 0.42f),
        };

        private TextBox tablePrefDpr = new TextBox
        {
            Value = "Диаметр проволки",
            Width = 0.22f,
            Height = 0.04f,
            Position = new Vector2(0.63f, 0.42f),
        };

        private TextBox tableSufDpr = new TextBox
        {
            Value = "мм",
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.94f, 0.42f),
        };

        public FloatTextBox Dpr => floatTextBoxDpr;


        private FloatTextBox flaotTextBoxDvit = new FloatTextBox
        {
            Value = 50,
            MaxValue = 55,
            MinValue = 45,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.87f, 0.49f),
        };

        private TextBox tablePrefDvit = new TextBox
        {
            Value = "Диаметр витка пружины",
            Width = 0.22f,
            Height = 0.04f,
            Position = new Vector2(0.63f, 0.49f),
        };

        private TextBox tableSufDvit = new TextBox
        {
            Value = "мм",
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.94f, 0.49f),
        };

        public FloatTextBox Dvit => flaotTextBoxDvit;


        private FloatTextBox floatTextBoxDx = new FloatTextBox
        {
            Value = 0,
            MaxValue = 54,
            MinValue = 0,
            Width = 0.05f,
            Height = 0.04f,
            Position = new Vector2(0.87f, 0.56f),
        };

        private TextBox tablePrefDx = new TextBox
        {
            Value = "Удлинение пружины",
            Width = 0.22f,
            Height = 0.04f,
            Position = new Vector2(0.63f, 0.56f),
        };

        private TextBox tableSufDx = new TextBox
        {
            Value = "мм",
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.94f, 0.56f),
        };

        public FloatTextBox Dx => floatTextBoxDx;


        private Button resetButton = new Button
        {
            Text = "Сброс",
            Width = 0.15f,
            Height = 0.05f,
            Position = new Vector2(0.725f, 0.62f),
        };

        public Button ResetButton => resetButton;

        private Button xUpButton = new Button
        {
            Text = "▲",
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.92f, 0.56f),
        };
        private Button xDownButton = new Button
        {
            Text = "▼",
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.92f, 0.58f),
        };


        private RectangleBackground backgroundBorder1 = new RectangleBackground
        {
            Height = 0.5f,
            Width = 0.6f,
            Position = new Vector2(0.55f, 0.2f),
            Background = new SolidColorRectangle
            {
                Color = new Color(132, 155, 147),
            },
            ZOrder = BackgroundBorderZOrder,
        };

        private RectangleBackground background1 = new RectangleBackground
        {
            Height = 0.485f,
            Width = 0.595f,
            Position = new Vector2(0.555f, 0.208f),
            Background = new SolidColorRectangle
            {
                Color = new Color(239, 243, 244),
            },
            ZOrder = BackgroundZOrder,
        };

        #endregion

        #region SecondTable

        private RadioButton radioButton1 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.57f, 0.75f)
        };
        private RadioButton radioButton2 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.57f, 0.81f)
        };
        private RadioButton radioButton3 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.57f, 0.87f)
        };
        private RadioButton radioButton4 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.72f, 0.75f)
        };
        private RadioButton radioButton5 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.72f, 0.81f)
        };
        private RadioButton radioButton6 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.72f, 0.87f)
        };
        private RadioButton radioButton7 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.87f, 0.87f)
        };

        private TextBox table2Header = new TextBox
        {
            Value = "Жидкость",
            Width = 0.18f,
            Height = 0.07f,
            Position = new Vector2(0.82f, 0.72f),
        };

        private TextBox table2value = new TextBox
        {
            Value = "",
            Width = 0.1f,
            Height = 0.07f,
            Position = new Vector2(0.87f, 0.79f),
        };

        public event EventHandler<float> CoefChanged;

        private TextBox radioButtonText1 = new TextBox
        {
            Value = "Глицерин",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.59f, 0.73f),
        };

        private TextBox radioButtonText2 = new TextBox
        {
            Value = "Вода",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.59f, 0.79f),
        };

        private TextBox radioButtonText3 = new TextBox
        {
            Value = "Эфир",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.59f, 0.85f),
        };

        private TextBox radioButtonText4 = new TextBox
        {
            Value = "Бензол",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.74f, 0.73f),
        };

        private TextBox radioButtonText5 = new TextBox
        {
            Value = "Нефть",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.74f, 0.79f),
        };

        private TextBox radioButtonText6 = new TextBox
        {
            Value = "Масло",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.74f, 0.85f),
        };

        private TextBox radioButtonText7 = new TextBox
        {
            Value = "Спирт",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.89f, 0.85f),
        };

        private RectangleBackground backgroundBorder2 = new RectangleBackground
        {
            Height = 0.2f,
            Width = 0.6f,
            Position = new Vector2(0.55f, 0.72f),
            Background = new SolidColorRectangle
            {
                Color = new Color(132, 155, 147),
            },
            ZOrder = BackgroundBorderZOrder,
        };

        private RectangleBackground background2 = new RectangleBackground
        {
            Height = 0.185f,
            Width = 0.595f,
            Position = new Vector2(0.555f, 0.728f),
            Background = new SolidColorRectangle
            {
                Color = new Color(239, 243, 244),
            },
            ZOrder = BackgroundZOrder,
        };

        #endregion

        private TextBox main = new TextBox()
        {
            Value = "Определение коэффициентов поверхностного натяжения жидкостей",
            Width = 0.4f,
            Height = 0.2f,
            Position = new Vector2(0.55f, 0.0f),
        };

        private Button exitButton = new Button()
        {
            Text = "Выход",
            Width = 0.05f,
            Height = 0.04f,
            Position = new Vector2(0.95f, 0f),
        };

        private Material LiquidMaterial;

        private AudioPlayer _rotate;

        private TextBox _arroy = new TextBox()
        {
            Value = "⬆",
            Width = 0.0f,
            Height = 0.0f,
            Position = new Vector2(0.1f, 0.4f),
            Active = false,
        };

        public TextBox Arroy => _arroy;

        private Button changerLiquid = new Button()
        {
            Text = "Жидкий металл",
            Width = 0.43f,
            Height = 0.05f,
            Position = new Vector2(0.56f, 0.93f),
        };

        private bool LiquidType = false;

        private TimerScript _timerScript;

        public override void Start()
        {
            #region FirstTable
            Canvas.RegisterElement(resetButton);
            resetButton.Click += (o, e) =>
            {
                floatTextBoxD1.Value = 48f;
                floatTextBoxD2.Value = 46f;
                floatTextBoxDpr.Value = 2f;
                flaotTextBoxDvit.Value = 50f;
                floatTextBoxDx.Value = 0f;
            };
            Canvas.RegisterElement(xUpButton);
            Canvas.RegisterElement(xDownButton);

            Canvas.RegisterElement(table1Header);

            Canvas.RegisterElement(tablePrefD1);
            Canvas.RegisterElement(floatTextBoxD1);
            Canvas.RegisterElement(tableSufD1);

            Canvas.RegisterElement(tablePrefD2);
            Canvas.RegisterElement(floatTextBoxD2);
            Canvas.RegisterElement(tableSufD2);

            Canvas.RegisterElement(tablePrefDpr);
            Canvas.RegisterElement(floatTextBoxDpr);
            Canvas.RegisterElement(tableSufDpr);

            Canvas.RegisterElement(tablePrefDvit);
            Canvas.RegisterElement(flaotTextBoxDvit);
            Canvas.RegisterElement(tableSufDvit);

            Canvas.RegisterElement(tablePrefDx);
            Canvas.RegisterElement(floatTextBoxDx);
            floatTextBoxDx.ReadOnly = true;
            Canvas.RegisterElement(tableSufDx);

            Canvas.RegisterElement(backgroundBorder1);
            Canvas.RegisterElement(background1);
            #endregion

            #region SecondTable

            LiquidMaterial = Scene.Find("water").GetComponent<MeshRenderer>().Material;

            EventHandler handler = (o, e) =>
            {
                table2value.Value = GetLiquidValue(out var number).ToString("0.0000") + " Н/м";
                CoefChanged?.Invoke(this, GetLiquidValue(out number));
                switch (number)
                {
                    case 1:
                        LiquidMaterial.AmbientCoefficient = LiquidType ? new Color4(0.8f, 0.8f, 0.8f, 1f) : new Color4(0.8f, 0.8f, 0.8f, 0.5f);
                        LiquidMaterial.DiffusionCoefficient = LiquidType ? new Color4(0.8f, 0.8f, 0.8f, 1f) : new Color4(0.8f, 0.8f, 0.8f, 0.5f);
                        LiquidMaterial.SpecularCoefficient = LiquidType ? new Color4(1f, 1f, 1f, 1f) : new Color4(0.8f, 0.8f, 0.8f, 0.5f);
                        break;
                    case 2:
                        LiquidMaterial.AmbientCoefficient = LiquidType ? new Color4(0.67f, 0.43f, 0.29f, 1f) : new Color4(0.9f, 0.9f, 0.9f, 0.2f);
                        LiquidMaterial.DiffusionCoefficient = LiquidType ? new Color4(0.67f, 0.43f, 0.29f, 1f) : new Color4(0.9f, 0.9f, 0.9f, 0.2f);
                        LiquidMaterial.SpecularCoefficient = LiquidType ? new Color4(1f, 1f, 1f, 1f) : new Color4(0.9f, 0.9f, 0.9f, 0.2f);
                        break;
                    case 3:
                        LiquidMaterial.AmbientCoefficient = LiquidType ? new Color4(0.51f, 0.56f, 0.6f, 1f) : new Color4(0.9f, 0.9f, 0.9f, 0.6f);
                        LiquidMaterial.DiffusionCoefficient = LiquidType ? new Color4(0.51f, 0.56f, 0.6f, 1f) : new Color4(0.9f, 0.9f, 0.9f, 0.6f);
                        LiquidMaterial.SpecularCoefficient = LiquidType ? new Color4(1f, 1f, 1f, 1f) : new Color4(0.9f, 0.9f, 0.9f, 0.6f);
                        break;
                    case 4:
                        LiquidMaterial.AmbientCoefficient = LiquidType ? new Color4(0.27f, 0.23f, 0.19f, 1f) : new Color4(0.7f, 0.5f, 0, 0.4f);
                        LiquidMaterial.DiffusionCoefficient = LiquidType ? new Color4(0.27f, 0.23f, 0.19f, 1f) : new Color4(0.7f, 0.5f, 0, 0.4f);
                        LiquidMaterial.SpecularCoefficient = LiquidType ? new Color4(1f, 1f, 1f, 1f) : new Color4(0.7f, 0.5f, 0, 0.4f);
                        break;
                    case 5:
                        LiquidMaterial.AmbientCoefficient = LiquidType ? new Color4(0.76f, 0.73f, 0.65f, 1f) : new Color4(0, 0, 0, 0.8f);
                        LiquidMaterial.DiffusionCoefficient = LiquidType ? new Color4(0.76f, 0.73f, 0.65f, 1f) : new Color4(0, 0, 0, 0.8f);
                        LiquidMaterial.SpecularCoefficient = LiquidType ? new Color4(1f, 1f, 1f, 1f) : new Color4(0, 0, 0, 0.8f);
                        break;
                    case 6:
                        LiquidMaterial.AmbientCoefficient = LiquidType ? new Color4(0.5f, 0.6f, 0.75f, 1f) : new Color4(0.8f, 0.5f, 0, 0.6f);
                        LiquidMaterial.DiffusionCoefficient = LiquidType ? new Color4(0.5f, 0.6f, 0.75f, 1f) : new Color4(0.8f, 0.5f, 0, 0.6f);
                        LiquidMaterial.SpecularCoefficient = LiquidType ? new Color4(1f, 1f, 1f, 1f) : new Color4(0.8f, 0.5f, 0, 0.6f);
                        break;
                    case 7:
                        LiquidMaterial.AmbientCoefficient = LiquidType ? new Color4(0.33f, 0.38f, 0.32f, 1f) : new Color4(0.9f, 0.9f, 0.9f, 0.25f);
                        LiquidMaterial.DiffusionCoefficient = LiquidType ? new Color4(0.33f, 0.38f, 0.32f, 1f) : new Color4(0.9f, 0.9f, 0.9f, 0.25f);
                        LiquidMaterial.SpecularCoefficient = LiquidType ? new Color4(1f, 1f, 1f, 1f) : new Color4(0.9f, 0.9f, 0.9f, 0.25f);
                        break;
                }
            };

            Canvas.RegisterElement(radioButton1);
            Canvas.RegisterElement(radioButton2);
            Canvas.RegisterElement(radioButton3);
            Canvas.RegisterElement(radioButton4);
            Canvas.RegisterElement(radioButton5);
            Canvas.RegisterElement(radioButton6);
            Canvas.RegisterElement(radioButton7);
            radioButton1.Check += handler;
            radioButton2.Check += handler;
            radioButton3.Check += handler;
            radioButton4.Check += handler;
            radioButton5.Check += handler;
            radioButton6.Check += handler;
            radioButton7.Check += handler;

            radioButton1.Checked = true;

            Canvas.RegisterElement(radioButtonText1);
            Canvas.RegisterElement(radioButtonText2);
            Canvas.RegisterElement(radioButtonText3);
            Canvas.RegisterElement(radioButtonText4);
            Canvas.RegisterElement(radioButtonText5);
            Canvas.RegisterElement(radioButtonText6);
            Canvas.RegisterElement(radioButtonText7);

            Canvas.RegisterElement(table2Header);
            Canvas.RegisterElement(table2value);

            Canvas.RegisterElement(backgroundBorder2);
            Canvas.RegisterElement(background2);
            #endregion

            Canvas.RegisterElement(_arroy);

            _rotate = Scene.Find("rotate").GetComponent<AudioPlayer>();
            _rotate.Repeat = true;

            _timerScript = (TimerScript)Scene.Find("Timer").Script;

            Canvas.RegisterElement(main);
            Canvas.RegisterElement(exitButton);
            Canvas.RegisterElement(changerLiquid);
            exitButton.Click += (o, e) => Engine.Instance.Stop();
            changerLiquid.Click += (o, e) =>
            {
                LiquidType = !LiquidType;
                CoefChanged?.Invoke(this, GetLiquidValue(out var _));
                radioButton1.Checked = true;
                if (LiquidType)
                {
                    changerLiquid.Text = "Жидкость";
                    radioButtonText1.Value = "Алюминий";
                    radioButtonText2.Value = "Медь";
                    radioButtonText3.Value = "Цинк";
                    radioButtonText4.Value = "Галлий";
                    radioButtonText5.Value = "Висмут";
                    radioButtonText6.Value = "Свинец";
                    radioButtonText7.Value = "Олово";
                    table2Header.Value = "Жидкий металл";
                }
                else 
                {
                    changerLiquid.Text = "Жидкий металл";
                    radioButtonText1.Value = "Глицерин";
                    radioButtonText2.Value = "Вода";
                    radioButtonText3.Value = "Эфир";
                    radioButtonText4.Value = "Бензол";
                    radioButtonText5.Value = "Нефть";
                    radioButtonText6.Value = "Масло";
                    radioButtonText7.Value = "Спирт";
                    table2Header.Value = "Жидкость";
                }
            };

            #region SetFloatTextBoxStyle
            SetFloatTextBox(floatTextBoxD1.Style);
            SetFloatTextBox(floatTextBoxD2.Style);
            SetFloatTextBox(floatTextBoxDpr.Style);
            SetFloatTextBox(flaotTextBoxDvit.Style);
            SetFloatTextBox(floatTextBoxDx.Style);
            #endregion

            #region SetTextBoxStyle
            SetTextBoxPref(tablePrefD1.Style);
            SetTextBoxSuff(tableSufD1.Style);
            SetTextBoxPref(tablePrefD2.Style);
            SetTextBoxSuff(tableSufD2.Style);
            SetTextBoxPref(tablePrefDpr.Style);
            SetTextBoxSuff(tableSufDpr.Style);
            SetTextBoxPref(tablePrefDvit.Style);
            SetTextBoxSuff(tableSufDvit.Style);
            SetTextBoxPref(tablePrefDx.Style);
            SetTextBoxSuff(tableSufDx.Style);
            SetTextBoxSuff(table1Header.Style, 40f);
            SetTextBoxSuff(table2Header.Style, 40f);
            SetTextBoxSuff(table2value.Style);
            SetTextBoxPref(radioButtonText1.Style, TextAlignment.Leading);
            SetTextBoxPref(radioButtonText2.Style, TextAlignment.Leading);
            SetTextBoxPref(radioButtonText3.Style, TextAlignment.Leading);
            SetTextBoxPref(radioButtonText4.Style, TextAlignment.Leading);
            SetTextBoxPref(radioButtonText5.Style, TextAlignment.Leading);
            SetTextBoxPref(radioButtonText6.Style, TextAlignment.Leading);
            SetTextBoxPref(radioButtonText7.Style, TextAlignment.Leading);
            SetTextBoxSuff(_arroy.Style, 200f);

            _arroy.Style.TextStyle.Color = new Color(0, 0, 0, 255);
            _arroy.Style.TextStyle.DisabledColor = new Color(0, 0, 0, 0);

            SetTextBoxSuff(main.Style, 50f, FontWeight.ExtraBold);
            main.Style.TextStyle.Color = new Color(157, 72, 127);
            #endregion

            #region SetButtons
            SetButtonStyle(resetButton.Style);
            SetButtonStyle(xUpButton.Style);
            SetButtonStyle(xDownButton.Style);
            SetButtonStyle(exitButton.Style, false);
            SetButtonStyle(changerLiquid.Style);
            #endregion
        }

        public override void FixedUpdate(double deltaTime)
        {
            if (xUpButton.IsPressed && floatTextBoxDx.MaxValue > floatTextBoxDx.Value)
            {
                floatTextBoxDx.Value += 0.02f;
                _rotate.Play();
            }
            else if (xDownButton.IsPressed && floatTextBoxDx.MinValue < floatTextBoxDx.Value)
            {
                floatTextBoxDx.Value -= 0.02f;
                _rotate.Play();
                if (floatTextBoxDx.Value < 0) floatTextBoxDx.Value = 0;
            }
            else if(!_timerScript.TimerStarted)
            {
                _rotate.Stop();
            }
        }

        public float GetLiquidValue(out int number)
        {
            if (radioButton1.Checked)
            {
                number = 1;
                return LiquidType ? 0.914f : 0.0594f;
            }
            if (radioButton2.Checked)
            {
                number = 2;
                return LiquidType ? 1.351f : 0.0728f;
            }
            if (radioButton3.Checked)
            {
                number = 3;
                return LiquidType ? 0.811f : 0.0169f;
            }
            if (radioButton4.Checked)
            {
                number = 4;
                return LiquidType ? 0.735f : 0.0439f;
            }
            if (radioButton5.Checked)
            {
                number = 5;
                return LiquidType ? 0.39f : 0.026f;
            }
            if (radioButton6.Checked)
            {
                number = 6;
                return LiquidType ? 0.48f : 0.0354f;
            }
            if (radioButton7.Checked)
            {
                number = 7;
                return LiquidType ? 0.554f : 0.0226f;
            }

            number = -1;
            return 0;
        }

        #region Styling
        private void SetFloatTextBox(TextBoxBaseStyle style)
        {
            style.TextStyle.TextFormat.FontSize = 20f;
        }

        private void SetTextBoxPref(TextBoxBaseStyle style, TextAlignment textAlignment = TextAlignment.Trailing)
        {
            style.TextStyle.TextFormat.FontSize = 30f;
            style.TextStyle.Color = new Color(197, 112, 167);
            style.TextStyle.TextFormat.TextAlignment = textAlignment;
            style.Border.Color = Color.Zero;
            style.ActiveBackground = new SolidColorRectangle()
            {
                Color = Color.Zero,
            };
            style.DisabledBackground = new SolidColorRectangle()
            {
                Color = Color.Zero,
            };
            style.DisabledBorder.Color = Color.Zero;
            style.FocusedBorder.Color = Color.Zero;
        }
        
        private void SetTextBoxSuff(TextBoxBaseStyle style, float fontSize = 30f, FontWeight fontWeight = FontWeight.Medium)
        {
            style.TextStyle.TextFormat.FontSize = fontSize;
            style.TextStyle.TextFormat.FontWeight = fontWeight;
            style.TextStyle.Color = new Color(197, 112, 167);
            style.Border.Color = Color.Zero;
            style.ActiveBackground = new SolidColorRectangle()
            {
                Color = Color.Zero,
            };
            style.DisabledBackground = new SolidColorRectangle()
            {
                Color = Color.Zero,
            };
            style.DisabledBorder.Color = Color.Zero;
            style.FocusedBorder.Color = Color.Zero;
        }

        private void SetButtonStyle(ButtonStyle style, bool offBackground = true)
        {
            style.TextStyle.Color = new Color(197, 112, 167);
            style.TextStyle.TextFormat.FontSize = 30f;
            style.TextStyle.TextFormat.FontWeight = FontWeight.ExtraBold;
            if (offBackground) {
                style.ActiveBackground = new SolidColorRectangle
                {
                    Color = Color.Zero,
                };
                style.PressedBackground = new SolidColorRectangle
                {
                    Color = Color.Zero,
                };
            }
        }

        #endregion
    }
}