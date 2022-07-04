
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia;
using Avalonia.Media;
using Avalonia.Data;

namespace AvaloniaReversy.Views
{
    public partial class MainWindow : Window
    {
        class Params : ReversyEngine.CoreInit
        {
            public string ColorPlayer1 => "#FFFFFF";

            public string ColorPlayer2 => "#000000";

            public ReversyEngine.SizeField Size => new ReversyEngine.SizeField() { X = 8, Y = 8 };

            public string StartPattern => @"00000000|
                                            00000000|
                                            00000000|
                                            00012000|
                                            00021000|
                                            00000000|
                                            00000000|
                                            00000000";
        }

        private ReversyEngine.Core _core;

        public MainWindow()
        {
            InitializeComponent();
            _core = new ReversyEngine.Core(new Params())
            {
                Finder = new ReversyEngine.LineFinder()
            };
            CoreControl = this.FindControl<GameControl>("CoreControl");
            CoreControl.Width = _core.CurrentSize.X;
            CoreControl.Height = _core.CurrentSize.Y;

            _core.StateChanged = (core) => 
            {
                if (core.State == ReversyEngine.CoreState.Move)
                    core.NextState();
            };

            foreach (var cell in _core.Field)
            {
                var vm = new ViewModels.CellViewModel(cell);
                var control = new CellControl()
                {
                    DataContext = vm,
                    IsCanClicked = vm.IsCanClicked,
                };
                control.PointerPressed += (s, args) => 
                {
                    if (_core.State == ReversyEngine.CoreState.Waiting)
                    {
                        cell.Action();
                        _core.NextState();
                    }
                };
                CoreControl.AddChip(control, vm.X, vm.Y);
            }           
        }

        private void UpdateMoving() 
        {

        }
    }
}
