using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Máš tam chybu", "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        switch (result)
        {
            case MessageBoxResult.OK:
                this.Close();
                break;
            case MessageBoxResult.Cancel:
                break;
            default: 
                break;
        }
    }
}