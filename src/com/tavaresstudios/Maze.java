package com.tavaresstudios;

import java.util.ArrayList;
import java.util.Iterator;

/**
 * Created with IntelliJ IDEA.
 * User: cct_000
 * Date: 6/4/14
 * Time: 9:18 PM
 * To change this template use File | Settings | File Templates.
 */
public class Maze {
    // Our set of cells - each cell in this array is representing
    // either a wall or a cell. For the walls, 1 = a wall, 0 = no wall,
    // for the cells the value in the array is the value of the mark for
    // that cell.
    private int[][] cells;

    private final int rows;
    private final int cols;

    public Maze(int rows, int cols) {
        cells = new int[rows * 2 + 1][cols * 2 + 1];
        for(int r = 0; r < rows * 2 + 1; ++r) {
            for(int c = 0; c < cols * 2 + 1; ++c) {
                cells[r][c] = 1;
            }
        }

        for (int r = 0; r < rows; ++r) {
            for (int c = 0; c < cols; ++c) {
                getCell(r,c).setMark(0);
            }
        }

        this.rows = rows;
        this.cols = cols;
    }

    public int getRows() {
        return rows;
    }

    public int getCols() {
        return cols;
    }

    public Cell entrance() {
        for(int row = 0; row < rows; ++row) {
            if (canGo(row, 0, Direction.LEFT)) {
                return getCell(row, 0);
            }
        }
        throw new RuntimeException("No Entrance!");
    }

    public Cell exit() {
        for (int row = 0; row < rows; ++row) {
            if (canGo(row, cols - 1, Direction.RIGHT)) {
                return getCell(row, cols - 1);
            }
        }
        throw new RuntimeException("No Exit!");
    }

    public Cell getCell(int row, int col) {
        return new MazeCell(row, col);
    }

    private Boolean canGo(int row, int col, Direction d) {
        int y = rowToIndex(row, d);
        int x = colToIndex(col, d);

        return isInMaze(row, col) && cells[y][x] == 0;
    }

    private Boolean isInMaze(int row, int col) {
        return isInMaze(row, col, Direction.NONE);
    }

    private Boolean isInMaze(int row, int col, Direction d) {
        row = rowOffset(row, d);
        col = colOffset(col, d);

        return (row >= 0 && row < rows) &&
                (col >= 0 && col < cols);
    }

    private void setMark(int row, int col, int mark) {
        setMark(row, col, mark, Direction.NONE);
    }

    private void setMark(int row, int col, int mark, Direction d)
    {
        cells[rowToIndex(rowOffset(row, d), Direction.NONE)][colToIndex(colOffset(col, d), Direction.NONE)] = mark;
    }

    private int getMark(int row, int col) {
        return getMark(row, col, Direction.NONE);
    }

    private int getMark(int row, int col, Direction d)
    {
        return cells[rowToIndex(rowOffset(row, d), Direction.NONE)][colToIndex(colOffset(col, d), Direction.NONE)];
    }

    public void openWall(Cell cell, Direction d) {
        int y = rowToIndex(cell.getRow(), d);
        int x = colToIndex(cell.getCol(), d);
        cells[y][x] = 0;
    }

    public void closeWall(Cell cell, Direction d) {
        cells[rowToIndex(cell.getRow(), d)][colToIndex(cell.getCol(), d)] = 1;
    }

    //
    // Implementation helpers for navigation
    //
    private int rowToIndex(int row, Direction d) {
        int y = row * 2 + 1;
        switch(d) {
            case NONE:
                return y;
            case UP:
                return y - 1;
            case LEFT:
                return y;
            case DOWN:
                return y + 1;
            case RIGHT:
                return y;
        }
        throw new RuntimeException("Unknown direction");
    }

    private int colToIndex(int col, Direction d) {
        int x = col * 2 + 1;
        switch(d) {
            case NONE:
                return x;
            case UP:
                return x;
            case LEFT:
                return x - 1;
            case DOWN:
                return x;
            case RIGHT:
                return x + 1;
        }
        throw new RuntimeException("Unknown direction");
    }

    private static int rowOffset(int row, Direction direction)
    {
        switch (direction)
        {
            case NONE:
            case LEFT:
            case RIGHT:
                return row;

            case UP:
                return row - 1;

            case DOWN:
                return row + 1;

            default:
                throw new RuntimeException("Unknown direction");
        }
    }

    private static int colOffset(int col, Direction direction)
    {
        switch (direction)
        {
            case NONE:
            case UP:
            case DOWN:
                return col;

            case LEFT:
                return col - 1;

            case RIGHT:
                return col + 1;

            default:
                throw new RuntimeException("Unknown direction");
        }
    }

    //
    // Implementation of the MazeCell class - this is a flyweight that
    // provides a view onto the actual storage for that cell in the maze.
    //
    private class MazeCell implements Cell {
        private final int row;
        private final int col;

        private MazeCell(int row, int col) {
            this.row = row;
            this.col = col;
        }

        @Override
        public int getRow() {
            return row;
        }

        @Override
        public int getCol() {
            return col;
        }

        @Override
        public Boolean canGo(Direction d) {
            return Maze.this.canGo(row, col, d);
        }

        @Override
        public Cell go(Direction d) {
            return new MazeCell(Maze.rowOffset(row, d), Maze.colOffset(col, d));
        }

        @Override
        public Boolean isInMaze() {
            return Maze.this.isInMaze(row, col);
        }

        @Override
        public int getMark() {
            return Maze.this.getMark(row, col);
        }

        @Override
        public void setMark(int mark) {
            Maze.this.setMark(row, col, mark);
        }

        @Override
        public Boolean isEntrance() {
            Cell entrance = Maze.this.entrance();
            return row == entrance.getRow() && col == entrance.getCol();
        }

        @Override
        public Boolean isExit() {
            Cell exit = Maze.this.exit();
            return row == exit.getRow() && col == exit.getCol();
        }

        @Override
        public Iterable<Cell> neighbors() {
            ArrayList<Cell> neighbors = new ArrayList<Cell>();
            for(Direction d: Direction.getAll()) {
                if (canGo(d)) {
                    neighbors.add(go(d));
                }
            }
            return neighbors;
        }
    }
}
