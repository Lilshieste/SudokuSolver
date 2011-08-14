using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game.Strategies
{
    public class CycleFirstBruteForceSudokuStrategy : ValueCycleSudokuStrategy
    {
        private Dictionary<ISudokuPosition, IValueCyclableCollection> valuesTable;

        public CycleFirstBruteForceSudokuStrategy()
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

            // Create an entry for each position
            for (int i = 0; i < squareSize; i++)
            {
                for (int j = 0; j < squareSize; j++)
                {
                    currentPosition = new SudokuPosition();
                    currentPosition.RowNumber = i;
                    currentPosition.ColumnNumber = j;

                    valuesTable.Add(currentPosition, CreateValueCyclableCollection(currentPosition));
                }
            }
        }

        private IValueCyclableCollection CreateValueCyclableCollection(ISudokuPosition position)
        {
            ValueCyclableCollection collection = new ValueCyclableCollection();
            int squareSize = Puzzle.Size * Puzzle.Size;

            for (int i = 1; i <= squareSize; i++)
            {
                if (!ValueExistsInRow(position, i) &&
                    !ValueExistsInColumn(position, i) &&
                    !ValueExistsInRegion(position, i))
                {
                    collection.Add(i);
                }
            }

            return collection;
        }

        private bool ValueExistsInRow(ISudokuPosition position, int value)
        {
            int squareSize = Puzzle.Size * Puzzle.Size;
            ISudokuPosition currentPosition = new SudokuPosition();
            currentPosition.RowNumber = position.RowNumber;

            for (int i = 0; i < squareSize; i++)
            {
                currentPosition.ColumnNumber = i;

                if (Puzzle.GetValue(currentPosition) == value)
                {
                    return true;
                }
            }

            return false;
        }

        private bool ValueExistsInColumn(ISudokuPosition position, int value)
        {
            int squareSize = Puzzle.Size * Puzzle.Size;
            ISudokuPosition currentPosition = new SudokuPosition();
            currentPosition.ColumnNumber = position.ColumnNumber;

            for (int i = 0; i < squareSize; i++)
            {
                currentPosition.RowNumber= i;

                if (Puzzle.GetValue(currentPosition) == value)
                {
                    return true;
                }
            }

            return false;
        }

        private bool ValueExistsInRegion(ISudokuPosition position, int value)
        {
            ISudokuRegion region = Puzzle.GetRegionByValuePosition(position);
            ISudokuPosition currentPosition = new SudokuPosition();

            for (int i = 0; i < region.Size; i++)
            {
                currentPosition.RowNumber = i;

                for (int j = 0; j < region.Size; j++)
                {
                    currentPosition.ColumnNumber = j;

                    if (region.GetValue(currentPosition) == value)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
