using System;
using System.Collections.Generic;
using System.Text;

using SudokuSolver.Game;

namespace SudokuSolver.Game.Strategies
{
    public abstract class BruteForceSudokuStrategyOLD : ISudokuStrategy
    {
        ISudokuPuzzle puzzle;

        public BruteForceSudokuStrategyOLD()
        {
            puzzle = null;
        }

        #region ISudokuStrategy Members

        public ISudokuPuzzle Puzzle
        {
            get
            {
                return puzzle;
            }
        }

        public void Solve(ISudokuPuzzle puzzle)
        {
            this.puzzle = puzzle;
            FindSolution();
        }

        #endregion

        protected abstract int GetNextPossibleValue(ISudokuPosition position);

        private bool FindSolution()
        {
            bool solved = false;
            int squareSize = puzzle.Size * puzzle.Size;
            ISudokuPosition currentPosition = puzzle.GetNextEmptyPosition();
            int value = GetNextPossibleValue(currentPosition);

            if (currentPosition == null)
            {
                solved = true;
            }
            else
            {
                while (value <= squareSize && !solved)
                {
                    if (puzzle.SetValue(currentPosition, value))
                    {
                        solved = FindSolution();
                    }
                    else
                    {
                        value = GetNextPossibleValue(currentPosition);
                    }
                }
            }

            if (!solved)
            {
                puzzle.ClearValue(currentPosition);
            }

            return solved;
        }
    }
}
