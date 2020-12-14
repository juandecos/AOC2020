using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(14)]
    class Day14 : Solver
    {
        public Dictionary<long, long> Memory = new Dictionary<long, long>();

        public override object SolveOne()
        {
            return Run((mask, address, value) =>
                Memory[address] =
                    CharsToLong(
                        LongToString(value).Select((x, i) => mask[i] == 'X' ? x : mask[i])));
        }

        long Run(Action<string, long, long> writeFunc)
        {
            Memory.Clear();
            string mask = string.Empty;
            foreach (var row in Rows)
            {
                var parts = row.Split(" = ");
                if (parts[0].StartsWith("mask"))
                {
                    mask = parts[1];
                    continue;
                }
                var address = int.Parse(parts[0].Substring(4, parts[0].Length - 5));
                var value = int.Parse(parts[1]);
                writeFunc(mask, address, value);
            }
            return Memory.Values.Sum();
        }

        public override object SolveTwo()
        {
            return Run((mask, address, value) =>
                Write(
                    string.Join("", LongToString(address).Select((x, i) => mask[i] == '0' ? x : mask[i])),
                    value));
        }

        void Write(string address, long value)
        {
            if (!address.Contains("X"))
            {
                Memory[Convert.ToInt64(address, 2)] = value;
                return;
            }
            var pos = address.IndexOf("X");
            Write(address.Substring(0, pos) + "0" + address.Substring(pos + 1), value);
            Write(address.Substring(0, pos) + "1" + address.Substring(pos + 1), value);
        }

        long CharsToLong(IEnumerable<char> input) => Convert.ToInt64(string.Join("", input), 2);
        string LongToString(long input) => Convert.ToString(input, 2).PadLeft(36, '0');
    }
}
