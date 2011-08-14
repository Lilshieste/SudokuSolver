using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game
{
    public interface ISudokuStrategy
    {
        ISudokuPuzzle Puzzle { get; }
        void Solve(ISudokuPuzzle puzzle);
    }
}
