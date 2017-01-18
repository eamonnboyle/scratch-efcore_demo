namespace Lego.Shared {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Menu : IEnumerable {
        private const char QuitKey = 'q';
        private readonly List<(char key, string text, Action action)> options = new List<(char, string, Action)>();

        public IEnumerator GetEnumerator() {
            return options.GetEnumerator();
        }

        public void Add(char key, string title, Action action) {
            options.Add((key, title, action));
        }

        public void Add((char key, string title, Action action) tuple) {
            options.Add(tuple);
        }

        public void Show() {
            while (true) {
                DisplayMenu();
                var input = ReadSelection();
                if (IsExit(input)) break;

                options.First(x => x.key == input).action();
            }
        }

        private char ReadSelection() {
            var keys = options.Select(x => x.Item1).ToList();
            var input = ' ';
            do {
                input = char.ToLower(Console.ReadKey(true).KeyChar);
            } while (!keys.Contains(input) && !IsExit(input));
            return input;
        }

        private void DisplayMenu() {
            Console.WriteLine("==== Menu ====");
            options.ForEach(x => Console.WriteLine(x.Item1 + ". " + x.Item2));
            Console.WriteLine(QuitKey + ". Quit");
            Console.WriteLine("== Please select an option ==");
        }

        private bool IsExit(char input) => input == QuitKey;
    }
}