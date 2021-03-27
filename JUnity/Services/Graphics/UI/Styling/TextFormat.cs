using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;

namespace JUnity.Services.Graphics.UI.Styling
{
    public sealed class TextFormat : IEquatable<TextFormat>
    {
        public string FontFamily { get; set; }

        public float FontSize { get; set; }

        public FontWeight FontWeight { get; set; }

        public FontStyle FontStyle { get; set; }

        public FontStretch FontStretch { get; set; }

        public TextAlignment TextAlignment { get; set; }

        public ParagraphAlignment ParagraphAlignment { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as TextFormat);
        }

        public bool Equals(TextFormat other)
        {
            return other != null &&
                   FontFamily == other.FontFamily &&
                   FontSize == other.FontSize &&
                   FontWeight == other.FontWeight &&
                   FontStyle == other.FontStyle &&
                   FontStretch == other.FontStretch &&
                   TextAlignment == other.TextAlignment &&
                   ParagraphAlignment == other.ParagraphAlignment;
        }

        public override int GetHashCode()
        {
            int hashCode = 983364192;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FontFamily);
            hashCode = hashCode * -1521134295 + FontSize.GetHashCode();
            hashCode = hashCode * -1521134295 + FontWeight.GetHashCode();
            hashCode = hashCode * -1521134295 + FontStyle.GetHashCode();
            hashCode = hashCode * -1521134295 + FontStretch.GetHashCode();
            hashCode = hashCode * -1521134295 + TextAlignment.GetHashCode();
            hashCode = hashCode * -1521134295 + ParagraphAlignment.GetHashCode();
            return hashCode;
        }
    }
}
