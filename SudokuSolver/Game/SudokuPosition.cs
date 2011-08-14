using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game
{
    public class SudokuPosition : ISudokuPosition
    {
        private const int DEFAULT_NUMBER_COLUMN = 0;
        private const int DEFAULT_NUMBER_ROW = 0;

        private int numberColumn;
        private int numberRow;

        public SudokuPosition()
            : this(DEFAULT_NUMBER_COLUMN, DEFAULT_NUMBER_ROW)
        {
        }

        public SudokuPosition(int rowNumber, int columnNumber)
        {
            numberRow = rowNumber;
            numberColumn = columnNumber;
        }

        #region ISudokuPosition Members

        public int RowNumber
        {
            get
            {
                return numberRow;
            }
            set
            {
                numberRow = value;
            }
        }

        public int ColumnNumber
        {
            get
            {
                return numberColumn;
            }
            set
            {
                numberColumn = value;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            ISudokuPosition pos = obj as ISudokuPosition;

            if (pos == null)
            {
                return false;
            }
            else
            {
                return RowNumber == pos.RowNumber &&
                        ColumnNumber == pos.ColumnNumber;
            }
        }

        public override int GetHashCode()
        {
            return 10 * RowNumber + ColumnNumber;
        }
    }
}
