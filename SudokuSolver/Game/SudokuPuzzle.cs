using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game
{
    public class SudokuPuzzle : ISudokuPuzzle
    {
        private const int MINIMUM_SIZE = 2;

        // 0 => MINIMUM_SIZE
        private const string STRING_FORMAT_ERR_SMALL_SIZE = "Size of puzzle must be at least {0}.";

        private int puzzleSize;
        private ISudokuBoard board;

        public SudokuPuzzle(int size)
        {
            SetSize(size);

            InitializePuzzle();
        }

        public bool Solve(ISudokuStrategy strategy)
        {
            strategy.Solve(this);

            return IsSolved();
        }

        #region ISudokuPuzzle Members

        public int Size
        {
            get
            {
                return puzzleSize;
            }
        }

        public int GetValue(ISudokuPosition position)
        {
            return board[position.RowNumber, position.ColumnNumber];
        }

        public bool SetValue(ISudokuPosition position, int value)
        {
            if (ValidatePosition(position, value))
            {
                DoSetValue(position, value);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ClearValue(ISudokuPosition position)
        {
            return board.ClearValue(position.RowNumber, position.ColumnNumber);
        }

        public bool IsSolved()
        {
            return Evaluate();
        }

        public ISudokuRegion GetRegion(ISudokuPosition position)
        {
            return new SudokuRegion(this, position);
        }

        public ISudokuRegion GetRegionByValuePosition(ISudokuPosition position)
        {
            int regionRow = position.RowNumber / Size;
            int regionCol = position.ColumnNumber / Size;
            ISudokuPosition regionPosition = new SudokuPosition(regionRow, regionCol);

            return GetRegion(regionPosition);
        }

        public ISudokuPosition GetNextEmptyPosition()
        {
            // Look into a more efficient way of doing this...
            for (int i = 0; i < SquareSize; i++)
            {
                for (int j = 0; j < SquareSize; j++)
                {
                    if (board[i, j] == board.EmptyValue)
                    {
                        return new SudokuPosition(i, j);
                    }
                }
            }

            return null;
        }

        #endregion

        private int SquareSize
        {
            get
            {
                return puzzleSize * puzzleSize;
            }
        }

        private void InitializePuzzle()
        {
            //board = new int[SquareSize, SquareSize];
            board = new SimpleSudokuBoard(SquareSize);
        }

        private void SetSize(int size)
        {
            ValidateSize(size);
            puzzleSize = size;
        }

        private void DoSetValue(ISudokuPosition position, int value)
        {
            board[position.RowNumber, position.ColumnNumber] = value;
        }

        private void ValidateSize(int size)
        {
            if (size < MINIMUM_SIZE)
            {
                throw new ArgumentException(String.Format(STRING_FORMAT_ERR_SMALL_SIZE, MINIMUM_SIZE));
            }
        }

        private bool ValidatePosition(ISudokuPosition position, int value)
        {
            return (ValidatePositionRow(position, value) &&
                    ValidatePositionColumn(position, value) &&
                    ValidatePositionRegion(position, value));
        }

        private bool ValidatePositionRow(ISudokuPosition position, int value)
        {
            int row = position.RowNumber;
            
            for (int i = 0; i < SquareSize; i++)
            {
                if (board[row, i] == value)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidatePositionColumn(ISudokuPosition position, int value)
        {
            int col = position.ColumnNumber;

            for (int i = 0; i < SquareSize; i++)
            {
                if (board[i, col] == value)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidatePositionRegion(ISudokuPosition position, int value)
        {
            ISudokuRegion region = GetRegionByValuePosition(position);
            ISudokuPosition currentPosition = new SudokuPosition();

            for (int i = 0; i < region.Size; i++)
            {
                currentPosition.RowNumber = i;

                for (int j = 0; j < region.Size; j++)
                {
                    currentPosition.ColumnNumber = j;

                    if (region.GetValue(currentPosition) == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool Evaluate()
        {
            // Since validation occurs at insertion, just check if there
            //  are any more cells to be filled

            return GetNextEmptyPosition() == null;
        }
    }
}
