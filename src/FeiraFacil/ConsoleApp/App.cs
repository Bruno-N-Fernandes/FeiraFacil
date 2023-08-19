using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class App
    {
        private readonly StringComparison _stringComparison;
        private readonly List<Menu> _menus;
        private bool _active;

        public App(bool caseSensitive = false)
        {
            _stringComparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            _menus = new();
            _active = true;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public App AddMenu(string code, string title, Action action)
        {
            _menus.Add(new Menu { Code = code, Title = title, Action = action });
            return this;
        }

        public void Run()
        {
            while (Exibir())
            {
                Console.Write("\r\n\r\n\r\nPressione Enter para voltar para o Menu... ");
                Console.ReadLine();
                Console.Clear();
            }
        }

        public bool Exibir()
        {
            Console.WriteLine("Menu");
            foreach (var menu in _menus)
                menu.Exibir();
            Console.Write("Selecione uma opção: ");

            var menuCode = Console.ReadLine();
            Console.Clear();
            var selectedMenu = _menus.Find(menu => menu.Code.Equals(menuCode, _stringComparison));
            selectedMenu?.Executar();

            return _active;
        }

        public void Sair() => _active = false;


        private sealed class Menu
        {
            internal string Code { get; set; }
            internal string Title { get; set; }
            internal Action Action { get; set; }

            internal void Exibir() => Console.WriteLine($"{Code} - {Title}");

            internal void Executar() => Action?.Invoke();
        }
    }
}