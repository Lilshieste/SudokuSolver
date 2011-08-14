using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game
{
    public class SudokuRegion : ISudokuRegion
    {
        ISudokuBoard board;

        public SudokuRegion(ISudokuPuzzle puzzle, ISudokuPosition position)
        {
            InitializeRegion(puzzle, position);
        }

        private void InitializeRegion(ISudokuPuzzle puzzle, ISudokuPosition position)
        {
            ISudokuPosition currentPosition = new SudokuPosition();
            board = new SimpleSudokuBoard(puzzle.Size);

            for (int i = 0; i < puzzle.Size; i++)
            {
                for (int j = 0; j < puzzle.Size; j++)
                {
                    currentPosition.RowNumber = puzzle.Size * position.RowNumber + i;
                    currentPosition.ColumnNumber = puzzle.Size * position.ColumnNumber + j;
                    board.SetValue(i, j, puzzle.GetValue(currentPosition));
                }
            }
        }

        #region ISudokuRegion Members

        int ISudokuRegion.Size
        {
            get
            {
                return board.Size;
            }
        }

        int ISudokuRegion.GetValue(ISudokuPosition position)
        {
            return board[position.RowNumber, position.ColumnNumber];
        }

        bool ISudokuRegion.SetValue(ISudokuPosition position, int value)
        {
            return board.SetValue(position.RowNumber, position.ColumnNumber, value);
        }

        #endregion
    }
}
