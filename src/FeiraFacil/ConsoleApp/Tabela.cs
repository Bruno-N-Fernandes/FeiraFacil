using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ConsoleApp
{
    public interface ITabela
    {
        void Draw<TItem2>(IEnumerable<TItem2> items);
    }

    /// <summary>
    /// https://www.w3schools.com/charsets/ref_utf_box.asp
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class Tabela<TItem> : ITabela
    {
        private readonly List<Field> _fields = new();
        private readonly string Title;

        public Tabela(string title) => Title = title;

        public void Add(string title, int length, Func<TItem, string> selector)
            => _fields.Add(new Field(title, length, selector));

        public void Draw<TItem2>(IEnumerable<TItem2> items) => Draw(items.OfType<TItem>());

        public void Draw(IEnumerable<TItem> items)
        {
            var paddingRight = _fields.Count * 3 + _fields.Sum(x => Math.Abs(x.Length)) - 3;
            var paddingLeft = (paddingRight + Title.Length) / 2;

            Console.WriteLine($"╔═{string.Join("═══", _fields.Select(x => x.Border))}═╗");
            Console.WriteLine($"║ {Title.PadLeft(paddingLeft).PadRight(paddingRight)} ║");
            Console.WriteLine($"╠═{string.Join("═╦═", _fields.Select(x => x.Border))}═╣");
            Console.WriteLine($"║ {string.Join(" ║ ", _fields.Select(x => x.Column))} ║");
            Console.WriteLine($"╠═{string.Join("═╬═", _fields.Select(x => x.Border))}═╣");
            Write(items, i => $"║ {string.Join(" ║ ", _fields.Select(x => x.Get(i)))} ║");
            Console.WriteLine($"╚═{string.Join("═╩═", _fields.Select(x => x.Border))}═╝");
        }

        private void Write(IEnumerable<TItem> items, Func<TItem, string> draw)
        {
            foreach (var item in items)
                Console.WriteLine(draw.Invoke(item));
        }

        public class Field
        {
            private readonly Func<TItem, string> _selector;
            public readonly int Length;

            public readonly string Border;
            public readonly string Column;

            public Field(string title, int length, Func<TItem, string> selector)
            {
                Length = length;
                Column = Length > 0 ? title.PadLeft(Length) : title.PadRight(-Length);
                Border = new string('═', Math.Abs(Length));
                _selector = selector;
            }

            public string Get(TItem item)
            {
                var value = _selector?.Invoke(item);
                return Length > 0 ? value?.PadLeft(Length) : value?.PadRight(-Length);
            }
        }


        public static class Box
        {
            public const string box1 = @"
┌───┐
│   │
├─┬─┤
│ │ │
├─┼─┤
│ │ │
└─┴─┘
";

            public const string box2 = @"
╔═══╗
║   ║
╠═╦═╣
║ ║ ║
╠═╬═╣
║ ║ ║
╚═╩═╝
";

            public const string box3 = @"
╔═══╗
║   ║
╠═╤═╣
║ │ ║
╟─┼─╢
║ │ ║
╚═╧═╝
";

        }
    }
}