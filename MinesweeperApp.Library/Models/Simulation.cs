using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Library.Models
{
    public abstract class Simulation
    {
        public Tile[][] Tiles;
        public int Width { get;  set; }
        public int Height { get;  set; }

        public abstract void UpdateHintCountOfAdjacentTiles(Tile mine);

        #region Indexers

        public Tile this[int index1, int index2]
        {
            get { return Tiles[index1][index2]; }
            set { Tiles[index1][index2] = value; }
        }

        public Tile this[Tile tile]
        {
            get { return Tiles[tile.RowNumber][tile.ColumnNumber]; }
            set { Tiles[tile.RowNumber][tile.ColumnNumber] = value; }
        }

        #endregion
    }
}
