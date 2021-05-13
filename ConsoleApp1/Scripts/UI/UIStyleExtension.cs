using JUnity.Components.UI;
using JUnity.Services.UI.Styling;
using JUnity.Services.UI.Surfaces;
using SharpDX;
using SharpDX.DirectWrite;

namespace Lab3.Scripts.UI
{
    public static class UIStyleExtension
    {
        public static void CreateStyleMainLabel(this TextBox textBox)
        {
            textBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.FocusedBorder = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.TextStyle = new DisablingTextStyle()
            {
                Color = new Color(200, 0, 80, 255),
                TextFormat = new JUnity.Services.UI.Styling.TextFormat
                {
                    FontFamily = "TimesNewRoman",
                    FontSize = 30f,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Normal,
                    FontWeight = FontWeight.Bold,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                }
            };
        }

        public static void CreateStyleTextBox(this TextBox textBox, float fontSize = 24f)
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
                    FontSize = fontSize,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Italic,
                    FontWeight = FontWeight.Normal,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = TextAlignment.Center,
                }
            };
        }

        public static void ChangeColorStyleTextBox(this TextBox textBox, Color color)
        {
            textBox.Style.TextStyle.Color = color;
        }

        public static void ChangeFontStyleTextBox(this TextBox textBox, FontStyle fontStyle)
        {
            textBox.Style.TextStyle.TextFormat.FontStyle = fontStyle;
        }

        public static void ChangeTextAlignmentTextBox(this TextBox textBox, TextAlignment alignment)
        {
            textBox.Style.TextStyle.TextFormat.TextAlignment = alignment;
        }

        public static void CreateStyleTitle(this TextBox textBox, float fontSize = 30f)
        {
            textBox.Style.ActiveBackground = new SolidColorRectangle() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.Border = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.FocusedBorder = new Border() { Color = new Color(0, 0, 0, 0) };
            textBox.Style.TextStyle = new DisablingTextStyle()
            {
                Color = new Color(210, 0, 0, 255),
                TextFormat = new JUnity.Services.UI.Styling.TextFormat
                {
                    FontFamily = "TimesNewRoman",
                    FontSize = fontSize,
                    FontStretch = FontStretch.Normal,
                    FontStyle = FontStyle.Normal,
                    FontWeight = FontWeight.Normal,
                    ParagraphAlignment = ParagraphAlignment.Center,
                    TextAlignment = SharpDX.DirectWrite.TextAlignment.Center,
                }
            };
        }
    }
}
