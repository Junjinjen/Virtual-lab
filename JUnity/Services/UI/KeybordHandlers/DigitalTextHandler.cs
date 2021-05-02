using SharpDX.DirectInput;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JUnity.Services.UI.KeybordHandlers
{
    public class DigitalTextHandler : IKeyboardHandler
    {
        public string HandleInput(string currentText, IEnumerable<Key> justPressedKeys, IEnumerable<Key> pressedKeys)
        {
            var builder = new StringBuilder(currentText);
            foreach (var key in justPressedKeys)
            {
                switch (key)
                {
                    case Key.D1:
                        builder.Append(1);
                        break;
                    case Key.D2:
                        builder.Append(2);
                        break;
                    case Key.D3:
                        builder.Append(3);
                        break;
                    case Key.D4:
                        builder.Append(4);
                        break;
                    case Key.D5:
                        builder.Append(5);
                        break;
                    case Key.D6:
                        builder.Append(6);
                        break;
                    case Key.D7:
                        builder.Append(7);
                        break;
                    case Key.D8:
                        builder.Append(8);
                        break;
                    case Key.D9:
                        builder.Append(9);
                        break;
                    case Key.D0:
                        builder.Append(0);
                        break;
                    case Key.Minus:
                        builder.Append('-');
                        break;
                    case Key.Back:
                        if (builder.Length > 0)
                        {
                            builder.Remove(builder.Length - 1, 1);
                        }
                        break;
                    case Key.Comma:
                        builder.Append(',');
                        break;
                    case Key.Period:
                        builder.Append('.');
                        break;
                }
            }

            return builder.ToString();
        }
    }
}
