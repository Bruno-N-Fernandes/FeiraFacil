using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class App
    {
        private readonly List<Menu> _menus;
        private readonly StringComparison _stringComparison;
        private bool _active;

        public App(bool caseSensitive)
        {
            _menus = new();
            _stringComparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            _active = true;
        }

        public void AddMenu(string code, string title, Action action) =>
            _menus.Add(new Menu { Code = code, Title = title, Action = action });

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


        public class Menu
        {
            public string Code { get; set; }
            public string Title { get; set; }
            public Action Action { get; set; }

            public void Exibir() => Console.WriteLine($"{Code} - {Title}");

            public void Executar() => Action?.Invoke();
        }
    }
}