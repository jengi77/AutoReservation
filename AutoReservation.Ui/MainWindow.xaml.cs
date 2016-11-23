using System.Windows;

namespace CarReservation.Ui
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(object viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
