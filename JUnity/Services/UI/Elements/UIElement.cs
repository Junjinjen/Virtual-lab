﻿using JUnity.Services.Graphics;
using JUnity.Services.Input;
using SharpDX;
using System;

namespace JUnity.Services.UI.Elements
{
    public abstract class UIElement : IDisposable
    {
        private bool _isDisposed;

        public bool IsVisible { get; set; } = true;

        public Vector2 Position { get; set; }

        public float ZOrder { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        internal virtual void HandleMouseDown(Vector2 mousePosition, MouseKey key) { }

        internal virtual void HandleMouseMove(Vector2 mousePosition, MouseKey key) { }

        internal virtual void HandleMouseUp(Vector2 mousePosition, MouseKey key) { }

        internal virtual void HandleMouseScroll(Vector2 mousePosition, int deltaScrollValue) { }

        protected internal abstract void Render(GraphicsRenderer renderer);

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    Engine.Instance.UIController.RemoveElement(this);
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
