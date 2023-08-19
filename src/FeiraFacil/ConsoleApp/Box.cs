using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ConsoleApp
{
    /// <summary>
    /// https://www.w3schools.com/charsets/ref_utf_box.asp
    /// </summary>
    public interface IBox
    {
        void Draw<TType>(IEnumerable<TType> items);

        public static readonly string[,] LineBorder1 = new string[,]
        {
            { "──", "───", "──" },
            { "┌─", "───", "─┐" },
            { "│ ", "   ", " │" },
            { "├─", "─┬─", "─┤" },
            { "│ ", " │ ", " │" },
            { "├─", "─┼─", "─┤" },
            { "│ ", " │ ", " │" },
            { "└─", "─┴─", "─┘" },
        };

        public static readonly string[,] LineBorder2 = new string[,]
        {
            { "══", "═══", "══" },
            { "╔═", "═══", "═╗" },
            { "║ ", "   ", " ║" },
            { "╠═", "═╦═", "═╣" },
            { "║ ", " ║ ", " ║" },
            { "╠═", "═╬═", "═╣" },
            { "║ ", " ║ ", " ║" },
            { "╚═", "═╩═", "═╝" },
        };
    }

    public class Box<TItem> : IBox
    {
        private readonly List<Field> _fields = new();
        private readonly string _boxHeader;
        private readonly string[,] Line;
        private readonly char _border;

        public Box(string boxHeader, string[,] lineBorder)
        {
            _boxHeader = boxHeader;
            Line = lineBorder ?? IBox.LineBorder2;
            _border = Line[0, 0][0];
        }

        public void Add(int length, string header, Func<TItem, string> selector)
            => _fields.Add(new Field(length, header, _border, selector));

        public void Draw<TType>(IEnumerable<TType> items) => Draw(items.OfType<TItem>());

        public void Draw(IEnumerable<TItem> items)
        {
            var paddingRight = _fields.Count * 3 + _fields.Sum(x => Math.Abs(x.Length)) - 3;
            var paddingLeft = (paddingRight + _boxHeader.Length) / 2;

            Console.WriteLine($"{Line[1, 0]}{string.Join(Line[1, 1], _fields.Select(x => x.Border))}{Line[1, 2]}");
            Console.WriteLine($"{Line[2, 0]}{_boxHeader.PadLeft(paddingLeft).PadRight(paddingRight)}{Line[2, 2]}");
            Console.WriteLine($"{Line[3, 0]}{string.Join(Line[3, 1], _fields.Select(x => x.Border))}{Line[3, 2]}");
            Console.WriteLine($"{Line[4, 0]}{string.Join(Line[4, 1], _fields.Select(x => x.Header))}{Line[4, 2]}");
            Console.WriteLine($"{Line[5, 0]}{string.Join(Line[5, 1], _fields.Select(x => x.Border))}{Line[5, 2]}");
            Write(items, i => $"{Line[6, 0]}{string.Join(Line[6, 1], _fields.Select(x => x.Get(i)))}{Line[6, 2]}");
            Console.WriteLine($"{Line[7, 0]}{string.Join(Line[7, 1], _fields.Select(x => x.Border))}{Line[7, 2]}");
        }

        private static void Write(IEnumerable<TItem> items, Func<TItem, string> draw)
        {
            foreach (var item in items)
                Console.WriteLine(draw.Invoke(item));
        }

        private sealed class Field
        {
            private readonly Func<TItem, string> _selector;
            internal readonly int Length;
            internal readonly string Border;
            internal readonly string Header;

            internal Field(int length, string header, char border, Func<TItem, string> selector)
            {
                Length = length;
                Header = Length > 0 ? header.PadLeft(Length) : header.PadRight(-Length);
                Border = new string(border, Math.Abs(Length));
                _selector = selector;
            }

            internal string Get(TItem item)
            {
                var value = _selector?.Invoke(item);
                return Length > 0 ? value?.PadLeft(Length) : value?.PadRight(-Length);
            }
        }
    }
}