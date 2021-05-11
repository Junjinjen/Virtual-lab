using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Styling;
using JUnity.Services.UI.Surfaces;
using SharpDX;
using SharpDX.DirectWrite;
using System;

namespace Lab2.Scripts
{
    public class Timer_Script : Script
    {
        public FloatTextBox timer_box = new FloatTextBox
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

        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        Scene scene = new Scene();
        Main_Script main;
        public override void Start()
        {
            main = (Main_Script)Scene.Find("Main").Script;
            Canvas.RegisterElement(timer_box);
            CreateStyleTimer(timer_box);
            Canvas.RegisterElement(start);
            CreateStyleButtonLabel(start);
            Canvas.RegisterElement(pause);
            CreateStyleButtonLabel(pause);
            Canvas.RegisterElement(reset);
            CreateStyleButtonLabel(reset);
            Canvas.RegisterElement(start_btn);
            Canvas.RegisterElement(pause_btn);
            Canvas.RegisterElement(reset_btn);

            start_btn.Click += (o, e) =>
            {
                timer.Start();
                main.TimerOn = true;
                Scene.Find("Fire").IsActive = true;
            };

            pause_btn.Click += (o, e) =>
            {
                main.TimerOn = false;
                timer.Stop();
            };

            reset_btn.Click += (o, e) =>
            {
                main.TimerOn = false;
                timer.Reset();
                Scene.Find("Fire").IsActive = false;
            };
            
        }

        private void CreateStyleButtonLabel(TextBox textBox)
        {
            textBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.FocusedBorder = new Border() { Color = new Color(0, 0, 0, 0) };
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
                    TextAlignment = TextAlignment.Center,
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
                    TextAlignment = TextAlignment.Center,
                }
            };
        }

        public override void Update(double deltaTime)
        {
            timer_box.Value = timer.ElapsedMilliseconds / 1000f;
        }
    }
}
