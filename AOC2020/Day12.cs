using System;
using System.Collections.Generic;

namespace AOC2020
{
    [Day(12)]
    class Day12 : Solver
    {
        public class Position
        {
            public int x;
            public int y;
            public int facing;

            public static readonly Dictionary<char, int> CompassToAngle = new Dictionary<char, int>()
            {
                ['E'] = 0,
                ['N'] = 90,
                ['W'] = 180,
                ['S'] = 270
            };

            public void Move(int distance) => Move(facing, distance);
            public void Move(char direction, int distance) => Move(CompassToAngle[direction], distance);
            public void Move(int angle, int distance)
            {
                switch (angle)
                {
                    case 0: x += distance; break;
                    case 90: y += distance; break;
                    case 180: x -= distance; break;
                    case 270: y -= distance; break;
                }
            }
            public void Turn(char action, int angle)
            {
                if (action == 'R') angle = 360 - angle;
                facing += angle;
                facing %= 360;
            }
            public void Rotate(char action, int angle)
            {
                if (action == 'R') angle = 360 - angle;
                for (int i = 0; i < angle / 90; i++)
                    (x, y) = (-y, x);
            }
            public int Manhattan() => Math.Abs(x) + Math.Abs(y);

            public static Position operator *(Position a, int b) =>
                new Position() { x = a.x * b, y = a.y * b };
            public static Position operator +(Position a, Position b) =>
                new Position() { x = a.x + b.x, y = a.y + b.y, facing = a.facing };
        }

        public override object SolveOne()
        {
            var ship = new Position() { x = 0, y = 0, facing = 0 };
            foreach (var row in Rows)
            {
                var action = row[0];
                var amount = int.Parse(row.Substring(1));
                switch (action)
                {
                    case 'N': case 'S': case 'E': case 'W': ship.Move(action, amount); break;
                    case 'L': case 'R': ship.Turn(action, amount); break;
                    case 'F': ship.Move(amount); break;
                }
            }
            return ship.Manhattan();
        }

        public override object SolveTwo()
        {
            var ship = new Position() { x = 0, y = 0 };
            var way = new Position() { x = 10, y = 1 };
            foreach (var row in Rows)
            {
                var action = row[0];
                var amount = int.Parse(row.Substring(1));
                switch (action)
                {
                    case 'N': case 'S': case 'E': case 'W': way.Move(action, amount); break;
                    case 'L': case 'R': way.Rotate(action, amount); break;
                    case 'F': ship += way * amount; break;
                }
            }
            return ship.Manhattan();
        }
    }
}
