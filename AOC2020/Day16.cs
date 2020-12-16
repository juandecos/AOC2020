using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(16)]
    class Day16 : Solver
    {
        class Rule
        {
            public string FieldName;
            public int R1Start;
            public int R1End;
            public int R2Start;
            public int R2End;
            public Rule(string input)
            {
                var parts = input.Split(": ");
                var subparts = parts[1].Split(" or ");
                var range1 = subparts[0].Split("-");
                var range2 = subparts[1].Split("-");
                FieldName = parts[0];
                R1Start = int.Parse(range1[0]);
                R1End = int.Parse(range1[1]);
                R2Start = int.Parse(range2[0]);
                R2End = int.Parse(range2[1]);
            }
            public bool Check(int input) =>
                (input >= R1Start && input <= R1End) || (input >= R2Start && input <= R2End);
        }

        readonly List<Rule> Rules;
        readonly List<int> MyTicket;
        readonly List<List<int>> Tickets;

        public Day16()
        {
            var groups = GroupRows().ToList();
            Rules = groups[0]
                .Select(x => new Rule(x))
                .ToList();
            MyTicket = groups[1][1]
                .Split(',')
                .Select(x => int.Parse(x))
                .ToList();
            Tickets = groups[2]
                .Skip(1)
                .Select(x => x.Split(',').Select(y => int.Parse(y)).ToList())
                .ToList();
        }

        public override object SolveOne()
        {
            return Tickets.Sum(ticket => ticket.Where(field => !Rules.Any(rule => rule.Check(field))).Sum());
        }

        public override object SolveTwo()
        {
            var goodTickets = Tickets.Where(ticket => ticket.All(field => Rules.Any(rule => rule.Check(field)))).ToList();
            int fieldCount = goodTickets[0].Count;
            var map = new Dictionary<int, string>();
            while (true)
            {
                for (int i = 0; i < fieldCount; i++)
                {
                    var possibles = Rules
                        .Where(x => !map.Values.Contains(x.FieldName))
                        .Where(x => goodTickets.All(ticket => x.Check(ticket[i])));
                    if (possibles.Count() == 1)
                    {
                        map[i] = possibles.First().FieldName;
                        break;
                    }
                }
                if (map.Count == Rules.Count)
                    break;
            }
            return map
                .Where(x => x.Value.StartsWith("departure"))
                .Select(x => MyTicket[x.Key])
                .Aggregate(1L, (x, y) => x * y);
        }
    }
}
