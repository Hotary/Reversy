using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace AvaloniaReversy.ViewModels
{
    public class CellViewModel: ViewModelBase
    {
        private static Avalonia.Media.BrushConverter converter = new Avalonia.Media.BrushConverter();
        ReversyEngine.Cell _cell;

        public CellViewModel(ReversyEngine.Cell cell) 
        {
            _cell = cell;
            _cell.UpdateStatus += Update;
        }

        public Views.IsCanClicked IsCanClicked
        {
            get => isCanClicked;
        }

        public Brush Color
        {
            get
            {
                if (_cell.Chip is not null)
                    return (Brush)converter.ConvertFromString(_cell.Chip.Color);
                return null;
            }
        }

        public int X => _cell.Position.X;
        public int Y => _cell.Position.Y;

        private bool isCanClicked() => _cell.IsCanClicked(_cell.Position);

        public void Action() => _cell.Action();

        private void Update() 
        {
            this.RaisePropertyChanged("Color");
        }
    }
}
