using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversyEngine
{
    public interface CoreInit
    {
        string ColorPlayer1 { get; }
        string ColorPlayer2 { get; }
        SizeField Size { get; }
        string StartPattern { get; }
    }

    public delegate void CoreStateChanged(Core core);
    public delegate void MovingHandler(Core core, IEnumerable<Cell> cells);

    public class Core
    {
        public SizeField CurrentSize => Field.Size;
        public Field Field { get; private set; }
        public Player CurrentPlayer { get; private set; } = Player.Player1;
        public CoreState State { get; private set; } = CoreState.Waiting;
        public CoreStateChanged StateChanged { get; set; }
        public MovingHandler MovingHandler { get; set; }
        public Dictionary<Player, Chip> Chips { get; private set; }
        public Dictionary<Player, int> CountChips { get; private set; }
        public ILineFinder Finder { get; set; }

        private List<Cell> changedCells;

        public Core(CoreInit args)
        {
            Field = new Field(args.Size, IsCanClicked, ClickedCell);
            Chips = new Dictionary<Player, Chip>()
            {
                { Player.Player1, new Chip(Player.Player1, args.ColorPlayer1) },
                { Player.Player2, new Chip(Player.Player2, args.ColorPlayer2) }
            };
            CountChips = new Dictionary<Player, int>()
            {
                {Player.Player1, 0},
                {Player.Player2, 0}
            };

            int[,] pattern = new int[CurrentSize.X, CurrentSize.Y];  // First dimension - X; second dimension - Y;

            string[] splitted = args.StartPattern.Split('|');
            for (int y = 0; y < CurrentSize.Y; y++)
            {
                var trimmed = splitted[y].Trim();
                    for (int x = 0; x < CurrentSize.X; x++)
                        pattern[x, y] = trimmed[x] - '0';
            }

            for (int x = 0; x < CurrentSize.X; x++)
                for (int y = 0; y < CurrentSize.Y; y++)
                {
                    var player = (Player)pattern[x, y];
                    if (player == Player.Player1 || player == Player.Player2)
                    {
                        Field[x, y].Chip = Chips[player];
                        CountChips[player]++;
                        Field.EmptyCells--;
                    }
                }
        }

        public void NextState()
        {
            switch (State)
            {
                case CoreState.Waiting:
                    State = CoreState.Move;
                    StartCountChips();
                    MovingHandler?.Invoke(this, changedCells);
                    break;
                case CoreState.Move:
                    if (IsGameOver)
                        State = CoreState.GameOver;
                    else
                    {
                        State = CoreState.Waiting;
                        ChangePlayer();
                        if (!IsCanMoveSomething) ChangePlayer();
                    }
                    break;
                case CoreState.GameOver:
                    break;
            }

            StateChanged?.Invoke(this);
        }

        private bool IsGameOver => Field.EmptyCells <= 0;

        private void ChangePlayer()
        {
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

        private void StartCountChips()
        {
            CountChips[Player.Player1] = 0;
            CountChips[Player.Player2] = 0;

            for (int x = 0; x < Field.Size.X; x++)
                for (int y = 0; y < Field.Size.Y; y++)
                    if (Field[x, y].Chip is object)
                        CountChips[Field[x, y].Chip.Player]++;
        }

        public bool IsCanMoveSomething
        {
            get
            {
                foreach (var cell in Field)
                    if (IsCanClicked(cell.Position))
                        return true;
                return false;
            }
        }

        public bool IsCanClicked(Position position)
        {
            var cell = Field.GetCell(position);
            if (cell.Chip is object) return false;

            var list = Finder.Search(Field, CurrentPlayer, position);

            return list is object;
        }

        public void ClickedCell(Cell cell)
        {
            if (cell.Chip is object) return;

            var list = Finder.Search(Field, CurrentPlayer, cell.Position);

            if (list is null)
                return;

            changedCells = new List<Cell>();
            for (int i = 0; i < 8 && list[i] is object; i++)
            {
                var line = list[i];
                var dX = (int)(line?.Start.X - line?.End.X);
                var dY = (int)(line?.Start.Y - line?.End.Y);
                var stepX = 0;
                var stepY = 0;
                var step = 0;
                if (dX != 0)
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
                    var _cell = Field[(int)line?.Start.X - stepX * j, (int)line?.Start.Y - stepY * j];
                    _cell.Chip = Chips[CurrentPlayer];
                    changedCells.Add(_cell);
                    _cell.UpdateStatus?.Invoke();
                }
            }

        }
    }
}
