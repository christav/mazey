﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazey
{
    public abstract class Cell
    {
        public abstract int Row { get; }
        public abstract int Col { get; }
        public abstract bool CanGo(Direction d);
        public abstract Cell Go(Direction d);
        public abstract bool IsInMaze { get; }

        public abstract int Mark { get; set; }

        public IEnumerable<Cell> Neighbors
        {
            get
            {
                foreach (Direction d in Maze.Directions())
                {
                    if (CanGo(d))
                    {
                        yield return Go(d);
                    }
                }
            }
        }
    }
}
