using System;
using System.Collections.Generic;
using System.Text;

using SudokuSolver.Game;

namespace SudokuSolver.Game.Strategies
{
    public abstract class SudokuStrategy : ISudokuStrategy
    {
        private ISudokuPuzzle puzzle;

        protected SudokuStrategy()
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
            DoSolve();
        }

        #endregion

        protected abstract void DoSolve();
    }
}
