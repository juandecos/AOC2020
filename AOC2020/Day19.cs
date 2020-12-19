using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(19)]
    class Day19 : Solver
    {
        readonly Dictionary<int, List<List<string>>> RuleMap = new Dictionary<int, List<List<string>>>();
        readonly List<string> Messages;

        public Day19()
        {
            RuleMap = GroupRows().First().Select(x => x.Split(": ")).ToDictionary(
                x => int.Parse(x[0]),
                x => x[1].Split(" | ").Select(y => y.Split(" ").ToList()).ToList());
            Messages = GroupRows().Skip(1).First();
        }

        public override object SolveOne()
        {
            var combinations = new HashSet<string>();
            AddCombinations(0, combinations);
            return Messages.Count(x => combinations.Contains(x));
        }

        public override object SolveTwo()
        {
            /*
             * Noted that starting with zero, we first hit two loops, but each loop
             * only comprises 42's and 31's:
             *      0: 8 11
             *      8: 42 | 42 8
             *      11: 42 31 | 42 11 31
             * So everything is some combination of 42's and 31's.
             * Via trial run, determined all 42's and 31's are 8 digits and mutually exclusive sets.
             * Also noted that "8" is equivalent to "1 or more 42's".
             * Also noted that "11" is equivalent to "1 or more 42's followed by the same number of 31's"
             * Combined, this means zero is "2 or more 42's followed by 1 or more 31's, with total 42's being more than 31's"
             * So a-counting we will go.
            */
            var full31 = new HashSet<string>();
            AddCombinations(31, full31);
            var full42 = new HashSet<string>();
            AddCombinations(42, full42);
            return Messages.Count(x => IsValid(full31, full42, x));
        }

        bool IsValid(HashSet<string> full31, HashSet<string> full42, string message)
        {
            if (!full42.Contains(message.Substring(0, 8)))
                return false;
            int count42 = 1;
            int count31 = 0;
            string stage = "42";
            for (int i = 8; i < message.Length; i += 8)
            {
                var sub = message.Substring(i, 8);
                (bool is42, bool is31) = (full42.Contains(sub), full31.Contains(sub));
                if (!is42 && !is31)
                    return false;
                if (is42 && stage != "42")
                    return false;
                if (is42)
                    count42++;
                else
                {
                    stage = "31";
                    count31++;
                }
            }
            return count42 > count31 && count31 > 0;
        }

        void AddCombinations(int current, HashSet<string> combinations)
        {
            if (RuleMap[current][0][0].StartsWith("\""))
            {
                combinations.Add(RuleMap[current][0][0].Substring(1, 1));
                return;
            }
            foreach (var set in RuleMap[current])
            {
                if (set.Count == 1)
                {
                    AddCombinations(int.Parse(set[0]), combinations);
                    continue;
                }
                var leftSide = new HashSet<string>();
                var rightSide = new HashSet<string>();
                AddCombinations(int.Parse(set[0]), leftSide);
                AddCombinations(int.Parse(set[1]), rightSide);
                foreach (var left in leftSide)
                    foreach (var right in rightSide)
                        combinations.Add(left + right);
            }
        }
    }
}
