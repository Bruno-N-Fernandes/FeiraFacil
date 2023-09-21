using System.Collections.Generic;

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
}