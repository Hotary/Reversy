﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReversy.ViewModels
{
    public class GameViewModel
    {
        private ReversyEngine.Core _core;

        public GameViewModel(ReversyEngine.Core core) 
        {
            _core = core;
        }

        public int Width => _core.CurrentSize.X;
        public int Height => _core.CurrentSize.Y;
    }
}
