using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReversy.Game
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
}
