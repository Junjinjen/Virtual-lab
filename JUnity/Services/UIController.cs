using Engine.UI;
using SharpDX;
using System.Collections.Generic;

namespace Engine.Services
{
    internal class UIController
    {
        private readonly List<UIElement> _elements = new List<UIElement>();
        private readonly Dictionary<MouseKey, UIElement> _mouseUpListeners = new Dictionary<MouseKey, UIElement>
        {
            { MouseKey.Left, null },
            { MouseKey.Right, null },
            { MouseKey.Middle, null },
            { MouseKey.Mouse4, null },
            { MouseKey.Mouse5, null },
        };

        public bool HandleMouseDown(Vector2 mousePosition, MouseKey key, bool isJustPressed)
        {
            var elementsUnderCursor = new List<UIElement>();
            foreach (var element in _elements)
            {
                if (element.IsVisible && IsCursorOverElement(mousePosition, element))
                {
                    elementsUnderCursor.Add(element);
                }
            }

            if (elementsUnderCursor.Count > 0)
            {
                elementsUnderCursor.Sort((x, y) => x.ZOrder.CompareTo(y.ZOrder));
                for (int i = 0; i < elementsUnderCursor.Count; i++)
                {
                    if (elementsUnderCursor[i].HandleMouseDown(mousePosition - elementsUnderCursor[i].Position, key, isJustPressed))
                    {
                        _mouseUpListeners[key] = elementsUnderCursor[i];
                        return true;
                    }
                }
            }

            return false;
        }

        public void HandleMouseUp(Vector2 mousePosition, MouseKey key)
        {
            var element = _mouseUpListeners[key];
            if (element != null)
            {
                _mouseUpListeners[key] = null;
                if (IsCursorOverElement(mousePosition, element))
                {
                    element.HandleMouseUp(mousePosition - element.Position, key);
                }
                else
                {
                    element.HandleMouseUpOutOfElement(key);
                }
            }
        }

        public bool HandleMouseScroll(Vector2 mousePosition, int deltaScrollValue)
        {
            var elementsUnderCursor = new List<UIElement>();
            foreach (var element in _elements)
            {
                if (element.IsVisible && IsCursorOverElement(mousePosition, element))
                {
                    elementsUnderCursor.Add(element);
                }
            }

            if (elementsUnderCursor.Count > 0)
            {
                elementsUnderCursor.Sort((x, y) => x.ZOrder.CompareTo(y.ZOrder));
                for (int i = 0; i < elementsUnderCursor.Count; i++)
                {
                    if (elementsUnderCursor[i].HandleMouseScroll(mousePosition - elementsUnderCursor[i].Position, deltaScrollValue))
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
            if (_elements.Remove(element))
            {
                foreach (var pair in _mouseUpListeners)
                {
                    if (pair.Value.Equals(element))
                    {
                        _mouseUpListeners[pair.Key] = null;
                        return;
                    }
                }
            }
        }

        private bool IsCursorOverElement(Vector2 mousePosition, UIElement element)
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
