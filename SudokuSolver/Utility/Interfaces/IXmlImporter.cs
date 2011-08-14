using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SudokuSolver.Utility
{
    public interface IXmlImporter<T> : IImporter<XmlNode, T>
    {
    }
}
