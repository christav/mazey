using System;
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
                    if (r%2 == 1 && c%2 == 1)
                    {
                        cells[r, c] = 0;
                    }
                    else
                    {
                        cells[r, c] = 1;
                    }
                }
            }
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

        public void Mark(int row, int col, int mark)
        {
            cells[RowToIndex(row, Direction.None), ColToIndex(col, Direction.None)] = mark;
        }

        public int GetMark(int row, int col)
        {
            return cells[RowToIndex(row, Direction.None), ColToIndex(col, Direction.None)];
        }

        public bool IsInMaze(int row, int col)
        {
            return (row >= 0 && row < Rows) &&
                   (col >= 0 && col < Cols);
        }

        private int RowToIndex(int row, Direction direction)
        {
            int y = row*2 + 1;

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
            int x = col*2 + 1;

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
    }
}
