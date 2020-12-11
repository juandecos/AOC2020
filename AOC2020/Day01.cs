using System.Linq;

namespace AOC2020
{
    [Day(1)]
    class Day01 : Solver
    {
        public override object SolveOne()
        {
            return IntRows
                .Where(x => IntRows.Contains(2020 - x))
                .Select(x => x * (2020 - x))
                .First();
        }

        public override object SolveTwo()
        {
            return IntRows
                .Where(x => IntRows.Exists(y => IntRows.Contains(2020 - x - y)))
                .Aggregate(1, (x, y) => x * y);
        }
    }
}
