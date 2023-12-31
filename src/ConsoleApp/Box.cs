﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace ConsoleApp
{
    public class Box<TItem> : IBox
    {
        private readonly ConsoleColor[] _colors;
        private readonly List<Field> _fields;
        private readonly string _boxHeader;
        private readonly string[,] Line;
        private readonly char _border;

        public Box(string boxHeader, string[,] lineBorder, params ConsoleColor[] colors)
        {
            _boxHeader = boxHeader;
            Line = lineBorder ?? IBox.LineBorder2;
            _border = Line[0, 0][0];
            _colors = colors.Any() ? colors : new[] { ConsoleColor.DarkGray, ConsoleColor.DarkBlue};
            _fields = new();
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

        private void Write(IEnumerable<TItem> items, Func<TItem, string> draw)
        {
            var i = 0;
            var color = Console.BackgroundColor;
            foreach (var item in items)
            {
                Console.BackgroundColor = _colors[i++ % _colors.Length];
                Console.WriteLine(draw.Invoke(item));
            }
            Console.BackgroundColor = color;
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