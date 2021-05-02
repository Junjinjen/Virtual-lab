using System;
using System.Collections.Generic;

namespace JUnity.Services.Graphics.Utilities
{
    internal static class Helper
    {
        public static void DisposeDictionaryElements<U, T>(IDictionary<U, T> dictionary)
            where T : class, IDisposable
        {
            foreach (var item in dictionary)
            {
                item.Value.Dispose();
            }
        }
    }
}
