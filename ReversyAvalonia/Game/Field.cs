using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReversy.Game
{
    public class Field
    {
        public SizeField Size { get; private set; }
        public Cell[,] Cells { get; private set; }  // First dimension - X; second dimension - Y;

        public Field(SizeField size, IsCanClicked isCanClicked, Action<Cell> _action)
        {
            Size = size;
            Cells = new Cell[size.X, size.Y];
            for (int x = 0; x < size.X; x++)
                for (int y = 0; y < size.Y; y++)
                    Cells[x, y] = new Cell(x, y)
                    {
                        IsCanClicked = isCanClicked,
                        action = _action
                    };
        }

        public Cell GetCell(Position position) => Cells[position.X, position.Y];

        public Cell this[int x, int y]
        {
            get
            {
                return Cells[x, y];
            }
        }

    }
}
