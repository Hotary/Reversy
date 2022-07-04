using System;
using System.Collections.Generic;
using System.Text;

namespace ReversyEngine
{
    public class ComputerEnemy
    {
        private CoreComputerEnemy _core;

        public ComputerEnemy(CoreComputerEnemy core) 
        {
            _core = core;
            _core.StateChanged += StateChanched;
        }

        private void StateChanched(Core core) 
        {
            if (core.CurrentPlayer == Player.Player2 && core.State == CoreState.Waiting)
            {
                Cell maxCell = null; var Max = 0;
                foreach (var cell in core.Field) 
                {
                    if (cell.Chip is null) 
                    {
                        var lines = core.Finder.Search(core.Field, core.CurrentPlayer, cell.Position);
                        if (lines is null) continue;
                        int i = 0;
                        foreach (var l in lines) 
                        {
                            i++;
                            if (l is null) break;
                        }
                        if (i >= Max) 
                        {
                            Max = i;
                            maxCell = cell;
                        }
                    }
                }
                if (!(maxCell is null))
                    core.ClickedCell(maxCell);
                core.NextState();
            }
        }
    }
}
