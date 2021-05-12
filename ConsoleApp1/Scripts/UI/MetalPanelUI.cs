using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Surfaces;
using SharpDX;

namespace Lab3.Scripts.UI
{
    public class MetalPanelUI : Script
    {
        TextBox MetalPanelLabel = new TextBox
        {
            Width = 0.2f,
            Height = 0.035f,
            Position = new Vector2(0.75f, 0.37f),
            Value = "Металл"
        };

        public RadioButton AluminumButton = new RadioButton("Materials")
        {
            Width = 0.015f,
            Height = 0.015f,
            Position = new Vector2(0.76f, 0.4225f),
            Checked = true,
        };

        TextBox aluminumLabel = new TextBox
        {
            Width = 0.08f,
            Height = 0.035f,
            Position = new Vector2(0.78f, 0.41f),
            Value = "Алюминий"
        };

        public RadioButton BrassButton = new RadioButton("Materials")
        {
            Width = 0.015f,
            Height = 0.015f,
            Position = new Vector2(0.76f, 0.4625f),
        };

        TextBox brassLabel = new TextBox
        {
            Width = 0.08f,
            Height = 0.035f,
            Position = new Vector2(0.78f, 0.45f),
            Value = "Латунь"
        };

        public RadioButton SteelButton = new RadioButton("Materials")
        {
            Width = 0.015f,
            Height = 0.015f,
            Position = new Vector2(0.76f, 0.5025f),
        };

        TextBox steelLabel = new TextBox
        {
            Width = 0.08f,
            Height = 0.035f,
            Position = new Vector2(0.78f, 0.49f),
            Value = "Сталь"
        };

        public RadioButton CastIronButton = new RadioButton("Materials")
        {
            Width = 0.015f,
            Height = 0.015f,
            Position = new Vector2(0.76f, 0.5425f),
        };

        TextBox castIronLabel = new TextBox
        {
            Width = 0.08f,
            Height = 0.035f,
            Position = new Vector2(0.78f, 0.53f),
            Value = "Чугун"
        };

        public TextBox CurrentWeight = new TextBox
        {
            Width = 0.11f,
            Height = 0.05f,
            Position = new Vector2(0.675f, 0.895f),
            Value = "0.942"
        };

        RectangleBackground panelBoxBorder = new RectangleBackground
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

        RectangleBackground panelBox = new RectangleBackground
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

        RectangleBackground weigherBoxBorder = new RectangleBackground
        {
            Width = 0.12f,
            Height = 0.07f,
            Position = new Vector2(0.67f, 0.885f),
            Background = new SolidColorRectangle
            {
                Color = new Color(100, 141, 151),
            },
            ZOrder = 500,
        };

        RectangleBackground weigherBox = new RectangleBackground
        {
            Width = 0.11f,
            Height = 0.05f,
            Position = new Vector2(0.675f, 0.895f),
            Background = new SolidColorRectangle
            {
                Color = new Color(26, 29, 28),
            },
            ZOrder = 400,
        };

        public override void Start()
        {
            Canvas.RegisterElement(MetalPanelLabel);
            MetalPanelLabel.CreateStyleTitle();

            Canvas.RegisterElement(AluminumButton);
            Canvas.RegisterElement(aluminumLabel);
            aluminumLabel.CreateStyleTextBox(24f);
            aluminumLabel.ChangeTextAlignmentTextBox(SharpDX.DirectWrite.TextAlignment.Leading);
            Canvas.RegisterElement(BrassButton);
            Canvas.RegisterElement(brassLabel);
            brassLabel.CreateStyleTextBox(24f);
            brassLabel.ChangeTextAlignmentTextBox(SharpDX.DirectWrite.TextAlignment.Leading);
            Canvas.RegisterElement(SteelButton);
            Canvas.RegisterElement(steelLabel);
            steelLabel.CreateStyleTextBox(24f);
            steelLabel.ChangeTextAlignmentTextBox(SharpDX.DirectWrite.TextAlignment.Leading);
            Canvas.RegisterElement(CastIronButton);
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
        }
    }
}
