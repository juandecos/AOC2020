using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(17)]
    class Day17 : Solver
    {
        class World3D : Dictionary<int, Dictionary<int, Dictionary<int, bool>>>
        {
            public bool GetValue(int x, int y, int z) =>
                ContainsKey(x) &&
                this[x].ContainsKey(y) &&
                this[x][y].ContainsKey(z) &&
                this[x][y][z];
            public void SetValue(int x, int y, int z, bool value)
            {
                if (value != GetValue(x, y, z))
                    this.Get(x).Get(y)[z] = value;
            }
            public int CountNeighbors(int x, int y, int z)
            {
                int count = 0;
                for (int dx = -1; dx <= 1; dx++)
                    for (int dy = -1; dy <= 1; dy++)
                        for (int dz = -1; dz <= 1; dz++)
                            if (dx != 0 || dy != 0 || dz != 0)
                                if (GetValue(x + dx, y + dy, z + dz))
                                    count++;
                return count;
            }
            public World3D Run()
            {
                int loX = Keys.Min();
                int hiX = Keys.Max();
                int loY = Values.Min(x => x.Keys.Min());
                int hiY = Values.Max(x => x.Keys.Max());
                int loZ = Values.Min(x => x.Values.Min(y => y.Keys.Min()));
                int hiZ = Values.Max(x => x.Values.Max(y => y.Keys.Max()));
                var output = new World3D();
                for (int x = loX - 1; x <= hiX + 1; x++)
                    for (int y = loY - 1; y <= hiY + 1; y++)
                        for (int z = loZ - 1; z <= hiZ + 1; z++)
                        {
                            int nCount = CountNeighbors(x, y, z);
                            output.SetValue(x, y, z, nCount == 3 || (GetValue(x, y, z) && nCount == 2));
                        }
                return output;
            }
        }

        class World4D : Dictionary<int, World3D>
        {
            public bool GetValue(int w, int x, int y, int z) => ContainsKey(w) && this[w].GetValue(x, y, z);
            public void SetValue(int w, int x, int y, int z, bool value)
            {
                if (value != GetValue(w, x, y, z))
                    this.Get(w).SetValue(x, y, z, value);
            }
            public int CountNeighbors(int w, int x, int y, int z)
            {
                int count = 0;
                for (int dw = -1; dw <= 1; dw++)
                    for (int dx = -1; dx <= 1; dx++)
                        for (int dy = -1; dy <= 1; dy++)
                            for (int dz = -1; dz <= 1; dz++)
                                if (dw != 0 || dx != 0 || dy != 0 || dz != 0)
                                    if (GetValue(w + dw, x + dx, y + dy, z + dz))
                                        count++;
                return count;
            }
            public World4D Run()
            {
                int loW = Keys.Min();
                int hiW = Keys.Max();
                int loX = Values.Min(w => w.Keys.Min());
                int hiX = Values.Max(w => w.Keys.Max());
                int loY = Values.Min(w => w.Values.Min(x => x.Keys.Min()));
                int hiY = Values.Max(w => w.Values.Max(x => x.Keys.Max()));
                int loZ = Values.Min(w => w.Values.Min(x => x.Values.Min(y => y.Keys.Min())));
                int hiZ = Values.Max(w => w.Values.Max(x => x.Values.Max(y => y.Keys.Max())));

                var output = new World4D();
                for (int w = loW - 1; w <= hiW + 1; w++)
                    for (int x = loX - 1; x <= hiX + 1; x++)
                        for (int y = loY - 1; y <= hiY + 1; y++)
                            for (int z = loZ - 1; z <= hiZ + 1; z++)
                            {
                                int nCount = CountNeighbors(w, x, y, z);
                                output.SetValue(w, x, y, z, nCount == 3 || (GetValue(w, x, y, z) && nCount == 2));
                            }
                return output;
            }
        }

        public override object SolveOne()
        {
            var world = new World3D();
            for (int x = 0; x < Rows.Count; x++)
                for (int y = 0; y < Rows[x].Length; y++)
                    if (Rows[x][y] == '#')
                        world.SetValue(x, y, 0, true);

            for (int i = 0; i < 6; i++)
                world = world.Run();

            return world.Sum(x => x.Value.Sum(y => y.Value.Count(z => z.Value)));
        }

        public override object SolveTwo()
        {
            var world = new World4D();
            for (int x = 0; x < Rows.Count; x++)
                for (int y = 0; y < Rows[x].Length; y++)
                    if (Rows[x][y] == '#')
                        world.SetValue(0, x, y, 0, true);

            for (int i = 0; i < 6; i++)
                world = world.Run();

            return world.Sum(w => w.Value.Sum(x => x.Value.Sum(y => y.Value.Count(z => z.Value))));
        }
    }
}
