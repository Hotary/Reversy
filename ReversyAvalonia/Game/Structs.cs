using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReversy.Game
{
    public enum CoreState
    {
        Move,
        Waiting,
        GameOver
    }

    public enum Player
    {
        None = 0,
        Player1 = 1,
        Player2 = 2
    }

    public struct SizeField
    {
        public int X;
        public int Y;
    }

    public struct Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x; Y = y;
        }
    }

    public struct Line
    {
        public Position Point1;
        public Position Point2;
    }

    public delegate bool IsCanClicked(Position position);
}
