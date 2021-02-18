using Engine.Services;
using Engine.UI;
using JUnity.Utilities;
using SharpDX;
using System;

namespace JUnity.UI
{
    public abstract class Button : UIElement
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

        public event EventHandler Click;
    }
}
