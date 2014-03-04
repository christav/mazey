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

        private static void PrintMaze(TextWriter output, Maze maze, Func<Maze, int, int, bool> isSolutionCell)
        {
            for (int row = 0; row < maze.Rows; ++row)
            {
                for (int col = 0; col < maze.Cols; ++col)
                {
                    output.Write('+');
                    output.Write(maze.CanGo(row, col, Direction.Up) ? 
                        IsSolutionPath(maze, row, col, Direction.Up, isSolutionCell) ? '*' : ' ' 
                        : '-');
                }
                output.WriteLine('+');

                for (int col = 0; col < maze.Cols; ++col)
                {
                    output.Write(maze.CanGo(row, col, Direction.Left) ? 
                        IsSolutionPath(maze, row, col, Direction.Left, isSolutionCell) ? '*' : ' '
                        : '|');
                    output.Write(isSolutionCell(maze, row, col) ? '*' : ' ');
                }
                output.WriteLine(maze.CanGo(row, maze.Cols - 1, Direction.Right) ? ' ' : '|');
            }
            for (int col = 0; col < maze.Cols; ++col)
            {
                output.Write("+-");
            }
            output.WriteLine("+");
        }

        private static bool IsSolutionPath(Maze maze, int row, int col, Direction direction, Func<Maze, int, int, bool> isSolutionCell)
        {
            if (isSolutionCell(maze, row, col))
            {
                return maze.IsInMaze(row, col, direction) && isSolutionCell(maze, Maze.RowOffset(row, direction), Maze.ColOffset(col, direction));
            }
            return false;
        }
    }
}
