using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(4)]
    class Day04 : Solver
    {
        public override object SolveOne()
        {
            return ParseData().Count(x => IsValidOne(x));
        }

        public override object SolveTwo()
        {
            return ParseData().Count(x => IsValidTwo(x));
        }

        List<List<string>> ParseData()
        {
            var data = new List<List<string>>
            {
                new List<string>()
            };
            foreach (var row in Rows)
            {
                if (row.Length == 0)
                {
                    data.Add(new List<string>());
                    continue;
                }
                data.Last().AddRange(row.Split(' '));
            }
            return data;
        }

        bool IsValidOne(List<string> personData)
        {
            return personData.Count(x =>
                x.StartsWith("byr:") ||
                x.StartsWith("iyr:") ||
                x.StartsWith("eyr:") ||
                x.StartsWith("hgt:") ||
                x.StartsWith("hcl:") ||
                x.StartsWith("ecl:") ||
                x.StartsWith("pid:")) == 7;
        }

        bool IsValidTwo(List<string> personData)
        {
            var validEyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            return personData.Count(x =>
            {
                string value = x.Split(':')[1];
                int.TryParse(value, out int intValue);
                switch (x.Substring(0, 4))
                {
                    case "byr:":
                        return intValue >= 1920 && intValue <= 2002;
                    case "iyr:":
                        return intValue >= 2010 && intValue <= 2020;
                    case "eyr:":
                        return intValue >= 2020 && intValue <= 2030;
                    case "hgt:":
                        if (value.EndsWith("in"))
                        {
                            int hgt = int.Parse(value.Substring(0, value.Length - 2));
                            return hgt >= 59 && hgt <= 76;
                        }
                        if (value.EndsWith("cm"))
                        {
                            int hgt = int.Parse(value.Substring(0, value.Length - 2));
                            return hgt >= 150 && hgt <= 193;
                        }
                        return false;
                    case "hcl:":
                        return
                            value.Length == 7 &&
                            value.StartsWith("#") &&
                            value.Substring(1).Count(y => "0123456789abcdef".Contains(y)) == 6;
                    case "ecl:":
                        return validEyeColors.Contains(value);
                    case "pid:":
                        return value.Length == 9 && value.Count(y => "0123456789".Contains(y)) == 9;
                    default:
                        return false;
                }
            }) == 7;
        }
    }
}
