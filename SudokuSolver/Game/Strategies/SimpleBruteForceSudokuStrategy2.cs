using System;
using System.Collections.Generic;
using System.Text;

using SudokuSolver.Game;

namespace SudokuSolver.Game.Strategies
{
    public class SimpleBruteForceSudokuStrategy2 : BruteForceSudokuStrategyOLD
    {
        private ISudokuPosition currentPosition;
        private int currentValue;

        public SimpleBruteForceSudokuStrategy2()
        {
            currentPosition = null;
            currentValue = 1;
        }

        protected override int GetNextPossibleValue(ISudokuPosition position)
        {
            if (position == currentPosition)
            {
                currentValue++;
            }
            else
            {
                currentPosition = position;
                currentValue = 1;
            }

            return currentValue;
        }
    }
}
