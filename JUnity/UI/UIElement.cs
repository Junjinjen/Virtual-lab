using JUnity.Services;
using SharpDX;

namespace JUnity.UI
{
    public abstract class UIElement
    {
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
    }
}
