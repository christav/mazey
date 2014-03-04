using System;
using System.Collections.Generic;
using System.Linq;

namespace Mazey
{
    public class Maze
    {
        private int[,] cells;

        public int Cols { get; private set; }
        public int Rows { get; private set; }

        public Maze(int rows, int cols)
        {
            Cols = cols;
            Rows = rows;
            cells = new int[rows * 2 + 1, cols * 2 + 1];
            for (int r = 0; r < rows * 2 + 1; ++r)
            {
                for (int c = 0; c < cols * 2 + 1; ++c)
                {
                    cells[r, c] = 1;
                }
            }
            this.AllCells((r, c) => Mark(r, c, 0));
        }

        public Tuple<int, int> Entrance
        {
            get
            {
                return Tuple.Create(Enumerable.Range(0, Rows).First(r => CanGo(r, 0, Direction.Left)), 0);
            }
        }

        public int EntranceRow
        {
            get { return Entrance.Item1; }
        }

        public int EntranceCol
        {
            get { return Entrance.Item2; }
        }
        public Tuple<int, int> Exit
        {
            get
            {
                return Tuple.Create(Enumerable.Range(0, Rows).First(r => CanGo(r, Cols - 1, Direction.Right)), Cols - 1);
            }
        }

        public int ExitRow
        {
            get { return Exit.Item1; }
        }

        public int ExitCol
        {
            get { return Exit.Item2; }
        }

        public bool CanGo(int row, int col, Direction direction)
        {
            int y = RowToIndex(row, direction);
            int x = ColToIndex(col, direction);

            return IsInMaze(row, col) &&
                   cells[y, x] == 0;
        }

        public void OpenWall(int row, int col, Direction direction)
        {
            int y = RowToIndex(row, direction);
            int x = ColToIndex(col, direction);
            cells[y, x] = 0;
        }

        public void CloseWall(int row, int col, Direction direction)
        {
            cells[RowToIndex(row, direction), ColToIndex(col, direction)] = 1;
        }

        public void Mark(int row, int col, int mark, Direction direction = Direction.None)
        {
            cells[RowToIndex(RowOffset(row, direction), Direction.None), ColToIndex(ColOffset(col, direction), Direction.None)] = mark;
        }

        public int GetMark(int row, int col, Direction direction = Direction.None)
        {
            return cells[RowToIndex(RowOffset(row, direction), Direction.None), ColToIndex(ColOffset(col, direction), Direction.None)];
        }

        public bool IsInMaze(int row, int col, Direction direction = Direction.None)
        {
            row = RowOffset(row, direction);
            col = ColOffset(col, direction);

            return (row >= 0 && row < Rows) &&
                   (col >= 0 && col < Cols);
        }

        public IEnumerable<Direction> Directions()
        {
            yield return Direction.Up;
            yield return Direction.Left;
            yield return Direction.Down;
            yield return Direction.Right;
        }

        public void AllCells(Action<int, int> perCellAction)
        {
            for (int r = 0; r < Rows; ++r)
            {
                for(int c = 0; c < Cols; ++c)
                {
                    perCellAction(r, c);
                }
            }
        }

        private int RowToIndex(int row, Direction direction)
        {
            int y = row * 2 + 1;

            switch (direction)
            {
                case Direction.None:
                    return y;
                case Direction.Up:
                    return y - 1;
                case Direction.Left:
                    return y;
                case Direction.Down:
                    return y + 1;
                case Direction.Right:
                    return y;
            }
            throw new Exception("Unknown direction");
        }

        private int ColToIndex(int col, Direction direction)
        {
            int x = col * 2 + 1;

            switch (direction)
            {
                case Direction.None:
                    return x;
                case Direction.Up:
                    return x;
                case Direction.Left:
                    return x - 1;
                case Direction.Down:
                    return x;
                case Direction.Right:
                    return x + 1;
            }

            throw new Exception("Unknown direction");
        }

        public static int RowOffset(int row, Direction direction)
        {
            switch (direction)
            {
                case Direction.None:
                case Direction.Left:
                case Direction.Right:
                    return row;

                case Direction.Up:
                    return row - 1;

                case Direction.Down:
                    return row + 1;

                default:
                    throw new Exception("Unknown direction");
            }
        }
        public static int ColOffset(int col, Direction direction)
        {
            switch (direction)
            {
                case Direction.None:
                case Direction.Up:
                case Direction.Down:
                    return col;

                case Direction.Left:
                    return col - 1;

                case Direction.Right:
                    return col + 1;

                default:
                    throw new Exception("Unknown direction");
            }
        }
    }
}
