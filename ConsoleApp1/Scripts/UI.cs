using JUnity;
using JUnity.Components;
using JUnity.Components.UI;
using JUnity.Services.UI.Surfaces;
using SharpDX;

namespace App.Scripts
{
    public class UI : Script
    {
        private const int BackgroundBorderZOrder = 102;
        private const int BackgroundZOrder = 101;

        private FloatTextBox fv_d1 = new FloatTextBox
        {
            Value = 48,
            MaxValue = 50,
            MinValue = 20,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.75f, 0.3f),
        };

        private TextBox tb_p_d1 = new TextBox
        {
            Value = "Внешний диаметр кольца",
            Width = 0.2f,
            Height = 0.04f,
            Position = new Vector2(0.55f, 0.3f),
            FontSize = 20f,
            TextColor = new Color(197, 112, 167),
        };

        private TextBox tb_s_d1 = new TextBox
        {
            Value = "мм",
            Width = 0.06f,
            Height = 0.04f,
            Position = new Vector2(0.8f, 0.3f),
            FontSize = 20f,
            TextColor = new Color(197, 112, 167),
        };

        private FloatTextBox fv_d2 = new FloatTextBox
        {
            Value = 46,
            MaxValue = 50,
            MinValue = 20,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.75f, 0.35f),
        };

        private FloatTextBox fv_dpr = new FloatTextBox
        {
            Value = 48,
            MaxValue = 50,
            MinValue = 20,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.75f, 0.4f),
        };

        private FloatTextBox fv_dvit = new FloatTextBox
        {
            Value = 46,
            MaxValue = 50,
            MinValue = 20,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.75f, 0.45f),
        };

        private FloatTextBox fv_dx = new FloatTextBox
        {
            Value = 46,
            MaxValue = 50,
            MinValue = 20,
            Width = 0.07f,
            Height = 0.04f,
            Position = new Vector2(0.75f, 0.5f),
        };

        private RectangleBackground b1b = new RectangleBackground
        {
            Height = 0.5f,
            Width = 0.6f,
            Position = new Vector2(0.55f, 0.2f),
            Background = new SolidColorRectangle{
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

        public override void Start()
        {
            Canvas.RegisterElement(tb_p_d1);
            Canvas.RegisterElement(fv_d1);
            Canvas.RegisterElement(tb_s_d1);

            Canvas.RegisterElement(fv_d2);
            Canvas.RegisterElement(fv_dpr);
            Canvas.RegisterElement(fv_dvit);
            Canvas.RegisterElement(fv_dx);
            Canvas.RegisterElement(b1b);
            Canvas.RegisterElement(b1);
        }
    }
}
