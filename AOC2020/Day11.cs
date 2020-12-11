using System;
using System.Linq;

namespace AOC2020
{
    using CheckFunction = Func<char[][], int, int, int, int, bool>;

    [Day(11)]
    class Day11 : Solver
    {
        public class Result
        {
            public char[][] Data;
            public int ChangeCount;
        }

        public override object SolveOne()
        {
            return Solve((data, x, y, dx, dy) => GetChar(data, x + dx, y + dy) == '#', 4);
        }

        public override object SolveTwo()
        {
            return Solve((data, x, y, dx, dy) => CanSee(data, x, y, dx, dy), 5);
        }

        int Solve(CheckFunction check, int claustrophobia)
        {
            var data = Rows.Select(x => x.ToCharArray()).ToArray();
            while (true)
            {
                var result = RunRound(data, check, claustrophobia);
                if (result.ChangeCount == 0)
                {
                    return result.Data.Sum(x => x.Count(y => y == '#'));
                }
                data = result.Data;
            }
        }

        Result RunRound(char[][] data, CheckFunction check, int claustrophobia)
        {
            var result = new Result
            {
                Data = data.Select(x => x.ToArray()).ToArray(),
                ChangeCount = 0
            };
            for (int y = 0; y < data.Count(); y++)
            {
                for (int x = 0; x < data[y].Count(); x++)
                {
                    if (data[y][x] == 'L' && CountCompass(data, x, y, check) == 0)
                    {
                        result.Data[y][x] = '#';
                        result.ChangeCount++;
                    }
                    else if (data[y][x] == '#' && CountCompass(data, x, y, check) >= claustrophobia)
                    {
                        result.Data[y][x] = 'L';
                        result.ChangeCount++;
                    }
                }
            }
            return result;
        }

        char GetChar(char[][] data, int x, int y)
        {
            if (x < 0 || y < 0 || x >= data[0].Length || y >= data.Length)
                return 'Z';
            return data[y][x];
        }

        bool CanSee(char[][] data, int x, int y, int dx, int dy)
        {
            while (true)
            {
                var nextChar = GetChar(data, x + dx, y + dy);
                if (nextChar == '#')
                    return true;
                if (nextChar == 'L' || nextChar == 'Z')
                    return false;
                if (nextChar == '.')
                {
                    x += dx;
                    y += dy;
                }
            }
        }

        int CountCompass(char[][] data, int x, int y, CheckFunction check)
        {
            int count = 0;
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    if (!(dx == 0 && dy == 0) && check(data, x, y, dx, dy))
                        count++;
            return count;
        }
    }
}
