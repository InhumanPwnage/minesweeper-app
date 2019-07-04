using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Library.Models
{
    public class Mine : Tile
    {
        public Mine(int row, int column, int mines) 
            : base(row, column, mines)
        {
            RowNumber = row;
            ColumnNumber = column;
        }
    }
}
