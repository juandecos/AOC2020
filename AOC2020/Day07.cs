using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(7)]
    class Day07 : Solver
    {
        public struct Child
        {
            public int Number;
            public string Name;
        }

        readonly Dictionary<string, List<Child>> Map = new Dictionary<string, List<Child>>();

        public Day07()
        {
            Rows.ForEach(x => ParseRow(x));
        }

        public override object SolveOne()
        {
            return Map.Count(x => CanContain(x.Key, "shiny gold")) - 1;
        }

        public override object SolveTwo()
        {
            return GetBagCount("shiny gold");
        }

        bool CanContain(string name, string searchName)
        {
            return name == searchName || Map[name].Exists(x => CanContain(x.Name, searchName));
        }

        int GetBagCount(string name)
        {
            return Map[name].Sum(x => x.Number * (1 + GetBagCount(x.Name)));
        }

        void ParseRow(string row)
        {
            var parts = row.Split(" bags contain ");
            if (parts[1] == "no other bags.")
            {
                Map[parts[0]] = new List<Child>();
                return;
            }
            Map[parts[0]] = parts[1]
                .Replace(".", "").Replace(" bags", "").Replace(" bag", "")
                .Split(", ")
                .Select(child =>
                    new Child
                    {
                        Number = int.Parse(child.Substring(0, child.IndexOf(" "))),
                        Name = child.Substring(child.IndexOf(" ") + 1)
                    }).ToList();
        }
    }
}
