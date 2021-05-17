using JUnity;
using JUnity.Components;
using JUnity.Components.Audio;
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
        #region Timer
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

        public Button ResetButton => reset_btn;
        #endregion

        System.Diagnostics.Stopwatch timer;
        Scene scene = new Scene();
        private AudioPlayer fireMusic;

        public bool TimerOn { get; set; }
        public bool TimerReset { get; set; }
        public float Seconds => timer.ElapsedMilliseconds / 1000f;

        GameObject fire;
        public override void Start()
        {
            timer = new System.Diagnostics.Stopwatch();
            timer.Reset();
            fireMusic = Scene.Find("Burn").GetComponent<AudioPlayer>();
            fireMusic.Repeat = true;
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
            TimerReset = true;
            fire = Scene.Find("Fire");
            start_btn.Click += (o, e) =>
            {
                StartTimer();
                fireMusic.Play();
                fire.IsActive = true;
            };

            pause_btn.Click += (o, e) =>
            {
                StopTimer();
                fireMusic.Stop();
                fire.IsActive = false;
            };

            reset_btn.Click += (o, e) =>
            {
                Reset();
                fireMusic.Stop();
                fire.IsActive = false;
            };
            
        }

        public void Reset()
        {
            TimerReset = true;
            TimerOn = false;
            timer.Reset();
        }

        public void StartTimer()
        {
            timer.Start();
            TimerOn = true;
            TimerReset = false;
        }
        public void StopTimer()
        {
            TimerOn = false;
            timer.Stop();
        }

        public override void FixedUpdate(double deltaTime)
        {
            timer_box.Value = timer.ElapsedMilliseconds / 1000f;
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
    }
}
