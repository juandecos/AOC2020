using System;
using System.Linq;

namespace AOC2020
{
    [Day(2)]
    class Day02 : Solver
    {
        public override object SolveOne()
        {
            return Rows.Where(row => IsValid(row, (fullString, searchChar, start, end) =>
            {
                int count = fullString.ToCharArray().Count(x => x == searchChar);
                return count >= start && count <= end;
            })).Count();
        }

        public override object SolveTwo()
        {
            return Rows.Where(row => IsValid(row, (fullString, searchChar, start, end) =>
                fullString[start] == searchChar ^ fullString[end] == searchChar)).Count();
        }

        public static bool IsValid(string input, Func<string, char, int, int, bool> validator)
        {
            var parts = input.Split(' ');
            var rangeParts = parts[0].Split('-');
            int start = int.Parse(rangeParts[0]) - 1;
            int end = int.Parse(rangeParts[1]) - 1;
            char searchChar = parts[1][0];
            string fullString = parts[2];
            return validator(fullString, searchChar, start, end);
        }
    }
}
