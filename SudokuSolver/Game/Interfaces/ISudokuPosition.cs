using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game
{
    public interface ISudokuPosition
    {
        int RowNumber { get; set; }
        int ColumnNumber { get; set; }
    }
}
