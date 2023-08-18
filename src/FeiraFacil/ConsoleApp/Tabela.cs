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
        private readonly List<Field> _fields = new List<Field>();
        private readonly string _title;

        public Tabela(string title)
        {
            _title = title;
        }

        public void Add(string title, int length, Func<TItem, string> selector)
            => _fields.Add(new Field(title, length, selector));

        public void Draw<TItem2>(IEnumerable<TItem2> items) => Draw(items.OfType<TItem>());

        public void Draw(IEnumerable<TItem> items)
        {
            var paddingRight = _fields.Count * 3 + _fields.Sum(x => Math.Abs(x.Length)) - 3;
            var paddingLeft = (paddingRight + _title.Length) / 2;

            Console.WriteLine($"╔═{string.Join("═══", _fields.Select(x => x.Border2))}═╗");
            Console.WriteLine($"║ {_title.PadLeft(paddingLeft).PadRight(paddingRight)} ║");
            Console.WriteLine($"╠═{string.Join("═╦═", _fields.Select(x => x.Border2))}═╣");
            Console.WriteLine($"║ {string.Join(" ║ ", _fields.Select(x => x.Column1))} ║");
            Console.WriteLine($"╠═{string.Join("═╬═", _fields.Select(x => x.Border2))}═╣");
            Loop(items, it => $"║ {string.Join(" ║ ", _fields.Select(x => x.Get(it)))} ║");
            Console.WriteLine($"╚═{string.Join("═╩═", _fields.Select(x => x.Border2))}═╝");
        }

        private void Loop(IEnumerable<TItem> items, Func<TItem, string> draw)
        {
            foreach (var item in items)
                Console.WriteLine(draw.Invoke(item));
        }

        public class Field
        {
            private readonly Func<TItem, string> _selector;
            public readonly int Length;

            public readonly string Border1;
            public readonly string Border2;
            public readonly string Column1;

            public Field(string title, int length, Func<TItem, string> selector)
            {
                Length = length;
                Column1 = Length > 0 ? title.PadLeft(Length) : title.PadRight(-Length);
                Border1 = new string('─', Math.Abs(Length));
                Border2 = new string('═', Math.Abs(Length));
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