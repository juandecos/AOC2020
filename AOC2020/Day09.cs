using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(9)]
    class Day09 : Solver
    {
        const int Preamble = 25;

        public Day09()
        {
        }

        public override object SolveOne()
        {
            for (int i = Preamble; i < LongRows.Count; i++)
            {
                if (!Combinations(LongRows.GetRange(i - Preamble, Preamble)).Contains(LongRows[i]))
                    return LongRows[i];
            }
            return 0;
        }

        HashSet<long> Combinations(List<long> input)
        {
            var output = new HashSet<long>();
            for (int i = 0; i < input.Count; i++)
                for (int j = 0; j < input.Count; j++)
                    if (i != j)
                        output.Add(input[i] + input[j]);
            return output;
        }

        public override object SolveTwo()
        {
            long target = (long)SolveOne();
            for (int i = 0; i < LongRows.Count; i++)
            {
                long sum = 0;
                for (int j = i; j < LongRows.Count; j++)
                {
                    sum += LongRows[j];
                    if (sum == target)
                    {
                        var range = LongRows.GetRange(i, j - i + 1);
                        return range.Max() + range.Min();
                    }
                }
            }
            return 0;
        }
    }
}
