package com.tavaresstudios;

/**
 * Created with IntelliJ IDEA.
 * User: cct_000
 * Date: 6/4/14
 * Time: 10:39 PM
 * To change this template use File | Settings | File Templates.
 */
public interface SolutionCellPredicate {
    /**
     * Check if the given cell is part of the solution path.
     * @param cell cell to check
     * @return true if part of solution, false if not.
     */
    Boolean check(Cell cell);
}

