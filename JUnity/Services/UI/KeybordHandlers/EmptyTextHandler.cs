using SharpDX.DirectInput;
using System.Collections.Generic;

namespace JUnity.Services.UI.KeybordHandlers
{
    public class EmptyTextHandler : IKeyboardHandler
    {
        public string HandleInput(string currentText, IEnumerable<Key> justPressedKeys, IEnumerable<Key> pressedKeys)
        {
            return currentText;
        }
    }
}