using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReversy.Game
{
    public class Cell
    {
        public IsCanClicked IsCanClicked { get; protected internal set; }
        public Action<Cell> action { get; protected internal set; }
        public Position Position { get; private set; }
        public Chip? Chip { get; set; }
        public Action UpdateStatus { get; protected internal set; }

        public Cell(Position position)
        {
            Position = position;
        }

        public Cell(int x, int y)
        {
            Position = new Position(x, y);
        }

        public void Action() 
        {
            action?.Invoke(this);
        }
    }
}
