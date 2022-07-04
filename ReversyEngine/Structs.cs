using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversyEngine
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


        public static bool operator ==(Position pos1, Position pos2)
        {
            return pos1.X == pos2.X && pos1.Y == pos2.Y;
        }

        public static bool operator !=(Position pos1, Position pos2)
        {
            return pos1.X != pos2.X || pos1.Y != pos2.Y;
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }
    }

    public struct Line
    {
        public Position Start;
        public Position End;
    }

    public delegate bool IsCanClicked(Position position);
}
