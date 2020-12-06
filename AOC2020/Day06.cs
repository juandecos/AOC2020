using System;
using System.Linq;

namespace AOC2020
{
    [Day(6)]
    class Day06 : Solver
    {
        public override object SolveOne()
        {
            return GroupRows().Sum(x =>
                String.Join("", x).ToCharArray()
                    .Distinct()
                    .Count());
        }

        public override object SolveTwo()
        {
            return GroupRows().Sum(x =>
                String.Join("", x).ToCharArray()
                    .GroupBy(y => y)
                    .Count(y => y.Count() == x.Count()));
        }
    }
}
