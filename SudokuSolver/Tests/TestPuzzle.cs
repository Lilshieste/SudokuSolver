using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using SudokuSolver.Game;
using SudokuSolver.Game.Strategies;
using SudokuSolver.Utility;

namespace SudokuSolver.Tests
{
    [TestFixture]
    public class TestPuzzle
    {
        //private const string INPUT_PUZZLE = "1,2,3,4\n3,4,1,2\n2,1,4,3\n4,3,2,1";
        //private const string INPUT_PUZZLE = "1,2,3,4\n3,4,1,2\n2,1,4,3\n4,3,0,1";
        //private const string INPUT_PUZZLE = "1,0,0,0\n0,0,1,2\n2,1,4,3\n4,3,0,1";

        //private const string INPUT_PUZZLE = "0,0,0,0,5,1,2,0,0\n2,0,1,0,9,0,0,8,5\n0,9,4,0,0,2,0,0,7\n6,0,0,0,2,0,7,3,0\n9,0,0,0,8,0,0,0,1\n0,3,8,0,7,0,0,0,6\n7,0,0,2,0,0,8,5,0\n3,8,0,0,4,0,1,0,9\n0,0,6,9,3,0,0,0,0";
        private const string INPUT_PUZZLE = "8,0,0,0,0,0,7,0,0\n0,0,2,0,3,0,0,0,0\n0,9,0,4,0,0,0,1,0\n0,8,5,6,0,2,0,0,0\n0,0,0,0,0,0,3,0,7\n0,0,0,0,0,9,5,0,0\n0,0,1,0,0,0,8,0,0\n6,5,0,2,0,4,0,0,0\n0,0,0,0,0,5,4,6,0";

        ISudokuPuzzle puzzle;
        ISudokuStrategy strategy;

        [SetUp]
        public void SetUp()
        {
            ISudokuPuzzleStringImporter importer = new SudokuPuzzleImporter();
            puzzle = importer.Import(INPUT_PUZZLE);

            //strategy = new BruteForceSudokuStrategy();
            strategy = new SimpleBruteForceSudokuStrategy();
            //strategy = new SimpleBruteForceSudokuStrategy2();
            //strategy = new CycleFirstBruteForceSudokuStrategy();
        }

        [Test]
        public void TestMain()
        {
            int start = Environment.TickCount;
            Assert.IsTrue(puzzle.Solve(strategy), "Did not solve the puzzle.");
            int end = Environment.TickCount;

            Console.WriteLine(String.Format("Time elapsed: {0} milliseconds", end - start));
            ISudokuPuzzleStringExporter exporter = new SudokuPuzzleExporter();
            Console.WriteLine(exporter.Export(puzzle));
        }
    }
}
