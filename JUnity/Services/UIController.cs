using Engine.UI;
using SharpDX;
using System.Collections.Generic;

namespace Engine.Services
{
    internal class UIController
    {
        private readonly List<UIElement> _elements = new List<UIElement>();

        /// <summary>
        /// Handle mouse down event.
        /// </summary>
        /// <param name="mousePosition">Mouse position (relative to the game window)</param>
        /// <param name="key">Pressed key</param>
        /// <param name="isJustPressed">True if the key is became pressed on this frame</param>
        /// <returns>Is click absorbed</returns>
        public bool HandleMouseDown(Vector2 mousePosition, MouseKey key, bool isJustPressed)
        {
            throw new System.NotSupportedException();
        }

        /// <summary>
        /// Handle mouse up event. Called only if the key was suppressed by UI.
        /// </summary>
        /// <param name="mousePosition">Mouse position (relative to the game window)</param>
        /// <param name="key">Pressed key</param>
        public void HandleMouseUp(Vector2 mousePosition, MouseKey key)
        {
            throw new System.NotSupportedException();
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

    /*var elementsUnderCursor = new List<UIElement>();
            foreach (var element in _elements)
            {
                if (element.IsVisible &&
                    element.Position.X <= clickPosition.X && element.Position.Y <= clickPosition.Y &&
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

            return false;*/
}
