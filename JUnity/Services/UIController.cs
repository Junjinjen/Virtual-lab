using Engine.UI;
using JUnity.Utilities;
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

        public bool HandleMouseDown(Vector2 mousePosition, MouseKey key)
        {
            var existingElement = _mouseUpListeners[key];
            if (existingElement != null)
            {
                existingElement.HandleMouseMove(mousePosition, key);
                return true;
            }

            foreach (var element in _elements)
            {
                if (element.IsVisible && UIHelper.IsCursorOverElement(mousePosition, element))
                {
                    element.HandleMouseDown(mousePosition, key);
                    _mouseUpListeners[key] = element;

                    return true;
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
                element.HandleMouseUp(mousePosition, key);
            }
        }

        public bool HandleMouseScroll(Vector2 mousePosition, int deltaScrollValue)
        {
            foreach (var element in _elements)
            {
                if (element.IsVisible && UIHelper.IsCursorOverElement(mousePosition, element))
                {
                    element.HandleMouseScroll(mousePosition, deltaScrollValue);
                    return true;
                }
            }

            return false;
        }

        public void RegisterElement(UIElement element)
        {
            _elements.Add(element);
            _elements.Sort((x, y) => x.ZOrder.CompareTo(y.ZOrder));
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
    }
}
