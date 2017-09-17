using System;
using System.IO;
using System.Linq;

namespace Mazey
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            var maze = new Maze(20, 20);
            var maker = new MazeMaker(maze);
            maker.MakeMaze();
            var solver = new MazeSolver(maze);
            solver.Solve();
            PrintMaze(Console.Out, maze, MazeSolver.IsSolutionCell);
        }

        private static void PrintMaze(TextWriter output, Maze maze, Func<Cell, bool> isSolutionCell)
        {
            for (int row = 0; row < maze.Rows; ++row)
            {
                PrintRowSeparator(output, maze, row);
                PrintRow(output, maze, row, isSolutionCell);
            }
            PrintMazeBottom(output, maze);
        }

        private static void PrintRow(TextWriter output, Maze maze, int row, Func<Cell, bool> isSolutionCell)
        {
            foreach (var cell in maze.CellsInRow(row))
            {
                if (cell.CanGo(Direction.Left))
                {
                    output.Write(" ");
                }
                else
                {
                    output.Write("\u2503");
                }
                 if (isSolutionCell(cell))
                {
                    output.Write("XXX");
                }
                else
                {
                    output.Write("   ");
                }
            }

            if (maze.Exit.Row == row)
            {
                output.Write(" ");
            }
            else
            {
                output.Write("\u2503");
            }
            output.Write("\n");
        }

        private static char[] CornerChars =
        {
            ' ',
            '╹',
            '╺',
            '┗',
            '╻',
            '┃',
            '┏',
            '┣',
            '╸',
            '┛',
            '━',
            '┻',
            '┓',
            '┫',
            '┳',
            '╋'
        };

        private static char CornerChar(Maze maze, Cell cell)
        {
            int index = 0;
            index |= cell.Go(Direction.Up).CanGo(Direction.Left) ? 0 : 1;
            index |= cell.CanGo(Direction.Up) ? 0 : 2;
            index |= cell.CanGo(Direction.Left) ? 0 : 4;
            index |= cell.Go(Direction.Left).CanGo(Direction.Up) ? 0 : 8;

            if (cell.Row == 0)
            {
                index &= 0xe;
            }
            if (cell.Col == 0)
            {
                index &= 0x7;
            }

            return CornerChars[index];
        }

        private static void PrintRowSeparator(TextWriter output, Maze maze, int row)
        {
            foreach (var cell in maze.CellsInRow(row))
            {
                output.Write(CornerChar(maze, cell));
                if (cell.CanGo(Direction.Up))
                {
                    output.Write("   ");
                }
                else
                {
                    output.Write("━━━");
                }
            }
            PrintRowSeparatorEnd(output, maze, row);
            output.Write("\n");
        }

        private static void PrintRowSeparatorEnd(TextWriter output, Maze maze, int row)
        {
            var cell = maze.GetCell(row, maze.Cols - 1);
            int index = 0;
            index |= cell.Go(Direction.Up).CanGo(Direction.Right) ? 0 : 1;
            index |= cell.CanGo(Direction.Right) ? 0 : 4;
            index |= cell.CanGo(Direction.Up) ? 0 : 8;

            if (row == 0)
            {
                index &= 0xe;
            }
            output.Write(CornerChars[index]);
        }

        private static void PrintMazeBottom(TextWriter output, Maze maze)
        {
            foreach (var cell in maze.CellsInRow(maze.Rows - 1))
            {
                PrintBottomSeparator(output, maze, cell);
            }
            PrintBottomRightChar(output, maze);
            output.Write("\n");
        }

        private static void PrintBottomSeparator(TextWriter output, Maze maze, Cell cell)
        {
            int index = 0xa | (cell.CanGo(Direction.Left) ? 0 : 1);
            if (cell.Col == 0)
            {
                index &= 0x7;
            }
            output.Write(CornerChars[index]);
            output.Write("━━━");
        }

        private static void PrintBottomRightChar(TextWriter output, Maze maze)
        {
            var cell = maze.GetCell(maze.Rows - 1, maze.Cols - 1);
            int index = 0x8 | (cell.CanGo(Direction.Right) ? 0 : 1);
            output.Write(CornerChars[index]);
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
