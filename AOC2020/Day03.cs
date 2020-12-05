namespace AOC2020
{
    [Day(3)]
    class Day03 : Solver
    {
        public override object SolveOne()
        {
            return GetTreeCount(3, 1);
        }

        public override object SolveTwo()
        {
            return
                GetTreeCount(1, 1) *
                GetTreeCount(3, 1) *
                GetTreeCount(5, 1) *
                GetTreeCount(7, 1) *
                GetTreeCount(1, 2);
        }

        public long GetTreeCount(int dx, int dy)
        {
            long treeCount = 0;
            for (int y = 0; y < Rows.Count; y += dy)
            {
                var currentRow = Rows[y];
                int x = ((y / dy) * dx) % currentRow.Length;
                if (currentRow[x] == '#')
                {
                    treeCount++;
                }
            }
            return treeCount;
        }
    }
}
