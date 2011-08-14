using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Utility
{
    public interface IExporter<T, U>
    {
        T Export(U target);
    }
}
