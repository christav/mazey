package com.tavaresstudios;

import java.io.PrintStream;

public class Mazey {
    public static void main (String args[]) {
        Mazey app = new Mazey();
        app.run();
    }

    private Maze maze;
    private SolutionCellPredicate isSolutionCell;

    public Mazey() {
        this.maze = new Maze(20, 20);
    }

    public void run() {
        MazeMaker maker = new MazeMaker(maze);
        maker.makeMaze();

        MazeSolver solver = new MazeSolver(maze);
        Maze solved = solver.solve();
        this.isSolutionCell = solver.isSolutionPathPredicate();

        printMaze(System.out, solved);
    }

    private void printMaze(PrintStream out, Maze maze) {
        for (int row = 0; row < maze.getRows(); ++row) {
            for (int col = 0; col < maze.getCols(); ++col) {
                Cell cell = maze.getCell(row, col);

                out.print("+");
                out.print(cell.canGo(Direction.UP) ?
                    isSolutionPath(cell, Direction.UP) ? "XXX" : "   "
                    : "---");
            }
            out.println("+");

            for (int col = 0; col < maze.getCols(); ++col) {
                Cell cell = maze.getCell(row, col);
                out.print(cell.canGo(Direction.LEFT) ?
                    isSolutionPath(cell, Direction.LEFT) ? "X" : " "
                    : "|");
               out.print(isSolutionCell.check(cell) ? "XXX" : "   ");
            }
            out.println(maze.getCell(row, maze.getCols() - 1).canGo(Direction.RIGHT) ? " " : "|");
        }

        for (int col = 0; col < maze.getCols(); ++col) {
            out.print("+---");
        }
        out.println("+");
    }

    private Boolean isSolutionPath(Cell cell, Direction d) {
        if (isSolutionCell.check(cell)) {
            return cell.go(d).isInMaze() && isSolutionCell.check(cell.go(d));
        }
        return false;
    }
}
