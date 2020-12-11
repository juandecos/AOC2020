using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public abstract class Solver
    {
        public List<string> Rows { get; set; }
        public List<long> LongRows { get; set; }
        public List<int> IntRows { get; set; }

        public Solver()
        {
            DayAttribute attribute = (DayAttribute)Attribute.GetCustomAttribute(GetType(), typeof(DayAttribute));
            string input = Properties.Resources.ResourceManager.GetString("Input" + attribute.Day.ToString().PadLeft(2, '0'));
            Rows = input.Split("\r\n").ToList();
            LongRows = Rows.ConvertAll(x => long.TryParse(x, out long n) ? n : 0);
            IntRows = Rows.ConvertAll(x => int.TryParse(x, out int n) ? n : 0);
        }

        public abstract object SolveOne();
        public abstract object SolveTwo();

        public void SolveAndPrintOne()
        {
            Console.WriteLine(SolveOne().ToString());
            Console.WriteLine("-------- DONE --------");
        }

        public void SolveAndPrintTwo()
        {
            Console.WriteLine(SolveTwo().ToString());
            Console.WriteLine("-------- DONE --------");
        }

        public IEnumerable<List<string>> GroupRows()
        {
            var group = new List<string>();
            foreach (var row in Rows)
            {
                if (row.Length != 0)
                {
                    group.Add(row);
                    continue;
                }
                yield return group;
                group.Clear();
            }
            yield return group;
        }
    }

    public static class SolverExtensions
    {
        public static string[] Split(this string input, string delimiter)
        {
            return input.Split(new string[] { delimiter }, StringSplitOptions.None);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class DayAttribute : Attribute
    {
        public int Day { get; set; }

        public DayAttribute(int day)
        {
            Day = day;
        }
    }
}
