using System.Globalization;

namespace JUnity.Services.UI.Formatters
{
    public class FloatFormatter
    {
        public bool IsValidFormat(string text)
        {
            text = text.Replace(',', '.');
            return float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var _);
        }
    }
}
