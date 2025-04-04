using ArtWarsServer.Model;
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

namespace ArtWarsServer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Server server;

    public MainWindow()
    {
        InitializeComponent();;
        server = ((App)Application.Current).server;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {

        MainFrame.Navigate(new View.ConnectingPage());// Navigating to the connecting page

        server.MainFrame = MainFrame;
       
    }

}