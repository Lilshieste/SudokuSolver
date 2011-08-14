using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game
{
    public interface IValueGrid<T> where T : struct
    {
        T EmptyValue { get; }

        int Size { get; }

        T this[int rowPosition, int columnPosition] { get; set; }

        T GetValue(int rowPosition, int columnPosition);

        bool SetValue(int rowPosition, int columnPosition, T value);

        bool ClearValue(int rowPosition, int columnPosition);
    }
}
