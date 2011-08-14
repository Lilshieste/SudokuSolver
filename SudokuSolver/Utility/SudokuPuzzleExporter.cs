using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using SudokuSolver.Game;

namespace SudokuSolver.Utility
{
    public class SudokuPuzzleExporter : ISudokuPuzzleStringExporter, ISudokuPuzzleXmlExporter
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

        private string CreateDelimitedString(ISudokuPuzzle target)
        {
            int squareSize = target.Size * target.Size;
            StringBuilder sb = new StringBuilder();
            ISudokuPosition currentPosition = new SudokuPosition();

            for (int i = 0; i < squareSize; i++)
            {
                currentPosition.RowNumber = i;

                for (int j = 0; j < squareSize; j++)
                {
                    currentPosition.ColumnNumber = j;

                    sb.Append(target.GetValue(currentPosition));
                    if (j < squareSize - 1)
                    {
                        sb.Append(DELIMITER_COL);
                    }
                }

                if (i < squareSize - 1)
                {
                    sb.Append(DELIMITER_ROW);
                }
            }

            return sb.ToString();
        }

        #region IExporter<string,ISudokuBoard> Members

        string IExporter<string, ISudokuPuzzle>.Export(ISudokuPuzzle target)
        {
            return CreateDelimitedString(target);
        }

        #endregion

        #region IExporter<XmlNode,ISudokuPuzzle> Members

        public System.Xml.XmlNode Export(ISudokuPuzzle target)
        {
            XmlDocument doc = new XmlDocument();

            return CreateRootElement(doc, target);
        }

        #endregion

        private XmlNode CreateRootElement(XmlDocument document, ISudokuPuzzle puzzle)
        {
            XmlElement elmRoot = document.CreateElement(XML_ELM_ROOT);
            XmlAttribute attSize = document.CreateAttribute(XML_ATT_SIZE);

            attSize.Value = puzzle.Size.ToString();

            document.AppendChild(elmRoot);
            elmRoot.AppendChild(CreateBoardElement(document, puzzle));
            elmRoot.Attributes.Append(attSize);

            return elmRoot;
        }

        private XmlNode CreateBoardElement(XmlDocument document, ISudokuPuzzle puzzle)
        {
            int squareSize = puzzle.Size * puzzle.Size;
            ISudokuPosition currentPosition = new SudokuPosition();
            XmlElement elmBoard = document.CreateElement(XML_ELM_BOARD);
            XmlElement elmRow;
            XmlElement elmCell;
            XmlAttribute attRow;
            XmlAttribute attCol;

            for (int i = 0; i < squareSize; i++)
            {
                currentPosition.RowNumber = i;
                
                elmRow = document.CreateElement(XML_ELM_ROW);

                for (int j = 0; j < squareSize; j++)
                {
                    currentPosition.ColumnNumber = j;

                    elmCell = document.CreateElement(XML_ELM_CELL);
                    attRow = document.CreateAttribute(XML_ATT_ROW);
                    attCol = document.CreateAttribute(XML_ATT_COL);

                    attRow.Value = i.ToString();
                    attCol.Value = j.ToString();
                    elmCell.InnerText = puzzle.GetValue(currentPosition).ToString();
                    elmCell.Attributes.Append(attRow);
                    elmCell.Attributes.Append(attCol);

                    elmRow.AppendChild(elmCell);
                }

                elmBoard.AppendChild(elmRow);
            }

            return elmBoard;
        }
    }
}
