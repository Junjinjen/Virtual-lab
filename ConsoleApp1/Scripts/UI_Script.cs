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
        public FloatTextBox V_b = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.8f, 0.2f),
            Value = 1f,
        };

        TextBox V_b_label = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.73f, 0.2f),
            Value = "V   = "
        };

        TextBox V_b_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new SharpDX.Vector2(0.745f, 0.21f),
            Value = "в"
        };

        FloatTextBox t0 = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.8f, 0.24f),
            Value = 14.53f
        };

        TextBox t0_label = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.73f, 0.24f),
            Value = "t    = "
        };

        TextBox t0_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new SharpDX.Vector2(0.745f, 0.25f),
            Value = "в"
        };


        FloatTextBox c_b = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.8f, 0.28f),
            Value = 14.53f
        };
        TextBox c_b_label = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.73f, 0.28f),
            Value = "c    = "
        };

        TextBox c_b_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new SharpDX.Vector2(0.745f, 0.29f),
            Value = "в"
        };


        FloatTextBox m_k = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.8f, 0.32f),
            Value = 14.53f
        };
        TextBox m_k_label = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.73f, 0.32f),
            Value = "m   = "
        };

        TextBox m_k_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new SharpDX.Vector2(0.748f, 0.33f),
            Value = "к"
        };


        FloatTextBox c_k = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.8f, 0.36f),
            Value = 14.53f
        };
        TextBox c_k_label = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.73f, 0.36f),
            Value = "c    = "
        };

        TextBox c_k_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new SharpDX.Vector2(0.745f, 0.37f),
            Value = "к"
        };


        FloatTextBox t = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.04f,
            Position = new SharpDX.Vector2(0.8f, 0.4f),
            Value = 14.53f,
            ReadOnly = true
        };
        TextBox t_label = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.73f, 0.4f),
            Value = "t    = "
        };
        TextBox t_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new SharpDX.Vector2(0.745f, 0.41f),
            Value = "к"
        };

        //


        FloatTextBox m_t = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.04f,
            Position = new SharpDX.Vector2(0.7f, 0.55f),
            Value = 14.53f,
            ReadOnly = true
        };
        TextBox m_t_label = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.63f, 0.55f),
            Value = "m    = "
        };

        TextBox m_t_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new SharpDX.Vector2(0.647f, 0.56f),
            Value = "т"
        };


        FloatTextBox t_t = new FloatTextBox
        {
            Width = 0.07f,
            Height = 0.04f,
            Position = new SharpDX.Vector2(0.7f, 0.6f),
            Value = 14.53f,
            ReadOnly = true
        };
        TextBox t_t_label = new TextBox
        {
            Width = 0.07f,
            Height = 0.035f,
            Position = new SharpDX.Vector2(0.63f, 0.6f),
            Value = "t     = "
        };

        TextBox t_t_label_index = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new SharpDX.Vector2(0.645f, 0.61f),
            Value = "т"
        };


        TextBox main_label = new TextBox
        {
            Width = 0.03f,
            Height = 0.03f,
            Position = new SharpDX.Vector2(0.645f, 0.61f),
            Value = "т"
        };

        TextBox first_label = new TextBox
        {
            Width = 0.4f,
            Height = 0.05f,
            Position = new SharpDX.Vector2(0.6f, 0.13f),
            Value = "Параметры калориметра и воды"
        };

        TextBox second_label = new TextBox
        {
            Width = 0.4f,
            Height = 0.05f,
            Position = new SharpDX.Vector2(0.6f, 0.48f),
            Value = "Параметры тела"
        };

        public override void Start()
        {
            Canvas.RegisterElement(V_b);
            Canvas.RegisterElement(V_b_label);
            CreateStyleTextBox(V_b_label, 13f);
            Canvas.RegisterElement(V_b_label_index);
            CreateStyleTextBox(V_b_label_index, 10f);

            Canvas.RegisterElement(t0);
            Canvas.RegisterElement(t0_label);
            CreateStyleTextBox(t0_label, 13f);
            Canvas.RegisterElement(t0_label_index);
            CreateStyleTextBox(t0_label_index, 10f);

            Canvas.RegisterElement(c_b);
            Canvas.RegisterElement(c_b_label);
            CreateStyleTextBox(c_b_label, 13f);
            Canvas.RegisterElement(c_b_label_index);
            CreateStyleTextBox(c_b_label_index, 10f);

            Canvas.RegisterElement(m_k);
            Canvas.RegisterElement(m_k_label);
            CreateStyleTextBox(m_k_label, 13f);
            Canvas.RegisterElement(m_k_label_index);
            CreateStyleTextBox(m_k_label_index, 10f);

            Canvas.RegisterElement(c_k);
            Canvas.RegisterElement(c_k_label);
            CreateStyleTextBox(c_k_label, 13f);
            Canvas.RegisterElement(c_k_label_index);
            CreateStyleTextBox(c_k_label_index, 10f);

            Canvas.RegisterElement(t);
            Canvas.RegisterElement(t_label);
            CreateStyleTextBox(t_label, 13f);
            Canvas.RegisterElement(t_label_index);
            CreateStyleTextBox(t_label_index, 10f);
            //


            Canvas.RegisterElement(m_t);
            Canvas.RegisterElement(m_t_label);
            CreateStyleTextBox(m_t_label, 13f);
            Canvas.RegisterElement(m_t_label_index);
            CreateStyleTextBox(m_t_label_index, 10f);

            Canvas.RegisterElement(t_t);
            CreateStyleReadonlyBox(t_t);
            Canvas.RegisterElement(t_t_label);
            CreateStyleTextBox(t_t_label, 13f);
            Canvas.RegisterElement(t_t_label_index);
            CreateStyleTextBox(t_t_label_index, 10f);

            //labels
            Canvas.RegisterElement(first_label);
            CreateStyleTextBoxLabel(first_label);

            Canvas.RegisterElement(second_label);
            CreateStyleTextBoxLabel(second_label);

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

        public override void Update(double deltaTime)
        {
            System.Console.WriteLine(V_b.Value);
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

        private void CreateStyleReadonlyBox(FloatTextBox textBox)
        {
            textBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.TextStyle = new DisablingTextStyle()
            {
                Color = new Color(255, 0, 0, 255),
                TextFormat = new JUnity.Services.UI.Styling.TextFormat
                {
                    FontFamily = "TimesNewRoman",
                    FontSize = 13.0f,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Normal,
                    FontWeight = FontWeight.Bold,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                }
            };
        }
    }
}
