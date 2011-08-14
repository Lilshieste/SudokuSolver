using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game
{
    public interface ISudokuPuzzle
    {
        int Size { get; }

        int GetValue(ISudokuPosition position);
        bool SetValue(ISudokuPosition position, int value);
        bool ClearValue(ISudokuPosition position);

        bool Solve(ISudokuStrategy strategy);
        bool IsSolved();

        ISudokuRegion GetRegion(ISudokuPosition position);
        ISudokuRegion GetRegionByValuePosition(ISudokuPosition position);

        /// <summary>
        /// Retrieves the next available empty position, starting from the top-left corner
        ///  of the puzzle.
        /// </summary>
        /// <returns>The ISudokuPosition of the empty cell, or null if all positions are filled.</returns>
        ISudokuPosition GetNextEmptyPosition();
    }
}
