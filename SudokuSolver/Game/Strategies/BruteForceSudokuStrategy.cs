using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game.Strategies
{
    public class BruteForceSudokuStrategy : ValueCycleSudokuStrategy
    {
        private Dictionary<ISudokuPosition, IValueCyclableCollection> valuesTable;

        public BruteForceSudokuStrategy()
            : base()
        {
        }

        protected override void DoSolve()
        {
            InitializeValuesTable();

            base.DoSolve();
        }

        public override int GetNextValue(ISudokuPosition position)
        {
            return valuesTable[position].GetNextValue();
        }

        public override void ResetCycle(ISudokuPosition position)
        {
            valuesTable[position].ResetCycle();
        }

        private void InitializeValuesTable()
        {
            valuesTable = new Dictionary<ISudokuPosition, IValueCyclableCollection>();

            int squareSize = Puzzle.Size * Puzzle.Size;
            ISudokuPosition currentPosition;

            // Create an entry for each position, containing all possible values
            for (int i = 0; i < squareSize; i++)
            {
                for (int j = 0; j < squareSize; j++)
                {
                    currentPosition = new SudokuPosition();
                    currentPosition.RowNumber = i;
                    currentPosition.ColumnNumber = j;

                    valuesTable.Add(currentPosition, CreateValueCyclableCollection());
                }
            }
        }

        private IValueCyclableCollection CreateValueCyclableCollection()
        {
            ValueCyclableCollection collection = new ValueCyclableCollection();
            int squareSize = Puzzle.Size * Puzzle.Size;

            for (int i = 1; i <= squareSize; i++)
            {
                collection.Add(i);
            }

            return collection;
        }
    }
}
