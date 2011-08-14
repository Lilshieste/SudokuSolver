using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game
{
    public interface ISudokuRegion
    {
        int Size { get; }

        int GetValue(ISudokuPosition position);
        bool SetValue(ISudokuPosition position, int value);
    }
}
