using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MinesweeperApp.Library.Models
{
    public class Game
    {
        private List<SimulationResult> ListOfSimulationsToRun;

        private Game()
        {
            ListOfSimulationsToRun = new List<SimulationResult>();
            Initialize();
        }

        /// <summary>
        /// Not really a Singleton, but i'd rather have one running at a time.
        /// </summary>
        /// <returns></returns>
        public static Game CreateNewGame()
        {
            return new Game();
        }

        private void Initialize()
        {
            bool continueAskingForGridSize = true;

            do
            {
                var rowsAndColumnsAsString = Console.ReadLine();

                //re-use the Tile object to store the grid width & height
                var gridSizeAsTile = ParseInputAsTile(rowsAndColumnsAsString);

                if (gridSizeAsTile.RowNumber == 0 && gridSizeAsTile.ColumnNumber == 0)
                {
                    //we have Grid with 0 by 0, meaning to start processing the simulations for Hints
                    continueAskingForGridSize = false;
                }
                else
                {
                    //valid grid size, prepare Simulation

                    //zero-based
                    int numberOfTimesWeAskedUserToInputRows = 0;

                    //create Simulation
                    var currentSimulation = new SimulationResult(gridSizeAsTile.ColumnNumber, gridSizeAsTile.RowNumber);

                    while (numberOfTimesWeAskedUserToInputRows < gridSizeAsTile.RowNumber)
                    {
                        //read user input
                        var inputtedTilesAsString = Console.ReadLine();

                        //parse the user input and return it to parse properly
                        var tiles = ParseInputAsTilesOrMines(inputtedTilesAsString, gridSizeAsTile.RowNumber);

                        //parse the tiles (as a string of . and *), and update the simulation
                        for (int i = 0; i < tiles.Length; i++)
                        {
                            //check if mine
                            if (tiles[i].Equals('*'))
                            {
                                var tileToIndexBy = new Tile(numberOfTimesWeAskedUserToInputRows, i, 0);

                                //assign it as a Mine
                                currentSimulation[tileToIndexBy] = new Mine(numberOfTimesWeAskedUserToInputRows, i, 0);

                                //increment the count onthe adjacent tiles
                                currentSimulation.UpdateHintCountOfAdjacentTiles(tileToIndexBy);
                            }
                        }

                        //increment the counter so that we stop asking accordingly
                        numberOfTimesWeAskedUserToInputRows++;
                    }

                    //add the simulation to the list to output
                    ListOfSimulationsToRun.Add(currentSimulation);
                }
            }
            while (continueAskingForGridSize);
        }


        public void Simulate()
        {
            for (int i = 0; i < ListOfSimulationsToRun.Count; i++)
            {
                //current simulation
                var simulation = ListOfSimulationsToRun[i];

                Console.WriteLine(Environment.NewLine + $"Field #{i + 1}:");

                //get the jagged array as a flat list for easier sequential processing & understanding
                var flattenedListOfTiles = simulation.Tiles.SelectMany(x => x).ToList();

                for (int j = 0; j < flattenedListOfTiles.Count; j++)
                {
                    //end of row, start new line
                    if(j != 0 && j % simulation.Width == 0)
                        Console.WriteLine();

                    //if a mine, output an asterisk instead of the mine count
                    if(flattenedListOfTiles[j] is Mine)
                        Console.Write("*");
                    else
                        Console.Write(flattenedListOfTiles[j].NumberOfAdjacentMines);
                }

                Console.WriteLine();
            }

           
        }


        private Tile ParseInputAsTile(string input)
        {
            /* TEST CASES, i can come up with to validate this works as intended - this comment is here strictly for the assignment, i wouldn't leave this kind of comment in my code, just placing my thoughts here
            5 5
            5 0
            0 0
            ada 0
            aa bb
            4 4 4
            5

            */

            //by default the Split method uses the space character as a delimiter
            //alternatively (in extreme cases due to performance), we can set up a Regex pattern to grab the first 2 numbers seperated by a space
            var rowsAndColumnsAsArray = input.Split();

            var tileToReturn = new Tile();

            if (rowsAndColumnsAsArray.Length >= 2)
            {
                //we try and parse the inputted string as integer. if it doesn't cast to an integer, we return 0
                int.TryParse(rowsAndColumnsAsArray[1], out int rowNumber);
                int.TryParse(rowsAndColumnsAsArray[0], out int columnNumber);

                //initialise the tile 
                tileToReturn = new Tile(rowNumber, columnNumber, 0);
            }

            return tileToReturn;
        }

        private string ParseInputAsTilesOrMines(string input, int gridWidth)
        {
            /* TEST CASES, assuming 4 columns;
            ....
            .*..
            4545
            454..4545..
            454..**45..
            */

            var resString = string.Join("", input.Where(x => x.Equals('.') || x.Equals('*')).ToArray());

            //we may need a stringbuilder to fill in missing tiles
            StringBuilder stringBuilderForParsedResults = new StringBuilder(resString);

            if (stringBuilderForParsedResults.Length < gridWidth)
            {
                //get how many tileswe need to add
                var numberOfTilesToAdd = gridWidth - stringBuilderForParsedResults.Length;

                for (int i = 0; i < numberOfTilesToAdd; i++)
                {
                    //add empty tiles
                    stringBuilderForParsedResults.Append(".");
                }
            }

            return stringBuilderForParsedResults.ToString();
        }


        

    }
}
