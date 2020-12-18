using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020
{
    [Day(18)]
    class Day18 : Solver
    {
        public override object SolveOne()
        {
            return Rows.Sum(x => Calc(x, input =>
                ApplyAll(input, new Regex(@"[0-9]* [\+\*] [0-9]*"))));
        }

        public override object SolveTwo()
        {
            return Rows.Sum(x => Calc(x, input =>
            {
                input = ApplyAll(input, new Regex(@"[0-9]* \+ [0-9]*"));
                input = ApplyAll(input, new Regex(@"[0-9]* \* [0-9]*"));
                return input;
            }));
        }

        string ApplyAll(string input, Regex regex)
        {
            while (true)
            {
                Match m = regex.Match(input);
                if (!m.Success)
                    return input;
                input = regex.Replace(input, CalcPair(m.Value), 1);
            }
        }

        long Calc(string input, Func<string, string> solveFunc)
        {
            Regex regex = new Regex(@"\([^()]*\)");
            while (true)
            {
                Match m = regex.Match(input);
                if (!m.Success)
                    return long.Parse(solveFunc(input));
                input = regex.Replace(input, solveFunc(m.Value.Replace("(", "").Replace(")", "")), 1);
            }
        }
        string CalcPair(string input) => input.Contains("+") ? CalcAdd(input) : CalcMult(input);
        string CalcAdd(string input)
        {
            var parts = input.Split(" + ");
            return (long.Parse(parts[0]) + long.Parse(parts[1])).ToString();
        }
        string CalcMult(string input)
        {
            var parts = input.Split(" * ");
            return (long.Parse(parts[0]) * long.Parse(parts[1])).ToString();
        }
    }
}
