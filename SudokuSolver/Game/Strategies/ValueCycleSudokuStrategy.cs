using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Game.Strategies
{
    public abstract class ValueCycleSudokuStrategy : SudokuStrategy, IValueCycleSudokuStrategy
    {
        protected override void DoSolve()
        {
            FindSolution();
        }

        #region IValueCycleSudokuStrategy Members

        public abstract int GetNextValue(ISudokuPosition position);

        public abstract void ResetCycle(ISudokuPosition position);

        #endregion

        private bool FindSolution()
        {
            bool solved = false;
            int squareSize = Puzzle .Size * Puzzle.Size;
            ISudokuPosition currentPosition = Puzzle.GetNextEmptyPosition();

            if (currentPosition == null)
            {
                solved = true;
            }
            else
            {
                int value = GetNextValue(currentPosition);

                while (value != ValueCyclableCollection.EmptyValue && !solved)
                {
                    if (Puzzle.SetValue(currentPosition, value))
                    {
                        solved = FindSolution();
                    }
                    else
                    {
                        value = GetNextValue(currentPosition);
                    }
                }
            }

            if (!solved)
            {
                Puzzle.ClearValue(currentPosition);
                ResetCycle(currentPosition);
            }

            return solved;
        }
    }

    public sealed class ValueCyclableCollection : IValueCyclableCollection
    {
        public const int EmptyValue = -1;

        IList<int> values;
        int currentIndex;

        public ValueCyclableCollection()
        {
            values = new List<int>();
            currentIndex = 0;
        }

        #region IValueCyclableList Members

        public int GetNextValue()
        {
            if (currentIndex < values.Count)
            {
                return values[currentIndex++];
            }
            else
            {
                return EmptyValue;
            }
        }

        public void ResetCycle()
        {
            currentIndex = 0;
        }

        #endregion

        #region ICollection<int> Members

        public void Add(int value)
        {
            values.Add(value);
        }

        public void Clear()
        {
            values.Clear();
        }

        public bool Contains(int value)
        {
            return values.Contains(value);
        }

        public void CopyTo(int[] array, int arrayIndex)
        {
            values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return values.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return values.IsReadOnly;
            }
        }

        public bool Remove(int value)
        {
            return values.Remove(value);
        }

        #endregion

        #region IEnumerable<int> Members

        public IEnumerator<int> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }

        #endregion
    }
}
