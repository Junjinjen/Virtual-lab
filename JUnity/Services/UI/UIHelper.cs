using JUnity.Components.UI;
using SharpDX;

namespace JUnity.Services.UI
{
    internal static class UIHelper
    {
        public static bool IsCursorOverElement(Vector2 mousePosition, UIElement element)
        {
            if (element.Position.X <= mousePosition.X && element.Position.Y <= mousePosition.Y &&
                element.Position.X + element.Width >= mousePosition.X && element.Position.Y + element.Height >= mousePosition.Y)
            {
                return true;
            }

            return false;
        }
    }
}
