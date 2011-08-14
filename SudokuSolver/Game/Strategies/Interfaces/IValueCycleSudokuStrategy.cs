using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game.Strategies
{
    public interface IValueCycleSudokuStrategy : ISudokuStrategy
    {
        /// <summary>
        /// Retrieves the next possible value available for the specified position.
        /// </summary>
        /// <param name="position">The position in the puzzle for which the next value
        ///  is being retrieved.</param>
        /// <returns>The next value available for the position.</returns>
        int GetNextValue(ISudokuPosition position);

        /// <summary>
        /// Restarts the value cycle.
        /// The next call to GetNextValue will return the first value in the collection.
        /// </summary>
        void ResetCycle(ISudokuPosition position);
    }

    public interface IValueCyclableCollection : ICollection<int>
    {
        /// <summary>
        /// Retrieves the next value in the collection.
        /// </summary>
        /// <returns>The next value in the collection, or -1 if there are no more values.</returns>
        int GetNextValue();

        /// <summary>
        /// Restarts the value cycle.
        /// The next call to GetNextValue will return the first value in the collection.
        /// </summary>
        void ResetCycle();
    }
}
