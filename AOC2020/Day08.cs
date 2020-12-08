using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(8)]
    class Day08 : Solver
    {
        readonly List<Instruction> Instructions = new List<Instruction>();

        public Day08()
        {
            Instructions = Rows.Select(x => ParseInstruction(x)).ToList();
        }

        public class Result
        {
            public int Accumulator;
            public bool IsLoop;
        }

        public class Instruction
        {
            public Instruction() { }
            public Instruction(Instruction other)
            {
                Operation = other.Operation;
                Argument = other.Argument;
            }
            public string Operation;
            public int Argument;
        }

        public override object SolveOne()
        {
            return Run(Instructions).Accumulator;
        }

        public override object SolveTwo()
        {
            for (int i = 0; i < Instructions.Count; i++)
            {
                var instructions = Instructions.ConvertAll(x => new Instruction(x));
                switch (instructions[i].Operation)
                {
                    case "nop":
                        instructions[i].Operation = "jmp";
                        break;
                    case "jmp":
                        instructions[i].Operation = "nop";
                        break;
                    default:
                        continue;
                }
                var result = Run(instructions);
                if (!result.IsLoop)
                    return result.Accumulator;
            }
            return "Failed to find a solution";
        }

        public Instruction ParseInstruction(string raw)
        {
            var parts = raw.Split(' ');
            return new Instruction() { Operation = parts[0], Argument = int.Parse(parts[1]) };
        }

        Result Run(List<Instruction> instructions)
        {
            var result = new Result() { Accumulator = 0, IsLoop = false };
            var visited = new HashSet<int>();
            for (int address = 0; address < instructions.Count;)
            {
                if (visited.Contains(address))
                {
                    result.IsLoop = true;
                    return result;
                }
                visited.Add(address);

                var instruction = instructions[address];
                switch (instruction.Operation)
                {
                    case "jmp":
                        address += instruction.Argument;
                        break;
                    case "nop":
                        address++;
                        break;
                    case "acc":
                        result.Accumulator += instruction.Argument;
                        address++;
                        break;
                }
            }
            return result;
        }
    }
}
