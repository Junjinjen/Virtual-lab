using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Surfaces;
using Lab3.Scripts.Interactions;
using SharpDX;
using System.Collections.Generic;

namespace Lab3.Scripts.UI
{
    public class MetalPanelUI : Script
    {
        private List<MetalParameters> metalParameters = new List<MetalParameters>()
        {
            new MetalParameters(2712f, 0.00025f, 920f),
            new MetalParameters(8921f, 0.00025f, 380f),
            new MetalParameters(7800f, 0.00025f, 500f),
            new MetalParameters(7850f, 0.00025f, 540f),
        };

        private int _currentMetalIndex = 0;

        public MetalParameters MetalParameters { get => metalParameters[_currentMetalIndex]; }

        private TextBox metalPanelLabel = new TextBox
        {
            Width = 0.2f,
            Height = 0.035f,
            Position = new Vector2(0.75f, 0.37f),
            Value = "Металл"
        };

        private RadioButton aluminumButton = new RadioButton("Materials")
        {
            Width = 0.015f,
            Height = 0.015f,
            Position = new Vector2(0.76f, 0.4225f),
            Checked = true,
        };

        private TextBox aluminumLabel = new TextBox
        {
            Width = 0.08f,
            Height = 0.035f,
            Position = new Vector2(0.78f, 0.41f),
            Value = "Алюминий"
        };

        private RadioButton brassButton = new RadioButton("Materials")
        {
            Width = 0.015f,
            Height = 0.015f,
            Position = new Vector2(0.76f, 0.4625f),
        };

        private TextBox brassLabel = new TextBox
        {
            Width = 0.08f,
            Height = 0.035f,
            Position = new Vector2(0.78f, 0.45f),
            Value = "Латунь"
        };

        private RadioButton steelButton = new RadioButton("Materials")
        {
            Width = 0.015f,
            Height = 0.015f,
            Position = new Vector2(0.76f, 0.5025f),
        };

        private TextBox steelLabel = new TextBox
        {
            Width = 0.08f,
            Height = 0.035f,
            Position = new Vector2(0.78f, 0.49f),
            Value = "Сталь"
        };

        private RadioButton castIronButton = new RadioButton("Materials")
        {
            Width = 0.015f,
            Height = 0.015f,
            Position = new Vector2(0.76f, 0.5425f),
        };

        private TextBox castIronLabel = new TextBox
        {
            Width = 0.08f,
            Height = 0.035f,
            Position = new Vector2(0.78f, 0.53f),
            Value = "Чугун"
        };

        public TextBox CurrentWeight = new TextBox
        {
            Width = 0.106f,
            Height = 0.05f,
            Position = new Vector2(0.677f, 0.894f),
            Value = "0,000"
        };

        private RectangleBackground panelBoxBorder = new RectangleBackground
        {
            Width = 0.22f,
            Height = 0.26f,
            Position = new Vector2(0.74f, 0.35f),
            Background = new SolidColorRectangle
            {
                Color = new Color(141, 161, 152),
            },
            ZOrder = 500,
        };

        private RectangleBackground panelBox = new RectangleBackground
        {
            Width = 0.21f,
            Height = 0.24f,
            Position = new Vector2(0.745f, 0.36f),
            Background = new SolidColorRectangle
            {
                Color = new Color(223, 233, 230),
            },
            ZOrder = 400,
        };

        private RectangleBackground weigherBoxBorder = new RectangleBackground
        {
            Width = 0.11f,
            Height = 0.058f,
            Position = new Vector2(0.675f, 0.89f),
            Background = new SolidColorRectangle
            {
                Color = new Color(100, 141, 151),
            },
            ZOrder = 500,
        };

        private RectangleBackground weigherBox = new RectangleBackground
        {
            Width = 0.106f,
            Height = 0.05f,
            Position = new Vector2(0.677f, 0.894f),
            Background = new SolidColorRectangle
            {
                Color = new Color(26, 29, 28),
            },
            ZOrder = 400,
        };

        public override void Start()
        {
            Canvas.RegisterElement(metalPanelLabel);
            metalPanelLabel.CreateStyleTitle();

            Canvas.RegisterElement(aluminumButton);
            Canvas.RegisterElement(aluminumLabel);
            aluminumLabel.CreateStyleTextBox(24f);
            aluminumLabel.ChangeTextAlignmentTextBox(SharpDX.DirectWrite.TextAlignment.Leading);
            Canvas.RegisterElement(brassButton);
            Canvas.RegisterElement(brassLabel);
            brassLabel.CreateStyleTextBox(24f);
            brassLabel.ChangeTextAlignmentTextBox(SharpDX.DirectWrite.TextAlignment.Leading);
            Canvas.RegisterElement(steelButton);
            Canvas.RegisterElement(steelLabel);
            steelLabel.CreateStyleTextBox(24f);
            steelLabel.ChangeTextAlignmentTextBox(SharpDX.DirectWrite.TextAlignment.Leading);
            Canvas.RegisterElement(castIronButton);
            Canvas.RegisterElement(castIronLabel);
            castIronLabel.CreateStyleTextBox(24f);
            castIronLabel.ChangeTextAlignmentTextBox(SharpDX.DirectWrite.TextAlignment.Leading);

            Canvas.RegisterElement(CurrentWeight);
            CurrentWeight.CreateStyleTextBox(28f);
            CurrentWeight.ChangeColorStyleTextBox(new Color(46, 156, 182));
            CurrentWeight.ChangeFontStyleTextBox(SharpDX.DirectWrite.FontStyle.Normal);

            Canvas.RegisterElement(panelBox);
            Canvas.RegisterElement(panelBoxBorder);

            Canvas.RegisterElement(weigherBox);
            Canvas.RegisterElement(weigherBoxBorder);

            var timer_script = (TimerScript)Scene.Find("Timer").Script;
            timer_script.OnTimerStarted += (o, e) =>
            {
                aluminumButton.Active = false;
                brassButton.Active = false;
                castIronButton.Active = false;
                steelButton.Active = false;
            };
            timer_script.OnTimerReseted += (o, e) =>
            {
                aluminumButton.Active = true;
                brassButton.Active = true;
                castIronButton.Active = true;
                steelButton.Active = true;
                CurrentWeight.Value = "0,000";
            };
        }

        public override void Update(double deltaTime)
        {
            if(aluminumButton.Checked)
            {
                _currentMetalIndex = 0;
            }

            if(brassButton.Checked)
            {
                _currentMetalIndex = 1;
            }

            if (castIronButton.Checked)
            {
                _currentMetalIndex = 2;
            }

            if (steelButton.Checked)
            {
                _currentMetalIndex = 3;
            }
        }
    }
}
