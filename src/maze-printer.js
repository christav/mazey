'use strict';

const { directions } = require('./maze');

function printMaze(maze, isSolutionCell, writeLine = console.log) {
  for(let row of maze.rows()) {
    let line = '';
    for(let cell of row) {
        line += '+';
        line += cell.canGo(directions.up) ?
          isSolutionPath(cell, directions.up, isSolutionCell) ? 'XXX' : '   ' 
          : '---';
    }
    line += '+';
    writeLine(line);

    line = '';
    for(let cell of row) {
      if (cell.isEntrance) {
        line += ' ';
      } else {
        line += cell.canGo(directions.left) ?
          isSolutionPath(cell, directions.left, isSolutionCell) ? 'X' : ' '
          : '|';
      }
      line += isSolutionCell(cell) ? 'XXX' : '   ';
    }
    line += row[row.length-1].isExit ? ' ' : '|';
    writeLine(line);
  }

  let line = '';
  for(let col = 0; col < maze.width; ++col) {
    line += '+---';
  }
  line += '+';
  writeLine(line);
}

function isSolutionPath(cell, direction, isSolutionCell) {
  if (isSolutionCell(cell)) {
    return cell.go(direction).isInMaze && isSolutionCell(cell.go(direction));
  }
  return false;
}

module.exports = { printMaze };
