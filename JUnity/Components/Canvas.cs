using JUnity.Services.UI.Elements;
using System;
using System.Collections.Generic;

namespace JUnity.Components
{
    public sealed class Canvas : IDisposable
    {
        private readonly List<UIElement> _elements = new List<UIElement>();

        public void RegisterElement(UIElement element)
        {
            if (!_elements.Contains(element))
            {
                _elements.Add(element);
                Engine.Instance.UIController.RegisterElement(element);
            }
        }

        public void Dispose()
        {
            foreach (var item in _elements)
            {
                Engine.Instance.UIController.RemoveElement(item);
            }
        }
    }
}
