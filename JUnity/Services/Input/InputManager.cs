using SharpDX;
using SharpDX.DirectInput;
using SharpDX.Windows;
using System;
using System.Windows.Forms;

namespace JUnity.Services.Input
{
    public enum MouseKey
    {
        Left,
        Right,
        Middle,
        Mouse4,
        Mouse5,
    }

    public sealed class InputManager : IDisposable
    {
        private RenderForm _renderForm;
        private DirectInput _directInput = new DirectInput();

        private Keyboard _keyboard;
        private KeyboardState _keyboardState;
        private KeyboardState _lastKeyboardState;

        private Mouse _mouse;
        private MouseState _mouseState;
        private MouseState _lastMouseState;
        private readonly bool[] _supressedKeys = new bool[8];

        public event EventHandler<MouseClickEventArgs> OnMouseBottonDown;
        public event EventHandler<MouseClickEventArgs> OnMouseBottonUp;

        public void Initialize(RenderForm renderForm)
        {
            _renderForm = renderForm;

            _keyboard = new Keyboard(_directInput);
            _keyboard.SetCooperativeLevel(_renderForm.Handle,
                CooperativeLevel.Background | CooperativeLevel.NonExclusive);
            _keyboard.Acquire();

            _mouse = new Mouse(_directInput);
            _mouse.SetCooperativeLevel(_renderForm.Handle,
                CooperativeLevel.Background | CooperativeLevel.NonExclusive);
            _mouse.Acquire();
        }

        public void Update()
        {
            _lastMouseState = _mouseState;
            _mouseState = _mouse.GetCurrentState();

            if (_lastMouseState != null)
            {
                var position = GetCursorPosition();
                for (int i = 0; i < 8; i++)
                {
                    if (_mouseState.Buttons[i])
                    {
                        if (!_lastMouseState.Buttons[i] && Engine.Instance.UIController.HandleMouseDown(position, (MouseKey)i) && !_supressedKeys[i])
                        {
                            _supressedKeys[i] = true;

                        }

                        if (_supressedKeys[i])
                        {
                            _mouseState.Buttons[i] = false;
                        }

                        OnMouseBottonDown?.Invoke(this, new MouseClickEventArgs((MouseKey)i, position));
                    }
                    else
                    {
                        if (_supressedKeys[i])
                        {
                            Engine.Instance.UIController.HandleMouseUp(position, (MouseKey)i);
                            _supressedKeys[i] = false;
                        }
                        if(_lastMouseState.Buttons[i]) OnMouseBottonUp?.Invoke(this, new MouseClickEventArgs((MouseKey)i, position)); 
                    }
                }

                if (_mouseState.Z != 0)
                {
                    Engine.Instance.UIController.HandleMouseScroll(position, _mouseState.Z);
                    _mouseState.Z = 0;
                }
            }

            var newKeyboardState = _keyboard.GetCurrentState();
            if (!Engine.Instance.UIController.HandleFocusedKeyboardInput(newKeyboardState))
            {
                _lastKeyboardState = _keyboardState;
                _keyboardState = newKeyboardState;
            }
        }

        private Vector2 _lastOffSet = Vector2.Zero;

        public Vector2 GetMouseOffset()
        {
            var newOffSet = new Vector2(_mouseState.X, -_mouseState.Y);
            if (_lastOffSet != newOffSet)
            {
                _lastOffSet = newOffSet;
                return newOffSet;
            }
            else
            {
                return Vector2.Zero;
            }
        }

        public bool IsKeyPressed(Key key)
        {
            return _keyboardState != null && _keyboardState.IsPressed(key);
        }

        public bool IsKeyPressed(MouseKey key)
        {
            return _keyboardState != null && _mouseState.Buttons[(int)key];
        }

        public bool IsKeyJustPressed(Key key)
        {
            if (_lastKeyboardState == null)
            {
                return false;
            }

            return _keyboardState != null && !_lastKeyboardState.IsPressed(key) && _keyboardState.IsPressed(key);
        }

        public bool IsKeyJustPressed(MouseKey key)
        {
            if (_lastMouseState == null)
            {
                return false;
            }

            return _keyboardState != null && !_lastMouseState.Buttons[(int)key] && _mouseState.Buttons[(int)key];
        }

        public bool IsKeyJustReleased(Key key)
        {
            if (_lastKeyboardState == null)
            {
                return false;
            }

            return _keyboardState != null && _lastKeyboardState.IsPressed(key) && !_keyboardState.IsPressed(key);
        }

        public bool IsKeyJustReleased(MouseKey key)
        {
            if (_lastMouseState == null)
            {
                return false;
            }

            return _keyboardState != null && _lastMouseState.Buttons[(int)key] && !_mouseState.Buttons[(int)key];
        }

        public Vector2 GetCursorPosition()
        {
            var cursorPosition = _renderForm.PointToClient(Cursor.Position);
            return new Vector2(cursorPosition.X / (float)_renderForm.ClientRectangle.Width,
                cursorPosition.Y / (float)_renderForm.ClientRectangle.Height);
        }

        public int GetScrollDeltaValue()
        {
            return _mouseState != null ? _mouseState.Z : 0;
        }

        public void Dispose()
        {
            _mouse.Unacquire();
            _keyboard.Unacquire();
            SharpDX.Utilities.Dispose(ref _mouse);
            SharpDX.Utilities.Dispose(ref _keyboard);
            SharpDX.Utilities.Dispose(ref _directInput);
        }
    }
    public class MouseClickEventArgs : EventArgs
    {
        public MouseClickEventArgs(MouseKey key, Vector2 position)
        {
            Key = key;
            Position = position;
        }

        public MouseKey Key { get; }
        public Vector2 Position { get; }
    }
}
