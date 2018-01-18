package com.tavaresstudios;

public class Mazey {
    private Maze maze;

    public Mazey() {
        this.maze = new Maze(20, 30);
    }

    public static void main(String args[]) {
        Mazey app = new Mazey();
        app.run();
    }

    public void run() {
        MazeMaker maker = new MazeMaker(maze);
        maker.makeMaze();

        MazeSolver solver = new MazeSolver(maze);
        Maze solved = solver.solve();

        MazePrinter printer = new MazePrinter(solver.isSolutionPathPredicate(), new UnicodeCharSet(), System.out);
        printer.printMaze(solved);
    }
}
