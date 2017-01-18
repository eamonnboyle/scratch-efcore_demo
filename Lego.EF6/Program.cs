using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lego.EF6 {
    using Lego.Shared;

    class Program {
        private static void Main(string[] args) {
            var menu = new Menu();
            menu.Add('1', "Recreate Database", RecreateDatabase);
            menu.Add('2', "Dump Database", DumpDatabase);
            menu.Add('3', "Count Sets", CountSets);
            menu.Show();

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void CountSets() {
            Console.WriteLine("Counting sets...");
            using (var db = new LegoDbContext()) {
                Console.WriteLine(db.Sets.Count());
            }
        }

        private static void DumpDatabase() {
            Console.WriteLine("Dumping Database...");
            const string tableFormat = "{0,-7} | {1 ,-16}";
            using (var context = new LegoDbContext()) {
                context.Database.Log = Console.WriteLine;
                Console.WriteLine(tableFormat, "Code", "Theme", "Description");
                var timer = Stopwatch.StartNew();
                var sets = context.Sets
                    .Include(x => x.Theme1)
                    .Where(x => x.Theme1.Description == "Ninjago")
                    .Select(x => new {x.Code, x.Description})
                    .ToList();
                timer.Stop();
                Console.WriteLine($"Found {sets.Count} in {timer.ElapsedMilliseconds} ms");

                foreach (var set in sets.Take(10))
                    Console.WriteLine(tableFormat, set.Code, set.Description);
            }
        }

        private static void RecreateDatabase() {
            Console.WriteLine("Recreating database...");
            using (var context = new LegoDbContext()) {
                context.Database.Delete();
                context.Database.Create();

                ReadFromFile("./sets.csv", context);

                context.SaveChanges();
            }
        }

        private static void ReadFromFile(string filename, LegoDbContext context) {
            var lines = File.ReadAllLines(filename).Skip(1);
            var themes = context.Themes.ToList();

            var timer = Stopwatch.StartNew();
            foreach (var line in lines) {
                var set = ParseLegoSet(line, themes);
                context.Sets.Add(set);
            }
            context.SaveChanges();
            timer.Stop();
            Console.WriteLine($"Adding all sets took {timer.ElapsedMilliseconds} ms");
        }

        private static LegoSet ParseLegoSet(string line, List<Theme> themes) {
            var record = line.Split(new[] {','}, StringSplitOptions.None);

            var set = new LegoSet(record[0], int.Parse(record[1]), record[6], GetTheme(themes, record[3])) {
                Theme2 = GetTheme(themes, record[4]),
                Theme3 = GetTheme(themes, record[5]),
                Pieces = int.Parse(record[2])
            };
            return set;
        }

        private static Theme GetTheme(IList<Theme> themes, string description) {
            var theme = themes.FirstOrDefault(x => Equals(x.Description, description));
            if (theme == null) {
                theme = new Theme(description);
                themes.Add(theme);
                return theme;
            }
            return theme;
        }
    }
}