using JUnity.UI;
using SharpDX;
using System.Collections.Generic;

namespace JUnity.Services
{
    internal class UIController
    {
        private readonly List<UIElement> _elements = new List<UIElement>();

        /// <summary>
        /// Handle mouse click
        /// </summary>
        /// <param name="clickPosition">Click position (relative to game window)</param>
        /// <returns>Is click absorbed</returns>
        public bool HandleClick(Vector2 clickPosition)
        {
            var elementsUnderCursor = new List<UIElement>();
            foreach (var element in _elements)
            {
                if (element.Position.X <= clickPosition.X && element.Position.Y <= clickPosition.Y &&
                    element.Position.X + element.Width >= clickPosition.X && element.Position.Y + element.Height >= clickPosition.Y)
                {
                    elementsUnderCursor.Add(element);
                }
            }

            if (elementsUnderCursor.Count > 0)
            {
                elementsUnderCursor.Sort((x, y) => x.ZOrder.CompareTo(y.ZOrder));
                for (int i = 0; i < elementsUnderCursor.Count; i++)
                {
                    if (elementsUnderCursor[i].HandleClick(clickPosition - elementsUnderCursor[i].Position))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void RegisterElement(UIElement element)
        {
            _elements.Add(element);
        }

        public void RemoveElement(UIElement element)
        {
            _elements.Remove(element);
        }
    }
}
