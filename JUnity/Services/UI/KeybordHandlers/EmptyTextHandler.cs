using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
