using Engine.Services;
using SharpDX;
using System;

namespace Engine.UI
{
    public abstract class UIElement : IDisposable
    {
        private bool _isDisposed;

        public bool IsVisible { get; set; } = true;

        public Vector2 Position { get; set; }

        public float ZOrder { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        internal abstract bool HandleMouseDown(Vector2 mousePosition, MouseKey key, bool isJustPressed);

        internal abstract void HandleMouseUp(Vector2 mousePosition, MouseKey key);

        internal abstract void HandleMouseUpOutOfElement(MouseKey key);

        internal abstract bool HandleMouseScroll(Vector2 mousePosition, int deltaScrollValue);

        internal abstract void Render(GraphicsRenderer renderer);

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    JUnity.Instance.UIController.RemoveElement(this);
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UIElement()
        {
            Dispose(false);
        }
    }
}
