using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC2020
{
    public abstract class Solver
    {
        public List<string> Rows { get; set; }

        public Solver()
        {
            DayAttribute attribute = (DayAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(DayAttribute));
            string input = Properties.Resources.ResourceManager.GetString("Input" + attribute.Day.ToString().PadLeft(2, '0'));
            Rows = input.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
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
    }

    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class DayAttribute : System.Attribute
    {
        public int Day { get; set; }

        public DayAttribute(int day)
        {
            Day = day;
        }
    }
}
