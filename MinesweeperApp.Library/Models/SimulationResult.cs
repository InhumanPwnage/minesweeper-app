using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Library.Models
{
    public class SimulationResult : Simulation
    {

        /// <summary>
        /// Grid init.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public SimulationResult(int columnCount, int rowCount)
        {
            Height = rowCount;
            Width = columnCount;

            //initialise a jagged array of Tiles
            Tiles = new Tile[rowCount][];
            for (int i = 0; i < rowCount; i++)
            {
                Tiles[i] = new Tile[columnCount];
                for (int j = 0; j < columnCount; j++)
                {
                    Tiles[i][j] = new Tile(row: i, column: j, mines: 0);
                }
            }
        }

        /// <summary>
        /// Updates the Mine count of adjacent tiles via the passed Mine as parameter.
        /// </summary>
        /// <param name="tile">The position in the jagged array to use as Mine position.</param>
        public override void UpdateHintCountOfAdjacentTiles(Tile tile)
        {
            //west
            if (tile.ColumnNumber - 1 >= 0)
            {
                Tiles[tile.RowNumber][tile.ColumnNumber - 1].NumberOfAdjacentMines++;
            }

            //north-west
            if (tile.ColumnNumber - 1 >= 0 && tile.RowNumber - 1 >= 0)
            {
                Tiles[tile.RowNumber - 1][tile.ColumnNumber - 1].NumberOfAdjacentMines++;
            }

            //south-west
            if (tile.ColumnNumber - 1 >= 0 && tile.RowNumber + 1 < Height)
            {
                Tiles[tile.RowNumber + 1][tile.ColumnNumber - 1].NumberOfAdjacentMines++;
            }

            //east
            if (tile.ColumnNumber + 1 < Width)
            {
                Tiles[tile.RowNumber][tile.ColumnNumber + 1].NumberOfAdjacentMines++;
            }

            //north-east
            if (tile.ColumnNumber + 1 < Width && tile.RowNumber - 1 >= 0)
            {
                Tiles[tile.RowNumber - 1][tile.ColumnNumber + 1].NumberOfAdjacentMines++;
            }

            //south-east
            if (tile.ColumnNumber + 1 < Width && tile.RowNumber + 1 < Height)
            {
                Tiles[tile.RowNumber + 1][tile.ColumnNumber + 1].NumberOfAdjacentMines++;
            }

            //north
            if (tile.RowNumber - 1 >= 0)
            {
                Tiles[tile.RowNumber - 1][tile.ColumnNumber].NumberOfAdjacentMines++;
            }

            //south
            if (tile.RowNumber + 1 < Height)
            {
                Tiles[tile.RowNumber + 1][tile.ColumnNumber].NumberOfAdjacentMines++;
            }
        }
    }
}
