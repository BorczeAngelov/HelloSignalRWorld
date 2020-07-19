using System.Windows;

namespace HelloSignalRWorld.Client.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var mainWindowVM = new MainWindowVM();
            DataContext = mainWindowVM;
        }
    }
}
