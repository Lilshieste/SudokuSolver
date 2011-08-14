using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.XPath;

using LS.Framework.XslPage;

using SudokuSolver.Game;
using SudokuSolver.Game.Strategies;
using SudokuSolver.Utility;

public partial class _Default : BaseXslPage 
{
    private const int PUZZLE_SIZE = 3;
    private const string XSL_FILENAME = "Stylesheets/Default.xsl";

    private ISudokuPuzzle inputPuzzle;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SubmittedToSelf)
        {
            AddNodeToPageXml(CreateParsedInputPuzzleNode());
            AddNodeToPageXml(CreateSolutionPuzzleNode());
        }
        else
        {
            AddNodeToPageXml(CreateInputPuzzleNode());
            AddNodeToPageXml(CreateEmptySolutionPuzzleNode());
        }
    }

    public override System.Xml.XPath.IXPathNavigable GetPageXsl()
    {
        return new XPathDocument(Server.MapPath(XSL_FILENAME));
    }

    private XmlNode CreateParsedInputPuzzleNode()
    {
        inputPuzzle = ParseInputPuzzleFromForm();

        ISudokuPuzzleXmlExporter exporter = new SudokuPuzzleExporter();
        XmlNode puzzleNode = exporter.Export(inputPuzzle);
        XmlElement inputPuzzleNode = puzzleNode.OwnerDocument.CreateElement("InputPuzzle");
        inputPuzzleNode.AppendChild(puzzleNode);

        return inputPuzzleNode;
    }

    private XmlNode CreateInputPuzzleNode()
    {
        ISudokuPuzzleStringImporter si = new SudokuPuzzleImporter();
        inputPuzzle = si.Import("0,0,0,0,5,1,2,0,0\n2,0,1,0,9,0,0,8,5\n0,9,4,0,0,2,0,0,7\n6,0,0,0,2,0,7,3,0\n9,0,0,0,8,0,0,0,1\n0,3,8,0,7,0,0,0,6\n7,0,0,2,0,0,8,5,0\n3,8,0,0,4,0,1,0,9\n0,0,6,9,3,0,0,0,0");

        //ISudokuPuzzle inputPuzzle = new SudokuPuzzle(PUZZLE_SIZE);
        ISudokuPuzzleXmlExporter exporter = new SudokuPuzzleExporter();
        XmlNode puzzleNode = exporter.Export(inputPuzzle);
        XmlElement inputPuzzleNode = puzzleNode.OwnerDocument.CreateElement("InputPuzzle");
        inputPuzzleNode.AppendChild(puzzleNode);

        return inputPuzzleNode;
    }

    private XmlNode CreateEmptySolutionPuzzleNode()
    {
        inputPuzzle = new SudokuPuzzle(PUZZLE_SIZE);
        ISudokuPuzzleXmlExporter exporter = new SudokuPuzzleExporter();
        XmlNode puzzleNode = exporter.Export(inputPuzzle);
        XmlElement inputPuzzleNode = puzzleNode.OwnerDocument.CreateElement("SolutionPuzzle");
        inputPuzzleNode.AppendChild(puzzleNode);

        return inputPuzzleNode;
    }

    private XmlNode CreateSolutionPuzzleNode()
    {
        inputPuzzle = ParseInputPuzzleFromForm();
        //ISudokuStrategy strat = new SimpleBruteForceSudokuStrategy();
        //ISudokuStrategy strat = new BruteForceSudokuStrategy();
        ISudokuStrategy strat = new CycleFirstBruteForceSudokuStrategy();
        int start = Environment.TickCount;
        bool solved = inputPuzzle.Solve(strat);
        int end = Environment.TickCount;
        int tts = end - start;

        ISudokuPuzzleXmlExporter exporter = new SudokuPuzzleExporter();
        XmlNode puzzleNode = exporter.Export(inputPuzzle);
        XmlElement inputPuzzleNode = puzzleNode.OwnerDocument.CreateElement("SolutionPuzzle");
        XmlAttribute attSolved = puzzleNode.OwnerDocument.CreateAttribute("IsSolved");
        XmlAttribute attTts = puzzleNode.OwnerDocument.CreateAttribute("TimeToSolve");
        
        attSolved.Value = solved.ToString();
        attTts.Value = tts.ToString();
        inputPuzzleNode.AppendChild(puzzleNode);
        inputPuzzleNode.Attributes.Append(attSolved);
        inputPuzzleNode.Attributes.Append(attTts);

        return inputPuzzleNode;
    }

    private ISudokuPuzzle ParseInputPuzzleFromForm()
    {
        ISudokuPuzzle puzzle = new SudokuPuzzle(PUZZLE_SIZE);
        ISudokuPosition currentPosition = new SudokuPosition();

        foreach (string key in Request.Form.AllKeys)
        {
            int trash; // Dummy variable
            if(Int32.TryParse(key.Substring(0, 1), out trash))
            {
                string[] row_col = key.Split('_');
                currentPosition.RowNumber = Int32.Parse(row_col[0]);
                currentPosition.ColumnNumber = Int32.Parse(row_col[1]);
                int value = Int32.Parse(Request.Form[key]);

                puzzle.SetValue(currentPosition, value);
            }
        }

        return puzzle;
    }
}
