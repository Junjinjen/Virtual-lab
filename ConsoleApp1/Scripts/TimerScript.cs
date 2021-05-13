using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Surfaces;
using SharpDX;
using System.Diagnostics;
using JUnity.Services.UI.Styling;
using static JUnity.Components.UI.Button;

namespace App.Scripts
{
    public class TimerScript : Script
    {
        private const int BackgroundBorderZOrder = 102;
        private const int BackgroundZOrder = 101;

        public bool TimerStarted { get; private set; }

        private Stopwatch _timer;
        public float Seconds => _timer.ElapsedMilliseconds / 1000f;

        #region TimerUI

        private Button buttonStart = new Button()
        {
            Text = "▶",
            Width = 0.03f,
            Height = 0.052f,
            Position = new Vector2(0.151f, 0.052f),
        };

        public Button StartButton => buttonStart;

        private Button buttonStop = new Button()
        {
            Text = "❚❚",
            Width = 0.03f,
            Height = 0.052f,
            Position = new Vector2(0.191f, 0.052f),
        };

        public Button StopButton => buttonStop;

        private Button buttonReset = new Button()
        {
            Text = "■",
            Width = 0.03f,
            Height = 0.052f,
            Position = new Vector2(0.231f, 0.052f),
        };

        public Button ResetButton => buttonReset;

        private FloatTextBox display = new FloatTextBox()
        {
            Value = 0,
            MaxValue = 9999,
            MinValue = 0,
            Width = 0.09f,
            Height = 0.06f,
            ReadOnly = true,
            Position = new Vector2(0.051f, 0.0491f),
            Format = "0.000"
        };

        private RectangleBackground backgroundBorderLayer2 = new RectangleBackground()
        {
            Height = 0.14f,
            Width = 0.3f,
            Position = new Vector2(0.01f, 0.01f),
            Background = new SolidColorRectangle
            {
                Color = new Color(140, 155, 150),
            },
            ZOrder = BackgroundBorderZOrder + 1,
        };

        private RectangleBackground backgroundBorderLayer1 = new RectangleBackground()
        {
            Height = 0.13f,
            Width = 0.294f,
            Position = new Vector2(0.013f, 0.015f),
            Background = new SolidColorRectangle
            {
                Color = new Color(255, 255, 255),
            },
            ZOrder = BackgroundBorderZOrder,
        };

        private RectangleBackground background = new RectangleBackground
        {
            Height = 0.12f,
            Width = 0.286f,
            Position = new Vector2(0.017f, 0.02f),
            Background = new SolidColorRectangle
            {
                Color = new Color(95, 125, 155),
            },
            ZOrder = BackgroundZOrder,
        };

        #endregion

        public override void Start()
        {
            Canvas.RegisterElement(backgroundBorderLayer2);
            Canvas.RegisterElement(backgroundBorderLayer1);
            Canvas.RegisterElement(background);
            Canvas.RegisterElement(display);
            Canvas.RegisterElement(buttonStart);
            Canvas.RegisterElement(buttonStop);
            Canvas.RegisterElement(buttonReset);

            #region displayStyle
            display.Style.Border = new Border()
            {
                Color = new Color(165, 165, 170, 255),
                Width = 5f,
            };
            display.Style.ActiveBackground = new SolidColorRectangle()
            {
                Color = new Color(0, 0, 0, 255),
            };
            display.Style.TextStyle.Color = new Color(35, 115, 160, 255);
            display.Style.TextStyle.TextFormat.FontSize = 35f;
            #endregion

            ButtonStyle(buttonStart.Style);
            ButtonStyle(buttonStop.Style);
            ButtonStyle(buttonReset.Style);

            buttonStart.Click += (o, e) =>
            {
                StartTimer();
            };
            buttonStop.Click += (o, e) =>
            {
                StopTimer();
            };
            buttonReset.Click += (o, e) =>
            {
                ResetTimer();
            };

            _timer = new Stopwatch();
            _timer.Reset();
        }
        
        private void ButtonStyle(ButtonStyle style)
        {
            style.TextStyle.Color = new Color(100, 100, 100, 255);
            style.TextStyle.TextFormat.FontSize = 25f;
            style.ActiveBackground = new SolidColorRectangle()
            {
                Color = new Color(200, 200, 200),
            };
            style.PressedBackground = new SolidColorRectangle()
            {
                Color = new Color(210, 210, 210),
            };
        }

        public override void FixedUpdate(double deltaTime)
        {
            display.Value = Seconds;
        }

        public void StartTimer()
        {
            TimerStarted = true;
            _timer.Start();
        }

        public void StopTimer()
        {
            TimerStarted = false;
            _timer.Stop();
        }

        public void ResetTimer()
        {
            TimerStarted = false;
            _timer.Reset();
        }
    }
}
