using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTalk.MorseMessenger.Common
{
    public static class MorseCodeTranslator
    {
        private static Dictionary<char, IEnumerable<MorseSymbol>> _morseTable = new Dictionary<char, IEnumerable<MorseSymbol>>()
            {
                {'a', new [] { MorseSymbol.Dot, MorseSymbol.Dash } },
                {'b', new [] { MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot } },
                {'c', new [] { MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dot } },
                {'d', new [] { MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dot } },
                {'e', new [] { MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dot } },
                {'f', new [] { MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dot } },
                {'g', new [] { MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dot } },
                {'h', new [] { MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot } },
                {'i', new [] { MorseSymbol.Dot, MorseSymbol.Dot } },
                {'j', new [] { MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash } },
                {'k', new [] { MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dash } },
                {'l', new [] { MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dot } },
                {'m', new [] { MorseSymbol.Dash, MorseSymbol.Dash } },
                {'n', new [] { MorseSymbol.Dash, MorseSymbol.Dot } },
                {'o', new [] { MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash } },
                {'p', new [] { MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dot } },
                {'q', new [] { MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dash } },
                {'r', new [] { MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dot } },
                {'s', new [] { MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot } },
                {'t', new [] { MorseSymbol.Dash } },
                {'u', new [] { MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dash } },
                {'v', new [] { MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dash } },
                {'w', new [] { MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dash } },
                {'x', new [] { MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dash } },
                {'y', new [] { MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dash } },
                {'z', new [] { MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dot } },
                {'0', new [] { MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash } },
                {'1', new [] { MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash } },
                {'2', new [] { MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash } },
                {'3', new [] { MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dash, MorseSymbol.Dash } },
                {'4', new [] { MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dash } },
                {'5', new [] { MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot } },
                {'6', new [] { MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot } },
                {'7', new [] { MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dot, MorseSymbol.Dot } },
                {'8', new [] { MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dot, MorseSymbol.Dot } },
                {'9', new [] { MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dot } },
                {' ', new [] { MorseSymbol.Dash, MorseSymbol.Dash, MorseSymbol.Dash } }
            };

        public static IEnumerable<MorseSymbol> Get(char c)
        {
            if (_morseTable.TryGetValue(c, out var result))
            {
                return result;
            }

            return null;
        }
    }
}
