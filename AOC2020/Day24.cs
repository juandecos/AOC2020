using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    using World = Dictionary<int, HashSet<int>>;

    [Day(24)]
    class Day24 : Solver
    {

        public override object SolveOne() => CountBlacks(GetStartWorld());

        World GetStartWorld()
        {
            var world = new World();
            foreach (var row in Rows)
            {
                int x = 0, y = 0;
                char last = ' ';
                foreach (var letter in row)
                {
                    x += (letter == 'e' && last != 's') ? 1 : (letter == 'w' && last != 'n') ? -1 : 0;
                    y += (last == 'n') ? 1 : (last == 's') ? -1 : 0;
                    last = letter;
                }
                if (IsBlack(world, x, y))
                    world[x].Remove(y);
                else
                    world.Get(x).Add(y);
            }
            return world;
        }

        public override object SolveTwo()
        {
            var world = GetStartWorld();
            for (int i = 0; i < 100; i++)
            {
                var newWorld = new World();
                int minX = world.Keys.Min() - 1;
                int maxX = world.Keys.Max() + 1;
                int minY = world.Min(x => x.Value.Min()) - 1;
                int maxY = world.Max(x => x.Value.Max()) + 1;
                for (int x = minX; x <= maxX; x++)
                    for (int y = minY; y <= maxY; y++)
                    {
                        bool isBlack = IsBlack(world, x, y);
                        int adjBlacks = CountAdjacentBlacks(world, x, y);
                        if ((isBlack && (adjBlacks == 1 || adjBlacks == 2)) || (!isBlack && adjBlacks == 2))
                            newWorld.Get(x).Add(y);
                    }
                world = newWorld;
            }
            return CountBlacks(world);
        }

        int CountBlacks(World world) => world.Sum(x => x.Value.Count);

        bool IsBlack(World world, int x, int y) => world.ContainsKey(x) && world[x].Contains(y);

        int CountAdjacentBlacks(World world, int x, int y) =>
            Enumerable.Range(-1, 3).Sum(dx => Enumerable.Range(-1, 3).Count(dy => dx != -dy && IsBlack(world, x + dx, y + dy)));
    }
}
