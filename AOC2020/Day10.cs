using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(10)]
    class Day10 : Solver
    {
        public int Max = 0;

        public Day10()
        {
            Max = IntRows.Max();
        }

        public override object SolveOne()
        {
            int[] hops = new int[] { 0, 0, 0 };
            int joltage = 0;
            while (true)
            {
                if (joltage == Max)
                {
                    return hops[0] * (hops[2] + 1);
                }
                int next = IntRows.Where(x => x > joltage).Min();
                hops[next - joltage - 1]++;
                joltage = next;
            }
        }

        public Dictionary<int, long> Memo = new Dictionary<int, long>();

        public override object SolveTwo()
        {
            return CountPaths(0);
        }

        long CountPaths(int current)
        {
            if (current == Max)
            {
                return 1;
            }
            return IntRows.Where(x => x > current && x <= current + 3).Sum(x =>
            {
                if (!Memo.ContainsKey(x))
                    Memo[x] = CountPaths(x);
                return Memo[x];
            });
        }
    }
}
