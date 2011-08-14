using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SudokuSolver.Game;

namespace SudokuSolver.Utility
{
    public class SudokuPuzzleImporter : ISudokuPuzzleStringImporter, ISudokuPuzzleXmlImporter
    {
        private const string XML_ELM_ROOT = "SudokuPuzzle";
        private const string XML_ELM_BOARD = "Board";
        private const string XML_ELM_ROW = "Row";
        private const string XML_ELM_CELL = "Cell";
        private const string XML_ATT_SIZE = "Size";
        private const string XML_ATT_ROW = "Row";
        private const string XML_ATT_COL = "Col";

        private const char DELIMITER_ROW = '\n';
        private const char DELIMITER_COL = ',';

        #region IImporter<string,ISudokuPuzzle> Members

        public ISudokuPuzzle Import(string source)
        {
            return ParseDelimitedString(source);
        }

        #endregion

        private ISudokuPuzzle ParseDelimitedString(string source)
        {
            //int size = ParsePuzzleSize(source);
            //ISudokuPuzzle puzzle = new SudokuPuzzle(size);

            //ParsePuzzle(source, puzzle);
            
            //return puzzle;

            return ParsePuzzle2(source);
        }

        private int ParsePuzzleSize(string source)
        {
            int colCountPrev = -1;
            int colCountCurrent = 0;
            int rowCount = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == DELIMITER_COL)
                {
                    colCountCurrent++;
                }
                else if (source[i] == DELIMITER_ROW)
                {
                    if (colCountPrev != -1)
                    {
                        if (colCountPrev != colCountCurrent)
                        {
                            throw new ArgumentException("Input string contains inconsistent column counts.");
                        }
                    }

                    colCountPrev = colCountCurrent;
                    rowCount++;
                }
            }

            if (colCountCurrent != rowCount)
            {
                throw new ArgumentException("Input string does not contain same number of columns and rows.");
            }

            // Sudoku puzzles are "size^2", so return square-root
            double size = Math.Sqrt(rowCount);

            return (int)size;
        }

        private void ParsePuzzle(string source, ISudokuPuzzle puzzle)
        {
            ISudokuPosition currentPosition = new SudokuPosition();

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == DELIMITER_COL)
                {
                    currentPosition.ColumnNumber++;
                }
                else if (source[i] == DELIMITER_ROW)
                {
                    currentPosition.RowNumber++;
                }
                else
                {
                    puzzle.SetValue(currentPosition, Int32.Parse(source[i].ToString()));
                }
            }
        }

        private ISudokuPuzzle ParsePuzzle2(string source)
        {
            ISudokuPuzzle puzzle;
            ISudokuPosition currentPosition = new SudokuPosition();

            string[] rows = source.Split(DELIMITER_ROW);
            string[] cols;

            puzzle = new SudokuPuzzle((int)Math.Sqrt(rows.Length));

            for (int i = 0; i < rows.Length; i++)
            {
                currentPosition.RowNumber = i;
                cols = rows[i].Split(DELIMITER_COL);

                for (int j = 0; j < cols.Length; j++)
                {
                    currentPosition.ColumnNumber = j;

                    puzzle.SetValue(currentPosition, Int32.Parse(cols[j].ToString()));
                }
            }

            return puzzle;
        }

        #region IImporter<XmlNode,ISudokuPuzzle> Members

        public ISudokuPuzzle Import(XmlNode source)
        {
            int size = Int32.Parse(source.Attributes[XML_ATT_SIZE].Value);
            ISudokuPuzzle puzzle = new SudokuPuzzle(size);

            ParsePuzzleXml(source, puzzle);

            return puzzle;
        }

        #endregion

        private void ParsePuzzleXml(XmlNode source, ISudokuPuzzle puzzle)
        {
            int squareSize = puzzle.Size * puzzle.Size;
            ISudokuPosition currentPosition = new SudokuPosition();
            XmlElement elmBoard = source[XML_ELM_BOARD];
            XmlElement elmRow;
            XmlElement elmCell;
            XmlAttribute attRow;
            XmlAttribute attCol;

            foreach (XmlNode row in elmBoard.ChildNodes)
            {
                elmRow = (XmlElement)row;

                foreach (XmlNode cell in elmRow.ChildNodes)
                {
                    elmCell = (XmlElement)cell;
                    attRow = elmCell.Attributes[XML_ATT_ROW];
                    attCol = elmCell.Attributes[XML_ATT_COL];

                    currentPosition.RowNumber = Int32.Parse(attRow.Value);
                    currentPosition.ColumnNumber = Int32.Parse(attCol.Value);

                    puzzle.SetValue(currentPosition, Int32.Parse(elmCell.InnerText));
                }
            }

            //for (int i = 0; i < squareSize; i++)
            //{
            //    currentPosition.RowNumber = i;
            //    elmRow = (XmlElement)elmBoard.ChildNodes[i];

            //    for (int j = 0; j < squareSize; j++)
            //    {
            //        currentPosition.ColumnNumber = j;
            //        elmCell = (XmlElement)elmRow.ChildNodes[j];

            //        puzzle.SetValue(currentPosition, Int32.Parse(elmCell.InnerText));
            //    }
            //}
        }
    }
}
