import sys
import random
from maze_maker import make_maze
from printer import print_maze
from solver import solve

def run(rows, cols):
    m = make_maze(rows, cols)
    solve(m)
    print_maze(sys.stdout, m)

if __name__ == "__main__":
    rows = int(sys.argv[1]) if len(sys.argv) >= 2 else 10
    cols = int(sys.argv[2]) if len(sys.argv) >= 3 else 20
    run(rows, cols)

