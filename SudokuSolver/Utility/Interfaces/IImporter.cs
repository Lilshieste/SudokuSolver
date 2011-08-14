using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Utility
{
    public interface IImporter<T, U>
    {
        U Import(T source);
    }
}
