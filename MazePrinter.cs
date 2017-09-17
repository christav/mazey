using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazey
{
    public class MazePrinter
    {
        TextWriter output;
        Maze maze;
        Func<Cell, bool> isSolutionCell;

        private MazePrinter(TextWriter output, Maze maze, Func<Cell, bool> isSolutionCell)
        {
            this.output = output;
            this.maze = maze;
            this.isSolutionCell = isSolutionCell;
        }

        public static void Print(TextWriter output, Maze maze, Func<Cell, bool> isSolutionCell)
        {
            var printer = new MazePrinter(output, maze, isSolutionCell);
            printer.Print();
        }

        private void Print()
        {
            for (int row = 0; row < maze.Rows; ++row)
            {
                PrintRowSeparator(row);
                PrintRow(row);
            }
            PrintMazeBottom();
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

        private char CornerChar(Cell cell)
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

        private void PrintRowSeparator(int row)
        {
            foreach (var cell in maze.CellsInRow(row))
            {
                output.Write(CornerChar(cell));
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

        private void PrintRow(int row)
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

        private void PrintMazeBottom()
        {
            foreach (var cell in maze.CellsInRow(maze.Rows - 1))
            {
                PrintBottomSeparator(cell);
            }
            PrintBottomRightChar();
            output.Write("\n");
        }

        private void PrintBottomSeparator(Cell cell)
        {
            int index = 0xa | (cell.CanGo(Direction.Left) ? 0 : 1);
            if (cell.Col == 0)
            {
                index &= 0x7;
            }
            output.Write(CornerChars[index]);
            output.Write("━━━");
        }

        private void PrintBottomRightChar()
        {
            var cell = maze.GetCell(maze.Rows - 1, maze.Cols - 1);
            int index = 0x8 | (cell.CanGo(Direction.Right) ? 0 : 1);
            output.Write(CornerChars[index]);
        }
    }
}
