using System;
using System.IO;
using System.Linq;

namespace Mazey
{
    class Program
    {
        static void Main(string[] args)
        {
            var maze = new Maze(20, 35);
            var maker = new MazeMaker(maze);
            maker.MakeMaze();
            var solver = new MazeSolver(maze);
            solver.Solve();
            PrintMaze(Console.Out, maze, MazeSolver.IsSolutionCell);
        }

        private static void PrintMaze(TextWriter output, Maze maze, Func<Cell, bool> isSolutionCell)
        {
            foreach (var row in maze.AllRows())
            {
                var currentRow = row.ToList();
                foreach (var cell in currentRow)
                {
                    output.Write('+');
                    output.Write(cell.CanGo(Direction.Up) ?
                        IsSolutionPath(cell, Direction.Up, isSolutionCell) ? '*' : ' '
                        : '-');                    
                }
                output.WriteLine('+');

                foreach (var cell in currentRow)
                {
                    output.Write(cell.CanGo(Direction.Left) ?
                        IsSolutionPath(cell, Direction.Left, isSolutionCell) ? '*' : ' '
                        : '|');
                    output.Write(isSolutionCell(cell) ? '*' : ' ');
                }
                output.WriteLine(currentRow[currentRow.Count - 1].CanGo(Direction.Right) ? ' ' : '|');
            }
            
            for (int col = 0; col < maze.Cols; ++col)
            {
                output.Write("+-");
            }
            output.WriteLine("+");
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
