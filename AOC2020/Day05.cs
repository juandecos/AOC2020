using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020
{
    [Day(5)]
    class Day05 : Solver
    {
        public override object SolveOne()
        {
            return Rows.Max(x => ParseSeatId(x));
        }

        public override object SolveTwo()
        {
            var ids = Rows.Select(x => ParseSeatId(x));
            return ids.OrderBy(x => x).First(x => !ids.Contains(x + 1)) + 1;
        }

        int ParseSeatId(string input)
        {
            return Convert.ToInt32(Regex.Replace(Regex.Replace(input, "[BR]", "1"), "[FL]", "0"), 2);
        }
    }
}
