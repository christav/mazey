package com.tavaresstudios;

/**
 * Interface defining the set of characters to draw when printing
 * the maze.
 * <p>
 * This gives strings for the various different corners and crossings
 * plus a lookup for the character by a bit-field index.
 */
public interface CharSet {
    /**
     * Get the appropriate character for drawing the
     * maze corners given a bit mask describing the various
     * combination of corners and directions to go in.
     * <p>
     * The bit mask with bits abcd corresponds to:
     * a: wall on top
     * b: wall on right
     * c: wall on bottom
     * d: wall on right
     *
     * @param charMask bitmask describing the require char as shown above
     * @return string to print the required maze outline
     */
    String getOutlineChar(int charMask);

    /**
     * Get the correct character for a solution cell in the
     * maze. See {@link #getOutlineChar(int)} to see how the
     * char mask works, except that in this case the path
     * string is three characters wide and the mask describes
     * the path in the middle of the cell not a cell outline.
     *
     * @param charMask bitmask describing the required character
     * @return string to print as cell contents.
     */
    String getSolutionChar(int charMask);
}
