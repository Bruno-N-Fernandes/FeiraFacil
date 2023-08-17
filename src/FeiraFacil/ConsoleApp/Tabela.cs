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

    public class Tabela<TItem> : ITabela
    {
        public const string Top = "┌┬┐";
        public const string Mid = "├┼┤";
        public const string Det = "│││";
        public const string Fot = "└┴┘";

        private readonly List<Field> _fields = new List<Field>();

        public void Add(string title, int length, Func<TItem, string> selector)
            => _fields.Add(new Field(title, length, selector));

        public void Draw<TItem2>(IEnumerable<TItem2> items) => Draw(items.OfType<TItem>());

        public void Draw(IEnumerable<TItem> items)
        {
            Console.WriteLine($"┌─{string.Join("─┬─", _fields.Select(x => x.Border))}─┐");
            Console.WriteLine($"│ {string.Join(" │ ", _fields.Select(x => x.Column))} │");
            Console.WriteLine($"├─{string.Join("─┼─", _fields.Select(x => x.Border))}─┤");
            foreach (var item in items)
                Console.WriteLine($"│ {string.Join(" │ ", _fields.Select(x => x.Get(item)))} │");
            Console.WriteLine($"└─{string.Join("─┴─", _fields.Select(x => x.Border))}─┘");
        }

        public class Field
        {
            private readonly int _length;
            private readonly Func<TItem, string> _selector;

            public readonly string Border;
            public readonly string Column;

            public Field(string title, int length, Func<TItem, string> selector)
            {
                _length = length;
                Column = _length > 0 ? title.PadLeft(_length) : title.PadRight(-_length);
                Border = new string('─', Math.Abs(_length));
                _selector = selector;
            }

            public string Get(TItem item)
            {
                var value = _selector?.Invoke(item);
                return _length > 0 ? value?.PadLeft(_length) : value?.PadRight(-_length);
            }
        }
    }
}