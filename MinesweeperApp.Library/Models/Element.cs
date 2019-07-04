using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Library.Models
{
    public abstract class Element
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public int NumberOfAdjacentMines { get; set; }
    }
}
