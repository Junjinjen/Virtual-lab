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
        /// Handle mouse down event.
        /// </summary>
        /// <param name="mousePosition">Mouse position (relative to element position)</param>
        /// <param name="key">Pressed key</param>
        /// <param name="isJustPressed">True if the key is became pressed on this frame</param>
        /// <returns>Is key absorbed</returns>
        internal abstract bool HandleMouseDown(Vector2 mousePosition, MouseKey key, bool isJustPressed);

        /// <summary>
        /// Handle mouse up event. Called only if the key was suppressed by UI.
        /// </summary>
        /// <param name="mousePosition">Mouse position (relative to element position)</param>
        /// <param name="key">Pressed key</param>
        internal abstract void HandleMouseUp(Vector2 mousePosition, MouseKey key);

        /// <summary>
        /// Handle mouse scroll event.
        /// </summary>
        /// <param name="mousePosition">Mouse position (relative to element position)</param>
        /// <param name="deltaScrollValue">Scroll value</param>
        /// <returns>Is scroll absorbed</returns>
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
