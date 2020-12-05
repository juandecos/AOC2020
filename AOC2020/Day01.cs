using System.Linq;

namespace AOC2020
{
    [Day(1)]
    class Day01 : Solver
    {
        public override object SolveOne()
        {
            var numbers = Rows.Select(x => int.Parse(x));
            return numbers
                .Where(x => numbers.Contains(2020 - x))
                .Select(x => x * (2020 - x))
                .First();
        }

        public override object SolveTwo()
        {
            var numbers = Rows.Select(x => int.Parse(x)).ToList();
            return numbers
                .Where(x => numbers.Exists(y => numbers.Contains(2020 - x - y)))
                .Aggregate(1, (x, y) => x * y);
        }
    }
}
