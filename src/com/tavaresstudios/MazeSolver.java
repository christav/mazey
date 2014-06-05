package com.tavaresstudios;

public class MazeSolver {
    private final Maze maze;

    public MazeSolver(Maze maze) {
        this.maze = maze;
    }

    public Maze solve() {
        return maze;
    }

    public SolutionCellPredicate isSolutionPathPredicate() {
        return new SolutionCellPredicate() {
            @Override
            public Boolean check(Cell cell) {
                return false;
            }
        };
    }
}
