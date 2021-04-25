using JUnity.Services.Input;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectInput;

namespace JUnity.Components.UI
{
    public abstract class UIElement
    {
        public bool IsVisible { get; set; } = true;

        public Vector2 Position { get; set; }

        public float ZOrder { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        internal virtual void HandleMouseDown(Vector2 mousePosition, MouseKey key) { }

        internal virtual void HandleMouseMove(Vector2 mousePosition, MouseKey key) { }

        internal virtual void HandleMouseUp(Vector2 mousePosition, MouseKey key) { }

        internal virtual void HandleMouseScroll(Vector2 mousePosition, int deltaScrollValue) { }

        internal virtual void HandleKeyboardInput(KeyboardState keyboardState) { }

        internal virtual void OnFocusLost() { }

        protected internal abstract void Render(RenderTarget renderTarget);
    }
}
