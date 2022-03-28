
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia;
using Avalonia.Media;
using Avalonia.Data;

namespace AvaloniaReversy.Views
{
    public partial class MainWindow : Window
    {
        private Game.Core _core;

        public string StartPattern => @"00000000|
                                            00000000|
                                            00000000|
                                            00012000|
                                            00021000|
                                            00000000|
                                            00000000|
                                            00000000";

        public MainWindow()
        {
            InitializeComponent();

            _core = new Game.Core("#FFFFFF", "#000000", new Game.SizeField() { X = 8, Y = 8 }, StartPattern)
            {
                Finder = new Game.LineFinder()
            };
            CoreControl = this.FindControl<GameControl>("CoreControl");
            CoreControl.Width = _core.CurrentSize.X;
            CoreControl.Height = _core.CurrentSize.Y;

            //В даьнейшем добавить итератор
            for (var x = 0; x < _core.CurrentSize.X; x++)
                for (var y = 0; y < _core.CurrentSize.Y; y++) 
                {
                    var cell = new ViewModels.CellViewModel(_core.Field[x, y]);
                    var control = new CellControl()
                    {
                        DataContext = cell,
                        IsCanClicked = cell.IsCanClicked,
                    };
                    control.PointerPressed += (s, args) => { cell.Action(); };
                    CoreControl.AddChip(control, x, y);
                }
                    
        }
    }
}
