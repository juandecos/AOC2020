using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(23)]
    class Day23 : Solver
    {
        class Node
        {
            public int Value;
            public Node Next;
        }

        public override object SolveOne()
        {
            var input = Rows[0].ToCharArray().Select(x => x - '0').ToList();
            var map = Run(input, 9, 100);
            string output = "";
            for (Node node = map[1].Next; node != map[1]; node = node.Next)
                output += node.Value.ToString();
            return output;
        }

        public override object SolveTwo()
        {
            var input = Rows[0].ToCharArray().Select(x => x - '0').ToList();
            var map = Run(input, 1000000, 10000000);
            return (long)map[1].Next.Value * map[1].Next.Next.Value;
        }

        private static Dictionary<int, Node> Run(List<int> input, int max, int rounds)
        {
            for (int i = input.Count + 1; i <= max; i++)
                input.Add(i);

            var map = new Dictionary<int, Node>();
            var current = new Node() { Value = input[0] };
            map[input[0]] = current;
            foreach (var item in input.Skip(1))
            {
                current.Next = new Node() { Value = item };
                current = current.Next;
                map[current.Value] = current;
            }
            current.Next = map[input[0]];
            current = current.Next;

            for (int i = 0; i < rounds; i++)
            {
                var pickup0 = current.Next;
                var pickup1 = pickup0.Next;
                var pickup2 = pickup1.Next;
                current.Next = pickup2.Next;

                int dest = current.Value - 1;
                while (true)
                {
                    if (dest == 0) dest = max;
                    if (pickup0.Value != dest && pickup1.Value != dest && pickup2.Value != dest)
                        break;
                    dest--;
                }
                var destNode = map[dest];

                pickup2.Next = destNode.Next;
                destNode.Next = pickup0;
                current = current.Next;
            }

            return map;
        }
    }
}
