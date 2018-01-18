package com.tavaresstudios;

import java.util.ArrayList;
import java.util.Random;

/**
 * Created with IntelliJ IDEA.
 * User: cct_000
 * Date: 6/4/14
 * Time: 10:44 PM
 * To change this template use File | Settings | File Templates.
 */
public class MazeMaker {
    private final Maze maze;
    private final Random rng;

    public MazeMaker(Maze maze) {
        this.maze = maze;
        rng = new Random();
    }

    public Maze makeMaze() {
        int startRow = rng.nextInt(maze.getRows());
        int startCol = rng.nextInt(maze.getCols());

        makeMaze(maze.getCell(startRow, startCol));

        for (int row = 0; row < maze.getRows(); ++row) {
            for (int col = 0; col < maze.getCols(); ++col) {
                maze.getCell(row, col).setMark(0);
            }
        }

        maze.openWall(maze.getCell(rng.nextInt(maze.getRows()), 0), Direction.LEFT);
        maze.openWall(maze.getCell(rng.nextInt(maze.getRows()), maze.getCols() - 1), Direction.RIGHT);

        return maze;
    }

    private void makeMaze(Cell cell) {
        cell.setMark(1);

        ArrayList<Direction> directions = availableDirections(cell);
        while (directions.size() > 0) {
            int index = rng.nextInt(directions.size());
            Direction d = directions.get(index);
            directions.remove(index);

            if (cell.go(d).getMark() == 0) {
                maze.openWall(cell, d);
                makeMaze(cell.go(d));
            }
        }
    }

    private ArrayList<Direction> availableDirections(Cell cell) {
        ArrayList<Direction> directions = new ArrayList<Direction>();

        for (Direction d : Direction.getAll()) {
            if (cell.go(d).isInMaze() && cell.go(d).getMark() == 0) {
                directions.add(d);
            }
        }
        return directions;
    }
}
