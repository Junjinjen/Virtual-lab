using SharpDX;
using SharpDX.DirectInput;
using System;

namespace JUnity.Services
{
    public enum MouseKey
    {
        Left,
        Right,
        Middle,
        Mouse4,
        Mouse5,
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly",
        Justification = "Will be correctly disposed by JUnity class")]
    public class InputManager : IDisposable
    {
        public void Initialize(IntPtr renderFormHandler)
        {
            throw new System.NotSupportedException();
        }

        public void Update()
        {
            throw new System.NotSupportedException();
        }

        public bool IsKeyPressed(Key key)
        {
            throw new System.NotSupportedException();
        }

        public bool IsKeyPressed(MouseKey key)
        {
            throw new System.NotSupportedException();
        }

        public bool IsKeyJustPressed(Key key)
        {
            throw new System.NotSupportedException();
        }

        public bool IsKeyJustPressed(MouseKey key)
        {
            throw new System.NotSupportedException();
        }

        public Vector2 GetCursorPosition()
        {
            throw new System.NotSupportedException();
        }

        public int GetScrollValue()
        {
            throw new System.NotSupportedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
