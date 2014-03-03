using System;
using System.Collections.Generic;
using System.Linq;

namespace Mazey
{
    public class MazeMaker
    {
        private readonly Maze maze;
        private readonly HashSet<Tuple<int, int>> border;
        private readonly HashSet<Tuple<int, int>> outside;
        private readonly Random rng;

        public MazeMaker(Maze maze)
        {
            this.maze = maze;
            rng = new Random();

            border = new HashSet<Tuple<int, int>>();
            outside = new HashSet<Tuple<int, int>>();

            for (int row = 0; row < maze.Rows; ++row)
            {
                for (int col = 0; col < maze.Cols; ++col)
                {
                    outside.Add(Tuple.Create(row, col));
                }
            }
        }

        public IEnumerable<Maze> MakeMaze()
        {
            Tuple<int, int> cell = Tuple.Create(rng.Next(maze.Rows), rng.Next(maze.Cols));

            // Initial conditions - add one cell to maze, set up initial border
            outside.Remove(cell);
            UpdateBorder(cell);

            // And add all cells to the maze
            while (border.Count != 0)
            {
                cell = PickBorderCell();
                ConnectToMaze(cell);
                UpdateBorder(cell);
                yield return maze;
            }

            // Knock out entrance and exit
            maze.OpenWall(rng.Next(maze.Rows), 0, Direction.Left);
            maze.OpenWall(rng.Next(maze.Rows), maze.Cols - 1, Direction.Right);
            yield return maze;
        }

        private Tuple<int, int> PickBorderCell()
        {
            return border.Skip(rng.Next(border.Count)).First();
        }

        private void UpdateBorder(Tuple<int, int> cell)
        {
            foreach (var neighbor in NeighborsOf(cell))
            {
                if (outside.Contains(neighbor))
                {
                    outside.Remove(neighbor);
                    border.Add(neighbor);
                }
            }
        }

        private void ConnectToMaze(Tuple<int, int> cell)
        {
            border.Remove(cell);
            var mazeNeighbors = NeighborsOf(cell).Where(n => !outside.Contains(n) && !border.Contains(n)).ToList();
            var cellToConnectTo = mazeNeighbors[rng.Next(mazeNeighbors.Count)];
            Direction direction = Direction.Right;
            if (cellToConnectTo.Item1 < cell.Item1)
            {
                direction = Direction.Up;
            }
            else if (cellToConnectTo.Item2 < cell.Item2)
            {
                direction = Direction.Left;
            }
            else if (cellToConnectTo.Item1 > cell.Item1)
            {
                direction = Direction.Down;
            }
            maze.OpenWall(cell.Item1, cell.Item2, direction);
        }

        private IEnumerable<Tuple<int, int>> NeighborsOf(Tuple<int, int> cell)
        {
            if (cell.Item1 > 0)
            {
                yield return Tuple.Create(cell.Item1 - 1, cell.Item2);
            }
            if (cell.Item2 > 0)
            {
                yield return Tuple.Create(cell.Item1, cell.Item2 - 1);
            }

            if (cell.Item1 < maze.Rows - 1)
            {
                yield return Tuple.Create(cell.Item1 + 1, cell.Item2);
            }

            if (cell.Item2 < maze.Cols - 1)
            {
                yield return Tuple.Create(cell.Item1, cell.Item2 + 1);
            }
        } 
    }
}
