using System;
using System.Collections.Generic;
using System.Text;

namespace ReversyEngine
{
    class CellHelper : Cell
    {
        public CellHelper(int x, int y, Action<Cell> a) : base(x, y)
        {
            action = a;
        }
    }
}
