using SharpDX;
using SharpDX.DirectInput;
using SharpDX.Windows;
using System;
using System.Windows.Forms;

namespace Engine.Services
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
        private RenderForm _renderForm;
        private DirectInput _directInput = new DirectInput();

        private Keyboard _keyboard;
        private KeyboardState _keyboardState;
        private KeyboardState _lastKeyboardState;

        private Mouse _mouse;
        private MouseState _mouseState;
        private MouseState _lastMouseState;
        private readonly bool[] _supressedKeys = new bool[8];

        public void Initialize(RenderForm renderForm)
        {
            _renderForm = renderForm;

            _keyboard = new Keyboard(_directInput);
            _keyboard.SetCooperativeLevel(_renderForm.Handle,
                CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);
            _keyboard.Acquire();

            _mouse = new Mouse(_directInput);
            _mouse.SetCooperativeLevel(_renderForm.Handle,
                CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);
            _mouse.Acquire();
        }

        public void Update()
        {
            _lastKeyboardState = _keyboardState;
            _keyboardState = _keyboard.GetCurrentState();

            _lastMouseState = _mouseState;
            _mouseState = _mouse.GetCurrentState();

            var position = GetCursorPosition();
            for (int i = 0; i < _mouseState.Buttons.Length; i++)
            {
                if (_mouseState.Buttons[i])
                {
                    if (JUnity.Instance.UIController.HandleMouseDown(position, (MouseKey)i, !_lastMouseState.Buttons[i] && !_supressedKeys[i]))
                    {
                        _mouseState.Buttons[i] = false;
                        _supressedKeys[i] = true;
                    }
                }
                else if (_supressedKeys[i])
                {
                    JUnity.Instance.UIController.HandleMouseUp(position, (MouseKey)i);
                    _supressedKeys[i] = false;
                }
            }
        }

        /// <summary>
        /// Checks if keyboad key is pressed.
        /// </summary>
        /// <param name="key">Keyboard key</param>
        /// <returns>True if the key is pressed</returns>
        public bool IsKeyPressed(Key key)
        {
            return _keyboardState.IsPressed(key);
        }

        /// <summary>
        /// Checks if mouse key is pressed.
        /// Returns false if the key is pressed but the cursor is over the UI element that absorbs input.
        /// </summary>
        /// <param name="key">Mouse key</param>
        /// <returns>True if the key is pressed and not absorbed by UI</returns>
        public bool IsKeyPressed(MouseKey key)
        {
            return _mouseState.Buttons[(int)key];
        }

        /// <summary>
        /// Checks if keyboad key is pressed on this frame.
        /// </summary>
        /// <param name="key">Keyboard key</param>
        /// <returns>True if the key is became pressed on this frame</returns>
        public bool IsKeyJustPressed(Key key)
        {
            return !_lastKeyboardState.IsPressed(key) && _keyboardState.IsPressed(key);
        }

        /// <summary>
        /// Checks if mouse key is pressed on this frame.
        /// Returns true only if the click wasn't started over the UI.
        /// </summary>
        /// <param name="key">Mouse key</param>
        /// <returns>True if the key is became pressed on this frame and the cursor isn't over the UI</returns>
        public bool IsKeyJustPressed(MouseKey key)
        {
            return !_lastMouseState.Buttons[(int)key] && !_supressedKeys[(int)key] && _mouseState.Buttons[(int)key];
        }

        /// <summary>
        /// Returns true if the keyboard key is released on this frame.
        /// </summary>
        /// <param name="key">Keyboard key</param>
        /// <returns>True if the key is became released on this frame</returns>
        public bool IsKeyJustReleased(Key key)
        {
            return _lastKeyboardState.IsPressed(key) && !_keyboardState.IsPressed(key);
        }

        /// <summary>
        /// Returns true if the mouse key is released on this frame.
        /// </summary>
        /// <param name="key">Mouse key</param>
        /// <returns>True if the key is became released on this frame and wasn't absorbed by UI during the press</returns>
        public bool IsKeyJustReleased(MouseKey key)
        {
            return _lastMouseState.Buttons[(int)key] && !_supressedKeys[(int)key] && !_mouseState.Buttons[(int)key];
        }

        public Vector2 GetCursorPosition()
        {
            return new Vector2((Cursor.Position.X - _renderForm.DesktopLocation.X) / (float)_renderForm.Width,
                (Cursor.Position.Y - _renderForm.DesktopLocation.Y) / (float)_renderForm.Height);
        }

        public int GetScrollDeltaValue()
        {
            return _mouseState.Z;
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
}
