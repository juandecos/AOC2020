using System.Linq;

namespace AOC2020
{
    [Day(15)]
    class Day15 : Solver
    {
        public override object SolveOne()
        {
            return Solve(2020);
        }

        public override object SolveTwo()
        {
            return Solve(30000000);
        }

        int Solve(int count)
        {
            var data = Rows[0].Split(',').Select(x => int.Parse(x)).ToList();
            var map = data.Select((x, i) => new { x, i }).ToDictionary(x => x.x, x => (Last: x.i, Prior: x.i));
            int last = data.Last();
            for (int i = data.Count; i < count; i++)
            {
                last = map.ContainsKey(last) ? map[last].Last - map[last].Prior : 0;
                map[last] = (i, map.ContainsKey(last) ? map[last].Last : i);
            }
            return last;
        }
    }
}
