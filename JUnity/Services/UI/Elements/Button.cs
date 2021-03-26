﻿using JUnity.Services.Graphics.UI;
using JUnity.Services.Input;
using SharpDX;
using System;

namespace JUnity.Services.UI.Elements
{
    public class Button : UIElement
    {
        internal override void HandleMouseDown(Vector2 mousePosition, MouseKey key)
        {
            if (key == MouseKey.Left)
            {
                OnMouseDown();
            }
        }

        internal override void HandleMouseUp(Vector2 mousePosition, MouseKey key)
        {
            if (key == MouseKey.Left && UIHelper.IsCursorOverElement(mousePosition, this))
            {
                OnMouseUp();
                Click?.Invoke(this, EventArgs.Empty);
            }
        }

        protected virtual void OnMouseDown() { }

        protected virtual void OnMouseUp() { }

        protected internal override void Render(UIRenderer renderer)
        {
            throw new NotImplementedException();
        }

        public event EventHandler Click;
    }
}
