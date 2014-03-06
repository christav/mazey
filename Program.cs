using System;
using System.IO;
using System.Linq;

namespace Mazey
{
    class Program
    {
        static void Main(string[] args)
        {
            var maze = new Maze(20, 20);
            var maker = new MazeMaker(maze);
            maker.MakeMaze();
            var solver = new MazeSolver(maze);
            solver.Solve();
            PrintMaze(Console.Out, maze, MazeSolver.IsSolutionCell);
        }

        private static void PrintMaze(TextWriter output, Maze maze, Func<Cell, bool> isSolutionCell)
        {
            bool firstRow = true;
            foreach (var row in maze.AllRows())
            {
                var currentRow = row.ToList();
                var firstCol = true;
                foreach (var cell in currentRow)
                {
                    if (firstRow)
                    {
                        output.Write(firstCol ? "\u250c" : "\u252c");
                    }
                    else
                    {
                        output.Write(firstCol ? "\u251c" : "\u253c");
                    }
                    output.Write(cell.CanGo(Direction.Up) ?
                        IsSolutionPath(cell, Direction.Up, isSolutionCell) ? "XXX" : "   "
                        : "\u2500\u2500\u2500");
                    firstCol = false;
                }
                output.WriteLine(firstRow ? "\u2510" :"\u2524");

                firstCol = true;
                foreach (var cell in currentRow)
                {
                    output.Write(cell.CanGo(Direction.Left) ?
                        IsSolutionPath(cell, Direction.Left, isSolutionCell) ? "X" : " "
                        : firstCol ? "\u2502" : "\u2502");
                    output.Write(isSolutionCell(cell) ? "XXX" : "   ");
                    firstCol = false;
                }
                output.WriteLine(currentRow[currentRow.Count - 1].CanGo(Direction.Right) ? " " : "\u2502");
                firstRow = false;
            }

            output.Write("\u2514\u2500\u2500\u2500");

            for (int col = 1; col < maze.Cols; ++col)
            {
                output.Write("\u2534\u2500\u2500\u2500");
            }
            output.WriteLine("\u2518");
        }

        private static bool IsSolutionPath(Cell cell, Direction direction, Func<Cell, bool> isSolutionCell)
        {
            if (isSolutionCell(cell))
            {
                return cell.Go(direction).IsInMaze && isSolutionCell(cell.Go(direction));
            }
            return false;
        }
    }
}
