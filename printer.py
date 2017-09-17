""" Function to print a maze """

from directions import Directions
from solver import is_solution_cell

def print_maze(out, maze):
    """ Print the maze to the given stream """
    for row in maze.all_rows():
        print_row(out, row)
    out.write("+---" * maze.cols + "+\n")

def print_row(out, row):
    """ Print one row of the maze """
    for cell in row:
        out.write("+")
        if cell.can_go(Directions.UP):
            out.write("   ")
        else:
            out.write("---")
    out.write("+\n")

    for cell in row:
        if cell.can_go(Directions.LEFT):
            out.write(" ")
        else:
            out.write("|")
        if is_solution_cell(cell):
            out.write("XXX")
        else:
            out.write("   ")
    
    last_cell = row[-1]
    if last_cell.can_go(Directions.RIGHT):
        out.write(" ")
    else:
        out.write("|")
    out.write("\n")
