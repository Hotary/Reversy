using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversyEngine
{
    public class Chip
    {
        public Player Player { get; private set; }
        public string Color { get; private set; }

        public Chip(Player player, string color)
        {
            Player = player; Color = color;
        }
    }

    public class ChipNullable : Chip
    {
        private ChipNullable _singleton;
        private ChipNullable() : base(Player.None, "") { }
        public ChipNullable Get 
        {
            get 
            {
                if (_singleton is null)
                    _singleton = new ChipNullable();
                return _singleton;
            }
        }
    }
}
