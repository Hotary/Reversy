using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReversy.Game
{

    public delegate void CoreStateChanged(Core core);

    public class Core
    {
        public SizeField CurrentSize => Field.Size;
        public Field Field { get; private set; }
        public Player CurrentPlayer { get; private set; } = Player.Player1;
        //public CoreState State { get; private set; }
        public Dictionary<Player, Chip> Chips { get; private set; }
        public Dictionary<Player, int> CountChips { get; private set; } = new Dictionary<Player, int>(0);
        public ILineFinder Finder { get; set; }

        public CoreStateChanged StateChanged { get; private set; }

        public Core(string cP1, string cP2, SizeField size, string p)
        {
            Field = new Field(size, IsCanClicked, ClickedCell);
            Chips = new Dictionary<Player, Chip>()
            {
                { Player.Player1, new Chip(Player.Player1, cP1) },
                { Player.Player2, new Chip(Player.Player2, cP2) }
            };

            int[,] pattern = new int[CurrentSize.X, CurrentSize.Y];  // First dimension - X; second dimension - Y;

            string[] splitted = p.Split('|');
            for (int x = 0; x < CurrentSize.X; x++)
            {
                var trimmed = splitted[x].Trim();
                for (int y = 0; y < CurrentSize.Y; y++)
                    pattern[x, y] = trimmed[y] - '0';
            }

            for (int x = 0; x < CurrentSize.X; x++)
                for (int y = 0; y < CurrentSize.Y; y++)
                {
                    var player = (Player)pattern[x, y];
                    if (player == Player.Player1 || player == Player.Player2)
                        Field[x, y].Chip = Chips[player];
                }
        }

        //public void PseudoTick()
        //{
        //    var last = State;

        //    switch (State)
        //    {
        //        case CoreState.Waiting:
        //            break;
        //        case CoreState.Move:
        //            Move();
        //            break;
        //        case CoreState.GameOver:
        //            break;
        //    }

        //    if (last != State)
        //        StateChanged?.Invoke(this);
        //}

        private void GoCountChips()
        {
            foreach (var kv in CountChips)
                CountChips[kv.Key] = 0;

            for (int x = 0; x < Field.Size.X; x++)
                for (int y = 0; y < Field.Size.Y; y++)
                    if (Field[x, y].Chip is not null)
                        CountChips[Field[x, y].Chip.Player]++;
        }


        public bool IsCanClicked(Position position)
        {
            var cell = Field.GetCell(position);
            if (cell.Chip is not null) return false;

            var list = Finder.Search(Field, CurrentPlayer, position);

            return list is not null;
        }

        public void ClickedCell(Cell cell) 
        {
            if (cell.Chip is not null) return;

            var list = Finder.Search(Field, CurrentPlayer, cell.Position);

            if (list is null)
                return;

            for (int i = 0; i < 8 && list[i] is not null; i++) 
            {
                var line = list[i];
                var dX = (int)(line?.Point1.X - line?.Point2.X);
                var dY = (int)(line?.Point1.Y - line?.Point2.Y);
                var stepX = 0;
                var stepY = 0;
                var step = 0;
                if(dX != 0)
                {
                    stepX = dX / Math.Abs(dX);
                    step = dX / stepX;
                }
                if (dY != 0)
                {
                    stepY = dY / Math.Abs(dY);
                    step = dY / stepY;
                }
                for (int j = 0; j < step; j++) 
                {
                    var _cell = Field[(int)line?.Point1.X - stepX * j, (int)line?.Point1.Y - stepY * j];
                    _cell.Chip = Chips[CurrentPlayer];
                    _cell.UpdateStatus();
                }
            }

            switch (CurrentPlayer) 
            {
                case Player.Player1:
                    CurrentPlayer = Player.Player2;
                    break;
                case Player.Player2:
                    CurrentPlayer = Player.Player1;
                    break;
            }
        }
    }
}
