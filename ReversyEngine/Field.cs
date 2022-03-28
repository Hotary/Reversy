using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversyEngine
{
    public class Field : IEnumerable<Cell>
    {
        public SizeField Size { get; private set; }
        public Cell[,] Cells { get; private set; }  // First dimension - X; second dimension - Y;\
        internal int EmptyCells { get; set; }

        public Field(SizeField size, IsCanClicked isCanClicked, Action<Cell> _action)
        {
            Size = size;
            EmptyCells = size.X * size.Y;
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
        public Cell this[int x, int y] => Cells[x, y];


        public IEnumerator<Cell> GetEnumerator() => new FieldEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => new FieldEnumerator(this);



        protected class FieldEnumerator : IEnumerator<Cell>
        {
            private Field _field;
            private int posX, posY;

            public Cell Current { get; private set; }
            object IEnumerator.Current => Current;

            public FieldEnumerator(Field field)
            {
                _field = field;
                Current = field[0, 0];
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                posY++;
                if (posY >= _field.Size.Y)
                {
                    posY = 0;
                    posX++;
                }
                if (posX >= _field.Size.X)
                    return false;
                Current = _field[posX, posY];
                return true;
            }

            public void Reset()
            {
                posX = 0; posY = 0;
                Current = _field[posX, posY];
            }
        }
    }
}
