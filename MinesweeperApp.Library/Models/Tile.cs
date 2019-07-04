using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Library.Models
{
    public class Tile : Element
    {
        public Tile()
        {
        }

        public Tile(int row, int column, int mines)
        {
            RowNumber = row;
            ColumnNumber = column;
            NumberOfAdjacentMines = mines;
        }
    }
}
