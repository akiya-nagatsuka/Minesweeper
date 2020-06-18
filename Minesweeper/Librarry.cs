using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Minesweeper
{
    class Cell
    {
        public int cellRow;
        public int cellColumn;
        public bool isOpen;
        public bool haveBomb;
        public bool haveSettedFlag;
        public int Status;
        public Cell(int row, int col)
        {
            cellRow = row;
            cellColumn = col; 
        }
    }
}
