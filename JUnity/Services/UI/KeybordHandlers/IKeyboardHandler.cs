using SharpDX.DirectInput;
using System.Collections.Generic;

namespace JUnity.Services.UI.KeybordHandlers
{
    public interface IKeyboardHandler
    {
        string HandleInput(string currentText, IEnumerable<Key> justPressedKeys, IEnumerable<Key> pressedKeys);
    }
}
