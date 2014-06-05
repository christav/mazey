package com.tavaresstudios;

import java.util.ArrayList;
import java.util.Iterator;

/**
 * Represents the directions you can move in the maze.
 */
public enum Direction {
    NONE,
    UP,
    LEFT,
    DOWN,
    RIGHT;

    public static Iterable<Direction> getAll() {
        ArrayList<Direction> directions = new ArrayList<Direction>();
        directions.add(UP);
        directions.add(LEFT);
        directions.add(DOWN);
        directions.add(RIGHT);
        return directions;
    }
}
