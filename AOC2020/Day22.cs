using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    [Day(22)]
    class Day22 : Solver
    {
        public override object SolveOne()
        {
            var player1 = new Queue<int>(GroupRows().ElementAt(0).Skip(1).Select(y => int.Parse(y)));
            var player2 = new Queue<int>(GroupRows().ElementAt(1).Skip(1).Select(y => int.Parse(y)));
            while (true)
            {
                int card1 = player1.Dequeue();
                int card2 = player2.Dequeue();
                if (card1 > card2)
                {
                    player1.Enqueue(card1); player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2); player2.Enqueue(card1);
                }
                if (player1.Count == 0)
                    return Score(player2);
                if (player2.Count == 0)
                    return Score(player1);
            }
        }

        public override object SolveTwo()
        {
            var player1 = new Queue<int>(GroupRows().ElementAt(0).Skip(1).Select(y => int.Parse(y)));
            var player2 = new Queue<int>(GroupRows().ElementAt(1).Skip(1).Select(y => int.Parse(y)));
            (int player, int score) = PlayRec(player1, player2);
            return score;
        }

        string Serialize(Queue<int> player1, Queue<int> player2) => string.Join(",", player1) + "|" + string.Join(",", player2);
        int Score(IEnumerable<int> input) => input.Select((x, i) => (x, i)).Sum(x => (input.Count() - x.i) * x.x);

        (int player, int score) PlayRec(Queue<int> player1, Queue<int> player2)
        {
            HashSet<string> previousHands = new HashSet<string>();

            while (true)
            {
                string currentState = Serialize(player1, player2);
                if (previousHands.Contains(currentState))
                    return (0, Score(player1));
                previousHands.Add(currentState);

                int card1 = player1.Dequeue();
                int card2 = player2.Dequeue();
                int winner = 0;
                if (player1.Count >= card1 && player2.Count >= card2)
                    (winner, _) = PlayRec(new Queue<int>(player1.Take(card1)), new Queue<int>(player2.Take(card2)));
                else if (card1 < card2)
                    winner = 1;

                if (winner == 0)
                {
                    player1.Enqueue(card1); player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2); player2.Enqueue(card1);
                }

                if (player1.Count == 0)
                    return (1, Score(player2));
                if (player2.Count == 0)
                    return (0, Score(player1));
            }

        }
    }
}
