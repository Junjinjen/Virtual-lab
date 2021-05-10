using JUnity;
using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Surfaces;
using SharpDX;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using static JUnity.Components.UI.Button;

namespace App.Scripts
{
    public class UI : Script
    {
        private const int BackgroundBorderZOrder = 102;
        private const int BackgroundZOrder = 101;

        #region FirstTable

        private TextBox tb_tb1Header = new TextBox
        {
            Value = "Параметры кольца и пружины",
            Width = 0.4f,
            Height = 0.07f,
            Position = new Vector2(0.57f, 0.2f),
        };

        private FloatTextBox fv_d1 = new FloatTextBox
        {
            Value = 48,
            MaxValue = 50,
            MinValue = 20,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.87f, 0.28f),
        };

        private TextBox tb_p_d1 = new TextBox
        {
            Value = "Внешний диаметр кольца",
            Width = 0.22f,
            Height = 0.04f,
            Position = new Vector2(0.63f, 0.28f),
        };

        private TextBox tb_s_d1 = new TextBox
        {
            Value = "мм",
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.94f, 0.28f),
        };


        private FloatTextBox fv_d2 = new FloatTextBox
        {
            Value = 46,
            MaxValue = 50,
            MinValue = 20,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.87f, 0.35f),
        };

        private TextBox tb_p_d2 = new TextBox
        {
            Value = "Внутренний диаметр кольца",
            Width = 0.22f,
            Height = 0.04f,
            Position = new Vector2(0.63f, 0.35f),
        };

        private TextBox tb_s_d2 = new TextBox
        {
            Value = "мм",
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.94f, 0.35f),
        };


        private FloatTextBox fv_dpr = new FloatTextBox
        {
            Value = 2f,
            MaxValue = 3f,
            MinValue = 1f,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.87f, 0.42f),
        };

        private TextBox tb_p_dpr = new TextBox
        {
            Value = "Диаметр проволки",
            Width = 0.22f,
            Height = 0.04f,
            Position = new Vector2(0.63f, 0.42f),
        };

        private TextBox tb_s_dpr = new TextBox
        {
            Value = "мм",
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.94f, 0.42f),
        };


        private FloatTextBox fv_dvit = new FloatTextBox
        {
            Value = 50,
            MaxValue = 55,
            MinValue = 45,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.87f, 0.49f),
        };

        private TextBox tb_p_dvit = new TextBox
        {
            Value = "Диаметр витка пружины",
            Width = 0.22f,
            Height = 0.04f,
            Position = new Vector2(0.63f, 0.49f),
        };

        private TextBox tb_s_dvit = new TextBox
        {
            Value = "мм",
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.94f, 0.49f),
        };


        private FloatTextBox fv_dx = new FloatTextBox
        {
            Value = 0,
            MaxValue = 100,
            MinValue = 0,
            Width = 0.05f,
            Height = 0.04f,
            Position = new Vector2(0.87f, 0.56f),
        };

        private TextBox tb_p_dx = new TextBox
        {
            Value = "Удлинение пружины",
            Width = 0.22f,
            Height = 0.04f,
            Position = new Vector2(0.63f, 0.56f),
        };

        private TextBox tb_s_dx = new TextBox
        {
            Value = "мм",
            Width = 0.04f,
            Height = 0.04f,
            Position = new Vector2(0.94f, 0.56f),
        };


        private Button resetB = new Button
        {
            Text = "Сброс",
            Width = 0.15f,
            Height = 0.05f,
            Position = new Vector2(0.725f, 0.62f),
        };
        private Button xUpB = new Button
        {
            Text = "▲",
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.92f, 0.56f),
        };
        private Button xDownB = new Button
        {
            Text = "▼",
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.92f, 0.58f),
        };


        private RectangleBackground b1b = new RectangleBackground
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

        private RectangleBackground b1 = new RectangleBackground
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

        RadioButton r1 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.57f, 0.75f)
        };
        RadioButton r2 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.57f, 0.81f)
        };
        RadioButton r3 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.57f, 0.87f)
        };
        RadioButton r4 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.72f, 0.75f)
        };
        RadioButton r5 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.72f, 0.81f)
        };
        RadioButton r6 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.72f, 0.87f)
        };
        RadioButton r7 = new RadioButton("Liquid")
        {
            Width = 0.02f,
            Height = 0.02f,
            Position = new Vector2(0.87f, 0.87f)
        };

        private TextBox tb_tb2Header = new TextBox
        {
            Value = "Жидкость",
            Width = 0.1f,
            Height = 0.07f,
            Position = new Vector2(0.87f, 0.72f),
        };

        private TextBox tb_2_value = new TextBox
        {
            Value = "",
            Width = 0.1f,
            Height = 0.07f,
            Position = new Vector2(0.87f, 0.79f),
        };


        private TextBox tb_r1 = new TextBox
        {
            Value = "Глицерин",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.59f, 0.73f),
        };

        private TextBox tb_r2 = new TextBox
        {
            Value = "Вода",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.59f, 0.79f),
        };

        private TextBox tb_r3 = new TextBox
        {
            Value = "Эфир",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.59f, 0.85f),
        };

        private TextBox tb_r4 = new TextBox
        {
            Value = "Бензол",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.74f, 0.73f),
        };

        private TextBox tb_r5 = new TextBox
        {
            Value = "Нефть",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.74f, 0.79f),
        };

        private TextBox tb_r6 = new TextBox
        {
            Value = "Масло",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.74f, 0.85f),
        };

        private TextBox tb_r7 = new TextBox
        {
            Value = "Спирт",
            Width = 0.10f,
            Height = 0.06f,
            Position = new Vector2(0.89f, 0.85f),
        };

        private RectangleBackground b2b = new RectangleBackground
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

        private RectangleBackground b2 = new RectangleBackground
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

        TextBox main = new TextBox()
        {
            Value = "Определение коэффициентов поверхностного натяжения жидкостей",
            Width = 0.4f,
            Height = 0.2f,
            Position = new Vector2(0.55f, 0.0f),
        };


        public override void Start()
        {
            #region FirstTable
            Canvas.RegisterElement(resetB);
            resetB.Click += (o, e) =>
            {
                fv_d1.Value = 48f;
                fv_d2.Value = 46f;
                fv_dpr.Value = 2f;
                fv_dvit.Value = 50f;
                fv_dx.Value = 0f;
            };
            Canvas.RegisterElement(xUpB);
            Canvas.RegisterElement(xDownB);

            Canvas.RegisterElement(tb_tb1Header);

            Canvas.RegisterElement(tb_p_d1);
            Canvas.RegisterElement(fv_d1);
            Canvas.RegisterElement(tb_s_d1);

            Canvas.RegisterElement(tb_p_d2);
            Canvas.RegisterElement(fv_d2);
            Canvas.RegisterElement(tb_s_d2);

            Canvas.RegisterElement(tb_p_dpr);
            Canvas.RegisterElement(fv_dpr);
            Canvas.RegisterElement(tb_s_dpr);

            Canvas.RegisterElement(tb_p_dvit);
            Canvas.RegisterElement(fv_dvit);
            Canvas.RegisterElement(tb_s_dvit);

            Canvas.RegisterElement(tb_p_dx);
            Canvas.RegisterElement(fv_dx);
            fv_dx.ReadOnly = true;
            Canvas.RegisterElement(tb_s_dx);

            Canvas.RegisterElement(b1b);
            Canvas.RegisterElement(b1);
            #endregion

            #region SecondTable

            EventHandler handler = (o, e) =>
            {
                tb_2_value.Value = GetLiquidValue().ToString("0.0000") + " Н/м";
            };

            Canvas.RegisterElement(r1);
            Canvas.RegisterElement(r2);
            Canvas.RegisterElement(r3);
            Canvas.RegisterElement(r4);
            Canvas.RegisterElement(r5);
            Canvas.RegisterElement(r6);
            Canvas.RegisterElement(r7);
            r1.Check += handler;
            r2.Check += handler;
            r3.Check += handler;
            r4.Check += handler;
            r5.Check += handler;
            r6.Check += handler;
            r7.Check += handler;

            r1.Checked = true;

            Canvas.RegisterElement(tb_r1);
            Canvas.RegisterElement(tb_r2);
            Canvas.RegisterElement(tb_r3);
            Canvas.RegisterElement(tb_r4);
            Canvas.RegisterElement(tb_r5);
            Canvas.RegisterElement(tb_r6);
            Canvas.RegisterElement(tb_r7);

            Canvas.RegisterElement(tb_tb2Header);
            Canvas.RegisterElement(tb_2_value);

            Canvas.RegisterElement(b2b);
            Canvas.RegisterElement(b2);
            #endregion

            Canvas.RegisterElement(main);

            #region SetFloatTextBoxStyle
            SetFloatTextBox(fv_d1.Style);
            SetFloatTextBox(fv_d2.Style);
            SetFloatTextBox(fv_dpr.Style);
            SetFloatTextBox(fv_dvit.Style);
            SetFloatTextBox(fv_dx.Style);
            #endregion

            #region SetTextBoxStyle
            SetTextBoxPref(tb_p_d1.Style);
            SetTextBoxSuff(tb_s_d1.Style);
            SetTextBoxPref(tb_p_d2.Style);
            SetTextBoxSuff(tb_s_d2.Style);
            SetTextBoxPref(tb_p_dpr.Style);
            SetTextBoxSuff(tb_s_dpr.Style);
            SetTextBoxPref(tb_p_dvit.Style);
            SetTextBoxSuff(tb_s_dvit.Style);
            SetTextBoxPref(tb_p_dx.Style);
            SetTextBoxSuff(tb_s_dx.Style);
            SetTextBoxSuff(tb_tb1Header.Style, 40f);
            SetTextBoxSuff(tb_tb2Header.Style, 40f);
            SetTextBoxSuff(tb_2_value.Style);
            SetTextBoxPref(tb_r1.Style, TextAlignment.Leading);
            SetTextBoxPref(tb_r2.Style, TextAlignment.Leading);
            SetTextBoxPref(tb_r3.Style, TextAlignment.Leading);
            SetTextBoxPref(tb_r4.Style, TextAlignment.Leading);
            SetTextBoxPref(tb_r5.Style, TextAlignment.Leading);
            SetTextBoxPref(tb_r6.Style, TextAlignment.Leading);
            SetTextBoxPref(tb_r7.Style, TextAlignment.Leading);

            SetTextBoxSuff(main.Style, 50f, FontWeight.ExtraBold);
            #endregion

            #region SetButtons
            SetButtonStyle(resetB.Style);
            SetButtonStyle(xUpB.Style);
            SetButtonStyle(xDownB.Style);
            #endregion
        }

        public override void FixedUpdate(double deltaTime)
        {
            if (xUpB.IsPressed && fv_dx.MaxValue > fv_dx.Value) fv_dx.Value += 0.1f;
            if (xDownB.IsPressed && fv_dx.MinValue < fv_dx.Value) fv_dx.Value -= 0.1f;
        }

        public float GetLiquidValue()
        {
            if (r1.Checked) return 0.0594f;
            if (r2.Checked) return 0.0728f;
            if (r3.Checked) return 0.0169f;
            if (r4.Checked) return 0.0439f;
            if (r5.Checked) return 0.026f;
            if (r6.Checked) return 0.0354f;
            if (r7.Checked) return 0.0226f;
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

        private void SetButtonStyle(ButtonStyle style)
        {
            style.TextStyle.Color = new Color(197, 112, 167);
            style.TextStyle.TextFormat.FontSize = 30f;
            style.TextStyle.TextFormat.FontWeight = FontWeight.ExtraBold;
            style.ActiveBackground = new SolidColorRectangle
            {
                Color = Color.Zero,
            };
            style.PressedBackground = new SolidColorRectangle
            {
                Color = Color.Zero,
            };
        }

        #endregion
    }
}