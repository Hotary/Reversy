using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace AvaloniaReversy.Views
{
    public partial class GameControl : UserControl
    {
        public GameControl()
        {
         
            InitializeComponent();
            
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Width = Width;
        }

        public static readonly StyledProperty<int> WidthProperty = AvaloniaProperty.Register<GameControl, int>(nameof(Width));

        private int _width;
        public int Width 
        {
            get => _width;
            set
            {
                var grid = this.FindControl<Grid>("FieldGrid");
                grid.ColumnDefinitions.Clear();
                for (int i = 0; i < value; i++)
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        public static readonly StyledProperty<int> HeightProperty = AvaloniaProperty.Register<GameControl, int>(nameof(Height));

        private int _height;
        public int Height
        {
            get => _height;
            set
            {
                var grid = this.FindControl<Grid>("FieldGrid");
                grid.RowDefinitions.Clear();
                for (int i = 0; i < value; i++)
                    grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        public void AddChip(Control item, int x, int y) 
        {
            var grid = this.FindControl<Grid>("FieldGrid");
            Grid.SetRow(item, y);
            Grid.SetColumn(item, x);
            grid.Children.Add(item);
        }
    }
}
