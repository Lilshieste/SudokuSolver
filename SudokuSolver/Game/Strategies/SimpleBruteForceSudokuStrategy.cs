using System;
using System.Collections.Generic;
using System.Text;

using SudokuSolver.Game;

namespace SudokuSolver.Game.Strategies
{
    public class SimpleBruteForceSudokuStrategy : ISudokuStrategy
    {
        private ISudokuPuzzle puzzle;

        public SimpleBruteForceSudokuStrategy()
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

        private bool FindSolution()
        {
            bool solved = false;
            int value = 1;
            int squareSize = puzzle.Size * puzzle.Size;
            ISudokuPosition currentPosition = puzzle.GetNextEmptyPosition();

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
                        value++;
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
