package com.tavaresstudios;

import java.io.PrintStream;
import java.util.ArrayList;
import java.util.List;

public class MazePrinter {
    private PrintStream out;
    private CharSet charSet;
    private SolutionCellPredicate isSolutionCell;
    private String horizontalBar;

    public MazePrinter(SolutionCellPredicate isSolutionCell, CharSet charSet, PrintStream out) {
        this.isSolutionCell = isSolutionCell;
        this.charSet = charSet;
        this.out = out;
        String bar = charSet.getOutlineChar(10);
        this.horizontalBar = bar + bar + bar;
    }

    public void printMaze(Maze maze) {
        for (int row = 0; row < maze.getRows(); row++) {
            printRowSeparator(maze, row);
            printRow(maze, row);
        }
        printMazeBottom(maze);
    }

    private void printRowSeparator(Maze maze, int row) {
        for (int col = 0; col < maze.getCols(); col++) {
            Cell cell = maze.getCell(row, col);
            out.print(cornerChar(cell));
            if (cell.canGo(Direction.UP)) {
                if (isSolutionCell.check(cell) && isSolutionCell.check(cell.go(Direction.UP))) {
                    out.print(charSet.getSolutionChar(5));
                } else {
                    out.print("   ");
                }
            } else {
                out.print(horizontalBar);
            }
        }
        out.println(rowSeparatorEnd(maze, row));
    }

    private String rowSeparatorEnd(Maze maze, int row) {
        Cell lastCell = maze.getCell(row, maze.getCols() - 1);
        Cell upCell = lastCell.go(Direction.UP);
        int mask = 0;
        if (!(upCell.isInMaze() && upCell.isExit())) {
            mask |= 1;
        }

        if (!lastCell.isExit()) {
            mask |= 4;
        }

        if (!lastCell.canGo(Direction.UP)) {
            mask |= 8;
        }

        if (row == 0) {
            mask &= 0xe;
        }

        return charSet.getOutlineChar(mask);
    }

    private String cornerChar(Cell cell) {
        List<Cell> neighbors = new ArrayList<>();
        neighbors.add(cell.go(Direction.UP));
        neighbors.add(cell.go(Direction.LEFT));
        int mask = 0;
        if (!(neighbors.get(0).isInMaze() && neighbors.get(0).canGo(Direction.LEFT))) {
            mask |= 1;
        }
        if (!cell.canGo(Direction.UP)) {
            mask |= 2;
        }
        if (!(cell.isEntrance() || cell.canGo(Direction.LEFT))) {
            mask |= 4;
        }
        if (!(neighbors.get(1).isInMaze() && neighbors.get(1).canGo(Direction.UP))) {
            mask |= 8;
        }

        if (cell.getRow() == 0) {
            mask &= 0xe;
        }

        if (cell.getCol() == 0) {
            mask &= 0x7;
        }

        return charSet.getOutlineChar(mask);
    }

    private void printRow(Maze maze, int row) {
        for (int col = 0; col < maze.getCols(); col++) {
            Cell cell = maze.getCell(row, col);
            if (cell.isEntrance()) {
                if (isSolutionCell.check(cell)) {
                    out.print(charSet.getSolutionChar(10).charAt(1));
                } else {
                    out.print(" ");
                }
            } else if (cell.canGo(Direction.LEFT)) {
                if (isSolutionCell.check(cell) && isSolutionCell.check(cell.go(Direction.LEFT))) {
                    out.print(charSet.getSolutionChar(10).charAt(1));
                } else {
                    out.print(" ");
                }
            } else {
                out.print(charSet.getOutlineChar(5));
            }
            out.print(cellContents(cell));
        }

        Cell lastCell = maze.getCell(row, maze.getCols() - 1);
        if (lastCell.isExit()) {
            if (isSolutionCell.check(lastCell)) {
                out.print(charSet.getSolutionChar(10).charAt(1));
            } else {
                out.print(" ");
            }
        } else {
            out.print(charSet.getOutlineChar(5));
        }
        out.println();
    }

    private String cellContents(Cell cell) {
        if (!isSolutionCell.check(cell)) {
            return "   ";
        }

        int mask = 0;

        if (cell.canGo(Direction.UP) && isSolutionCell.check(cell.go(Direction.UP))) {
            mask |= 1;
        }

        if (cell.isExit() || (cell.canGo(Direction.RIGHT) && isSolutionCell.check(cell.go(Direction.RIGHT)))) {
            mask |= 2;
        }

        if (cell.canGo(Direction.DOWN) && isSolutionCell.check(cell.go(Direction.DOWN))) {
            mask |= 4;
        }

        if (cell.isEntrance() || (cell.canGo(Direction.LEFT) && isSolutionCell.check(cell.go(Direction.LEFT)))) {
            mask |= 8;
        }

        return charSet.getSolutionChar(mask);
    }

    private void printMazeBottom(Maze maze) {
        int lastRow = maze.getRows() - 1;
        for (int col = 0; col < maze.getCols(); col++) {
            Cell cell = maze.getCell(lastRow, col);
            int mask = 0xa;
            if (!cell.canGo(Direction.LEFT)) {
                mask |= 1;
            }
            if (col == 0) {
                mask &= 7;
            }
            out.print(charSet.getOutlineChar(mask));
            out.print(horizontalBar);
        }

        Cell lastCell = maze.getCell(lastRow, maze.getCols() - 1);
        int mask = 0x8;
        if (!lastCell.canGo(Direction.RIGHT)) {
            mask |= 1;
        }
        out.println(charSet.getOutlineChar(mask));
    }
}
