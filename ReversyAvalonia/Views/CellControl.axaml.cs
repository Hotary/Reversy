using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace AvaloniaReversy.Views
{
    public delegate bool IsCanClicked();

    public partial class CellControl : UserControl
    {
        private static BrushConverter _brushConverter = new Avalonia.Media.BrushConverter();
        private static Brush pointerEnterBrush = (Brush)_brushConverter.ConvertFromString("#4a44ff");
        private static Brush pointerLeaveBrush = (Brush)_brushConverter.ConvertFromString("#00000000");

        public CellControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Panel = this.FindControl<Panel>("Panel");
            Ellipse = this.FindControl<Ellipse>("Ellipse");
            Panel.Background = pointerLeaveBrush;
        }

        public static readonly StyledProperty<IsCanClicked> IsCanClickedProperty = AvaloniaProperty.Register<CellControl, IsCanClicked>(nameof(IsCanClicked));
        public IsCanClicked IsCanClicked { get; set; }

        private void PointerEnterHandler(object sender, PointerEventArgs e)
        {
            if (IsCanClicked is not null)
                if (IsCanClicked())
                    Panel.Background = pointerEnterBrush;
        }
        private void PointerLeaveHandler(object sender, PointerEventArgs e)
        {
            Panel.Background = pointerLeaveBrush;
        }
    }
}
