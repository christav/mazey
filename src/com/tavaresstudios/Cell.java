package com.tavaresstudios;

import java.util.Collection;

/**
 * Representation of a single cell in the maze.
 */
public interface Cell {
    /**
     * Return the row this cell is in.
     *
     * @return row number.
     */
    int getRow();

    /**
     * Return the column this cell is in.
     *
     * @return column number.
     */
    int getCol();

    /**
     * Tests if you can legally move in the maze from the current
     * position in the direction given.
     *
     * @param d Direction to move in.
     * @return frue if move is legal, false if it isn't.
     */
    Boolean canGo(Direction d);

    /**
     * Return the cell in the maze in the given direction.
     *
     * @param d Direction to move in.
     * @return The next cell.
     */
    Cell go(Direction d);

    /**
     * Tests if this cell is within the bounds of the maze.
     *
     * @return true if in the maze, false if outside it.
     */
    Boolean isInMaze();

    /**
     * Get the marked value for this cell.
     *
     * @return The mark.
     */
    int getMark();

    /**
     * Set the marked value for this cell.
     *
     * @param mark mark to set.
     */
    void setMark(int mark);

    /**
     * Is this cell the entrance to the maze?
     *
     * @return true if is the entrance, false if not.
     */
    Boolean isEntrance();

    /**
     * Is this cell the exit to the maze?
     *
     * @return true if it is the exit, false if not.
     */
    Boolean isExit();

    /**
     * Returns an iterable of all the cells that it is legal to move to
     * from this cell.
     *
     * @return iterable of cells.
     */
    Collection<Cell> neighbors();
}
