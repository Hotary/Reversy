using System;
using System.Collections.Generic;
using System.Text;

namespace ReversyEngine
{
    public class CoreComputerEnemy : Core
    {
        private ComputerEnemy ce;

        public CoreComputerEnemy(CoreInit args) : base(args)
        {
            ce = new ComputerEnemy(this);
        }

        public override bool IsCanClicked(Position position)
        {
            //if (CurrentPlayer != Player.Player1)
            //    return false;
            return base.IsCanClicked(position);
        }
    }
}
