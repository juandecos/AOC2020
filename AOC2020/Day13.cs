using System.Linq;

namespace AOC2020
{
    [Day(13)]
    class Day13 : Solver
    {
        public override object SolveOne()
        {
            var busList = Rows[1].Split(',').Where(x => x != "x").Select(x => int.Parse(x)).ToList();
            var firstBus = busList
                .Select(x => new { TimeUntilNext = x - (IntRows[0] % x), Number = x })
                .OrderBy(x => x.TimeUntilNext)
                .First();
            return firstBus.TimeUntilNext * firstBus.Number;
        }

        public override object SolveTwo()
        {
            var busList = Rows[1]
                .Split(',')
                .Select((x, i) => new { Number = x, Index = i })
                .Where(x => x.Number != "x")
                .Select(x => new { Number = int.Parse(x.Number), Offset = x.Index });
            long lcm = 1, answer = 0;
            foreach (var bus in busList)
            {
                answer = GetNext(answer, lcm, bus.Number, bus.Offset);
                lcm = Lcm(lcm, bus.Number);
            }
            return answer;
        }

        long GetNext(long start, long lcm, long y, int offset)
        {
            for (long i = start; ; i += lcm)
            {
                if (((i + offset) % y) == 0) return i;
            }
        }

        long Gcf(long a, long b)
        {
            while (b != 0)
            {
                (b, a) = (a % b, b);
            }
            return a;
        }

        long Lcm(long a, long b)
        {
            return (a / Gcf(a, b)) * b;
        }
    }
}
