using Engine.Services;
using SharpDX;
using System;

namespace Engine.UI
{
    public abstract class UIElement : IDisposable
    {
        private bool _isDisposed;

        public bool IsVisible { get; set; }

        public Vector2 Position { get; set; }

        public float ZOrder { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        /// <summary>
        /// Handle mouse click
        /// </summary>
        /// <param name="clickPosition">Click position (relative to element position)</param>
        /// <returns>Is click absorbed</returns>
        internal abstract bool HandleClick(Vector2 clickPosition);

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
