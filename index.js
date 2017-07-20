'use strict';

const argv = require('minimist')(process.argv.slice(2));
const { Maze } = require('./src/maze');
const { makeMaze } = require('./src/maze-maker');
const { printMaze } = require('./src/maze-printer');
const { solveMaze, isSolutionCell } = require('./src/maze-solver');

argv.w = argv.w || 10;
argv.h = argv.h || 10;

let maze = makeMaze(argv.w, argv.h);
solveMaze(maze);
printMaze(maze, isSolutionCell);
