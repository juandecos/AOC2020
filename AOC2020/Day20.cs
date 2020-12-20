using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    using Grid = List<List<char>>;
    using PieceGrid = List<List<Piece>>;

    class Piece
    {
        public int Id;
        public Grid Data;
        public string Top;
        public string Bottom;
        public string Left;
        public string Right;
        public Piece(int id, Grid data)
        {
            Id = id;
            Data = data;
            Top = string.Join("", data.First());
            Bottom = string.Join("", data.Last());
            Left = string.Join("", data.Select(x => x.First()));
            Right = string.Join("", data.Select(x => x.Last()));
        }
    }

    [Day(20)]
    class Day20 : Solver
    {
        readonly List<Piece> Pieces = new List<Piece>();
        readonly int PieceSize;
        readonly int PuzzleSize;
        PieceGrid Solution;

        public Day20()
        {
            var pieces = GroupRows().Select(x => x.Skip(1).Select(y => y.ToCharArray().ToList()).ToList()).ToList();
            var pieceIdList = GroupRows().Select(x => int.Parse(x[0].Substring(5, 4))).ToList();
            PieceSize = pieces[0].Count;
            PuzzleSize = (int)Math.Sqrt(pieces.Count);

            for (int i = 0; i < pieces.Count; i++)
            {
                var pieceCopy = pieces[i].ConvertAll(x => x.ConvertAll(y => y));
                for (int flip = 0; flip < 2; flip++)
                {
                    for (int rotate = 0; rotate < 4; rotate++)
                    {
                        Pieces.Add(new Piece(pieceIdList[i], pieceCopy));
                        pieceCopy = Rotate90(pieceCopy);
                    }
                    pieceCopy = Flip(pieceCopy);
                }
            }

            Pieces.First(x => TrySolve(x));
        }

        bool TrySolve(Piece startPiece)
        {
            var usedPieces = new HashSet<int> { startPiece.Id };
            var attempt = GetBlankGrid<Piece>(PuzzleSize, null);
            attempt[0][0] = startPiece;
            for (int i = 0; i < PuzzleSize; i++)
                for (int j = 0; j < PuzzleSize; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    var correctPiece = Pieces
                        .Where(x => !usedPieces.Contains(x.Id))
                        .FirstOrDefault(x => CanPlace(x, attempt, i, j));
                    if (correctPiece == null)
                        return false;
                    attempt[i][j] = correctPiece;
                    usedPieces.Add(correctPiece.Id);
                }
            Solution = attempt;
            return true;
        }

        public override object SolveOne()
        {
            return (long)Solution[0][0].Id *
                Solution[0][PuzzleSize - 1].Id *
                Solution[PuzzleSize - 1][0].Id *
                Solution[PuzzleSize - 1][PuzzleSize - 1].Id;
        }

        public override object SolveTwo()
        {
            var image = GetImage(Solution);
            var monster = new Grid
            {
                "                  # ".ToCharArray().ToList(),
                "#    ##    ##    ###".ToCharArray().ToList(),
                " #  #  #  #  #  #   ".ToCharArray().ToList()
            };
            image = GetVersionWithMatch(image, monster);
            return CountChar(image, '#') - (CountChar(monster, '#') * CountMatches(image, monster));
        }

        Grid GetVersionWithMatch(Grid full, Grid search)
        {
            var output = full;
            for (int flip = 0; flip < 2; flip++)
            {
                for (int rotate = 0; rotate < 4; rotate++)
                {
                    if (CountMatches(output, search) > 0)
                        return output;
                    output = Rotate90(output);
                }
                output = Flip(output);
            }
            throw new Exception("No match");
        }

        int CountChar(Grid tile, char input) => tile.Sum(x => x.Count(y => y == input));

        int CountMatches(Grid full, Grid search)
        {
            int count = 0;
            for (int i = 0; i < full.Count - search.Count; i++)
                for (int j = 0; j < full[0].Count - search[0].Count; j++)
                    if (Match(full, search, i, j))
                        count++;
            return count;
        }

        bool Match(Grid full, Grid search, int offsetI, int offsetJ)
        {
            for (int i = 0; i < search.Count; i++)
                for (int j = 0; j < search[0].Count; j++)
                    if (search[i][j] != ' ' && full[offsetI + i][offsetJ + j] != '#')
                        return false;
            return true;
        }

        bool CanPlace(Piece pieceData, PieceGrid solution, int i, int j)
        {
            if (i > 0 && solution[i - 1][j] != null && solution[i - 1][j].Bottom != pieceData.Top)
                return false;
            if (i < PuzzleSize - 1 && solution[i + 1][j] != null && solution[i + 1][j].Top != pieceData.Bottom)
                return false;
            if (j > 0 && solution[i][j - 1] != null && solution[i][j - 1].Right != pieceData.Left)
                return false;
            if (j < PuzzleSize - 1 && solution[i][j + 1] != null && solution[i][j + 1].Left != pieceData.Right)
                return false;
            return true;
        }

        Grid GetImage(PieceGrid grid)
        {
            int strippedPieceSize = PieceSize - 2;
            var output = GetBlankGrid(strippedPieceSize * PuzzleSize, ' ');
            for (int i = 0; i < PuzzleSize; i++)
                for (int j = 0; j < PuzzleSize; j++)
                    for (int x = 0; x < strippedPieceSize; x++)
                        for (int y = 0; y < strippedPieceSize; y++)
                            output[(i * strippedPieceSize) + y][(j * strippedPieceSize) + x] =
                                grid[i][j].Data[y + 1][x + 1];
            return output;
        }

        List<List<T>> GetBlankGrid<T>(int size, T fill)
        {
            var output = new List<List<T>>();
            for (int i = 0; i < size; i++)
            {
                output.Add(new List<T>());
                for (int j = 0; j < size; j++)
                    output[i].Add(fill);
            }
            return output;
        }

        Grid Flip(Grid tile)
        {
            Grid output = new Grid();
            foreach (var row in tile)
            {
                var copy = row.ConvertAll(x => x);
                copy.Reverse();
                output.Add(copy);
            }
            return output;
        }

        Grid Rotate90(Grid tile)
        {
            Grid output = GetBlankGrid(tile.Count, ' ');
            for (int i = 0; i < tile.Count; i++)
                for (int j = 0; j < tile.Count; j++)
                    output[tile.Count - 1 - j][i] = tile[i][j];
            return output;
        }
    }
}
