using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game
{
    public class SimpleSudokuBoard : ISudokuBoard
    {
        private const int EMPTY_VALUE = 0;

        private int boardSize;
        private int[,] board;

        public SimpleSudokuBoard(int size)
        {
            boardSize = size;
            board = new int[size, size];
        }

        #region IValueGrid<int> Members

        public int EmptyValue
        {
            get
            {
                return EMPTY_VALUE;
            }
        }

        public int Size
        {
            get
            {
                return boardSize;
            }
        }

        public int this[int rowPosition, int columnPosition]
        {
            get
            {
                return GetValue(rowPosition, columnPosition);
            }

            set
            {
                SetValue(rowPosition, columnPosition, value);
            }
        }

        public int GetValue(int rowPosition, int columnPosition)
        {
            return board[rowPosition, columnPosition];
        }

        public bool SetValue(int rowPosition, int columnPosition, int value)
        {
            board[rowPosition, columnPosition] = value;

            return true;
        }

        public bool ClearValue(int rowPosition, int columnPosition)
        {
            board[rowPosition, columnPosition] = EmptyValue;

            return true;
        }

        #endregion
    }
}
