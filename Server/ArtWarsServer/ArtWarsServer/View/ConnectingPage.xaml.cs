using ArtWarsServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArtWarsServer.View
{
    /// <summary>
    /// Interaction logic for ConnectingPage.xaml
    /// </summary>
    public partial class ConnectingPage : Page
    {
        //get the server
        Server server;
        

        public ConnectingPage()
        {
            InitializeComponent();
            server = ((App)Application.Current).server;
        }
        private void ConnectingPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (((App)Application.Current).server != null) // Check if server is initialized
            {// Setting the IP address and session code for the connecting page
                ip_textbox.Text = ((App)Application.Current).server.IP_Address;// IP address
                session_code_box.Text = ((App)Application.Current).server.code.ToString();// Session code
            }
            else
            {// If server is not initialized, display error messages
                ip_textbox.Text = "OOPS. Something went wrong!";
                session_code_box.Text = "OOPS. Something went wrong!";
            }
        }

        private void start_button_click(object sender, RoutedEventArgs e)
        {
            server.MainFrame?.Navigate(new View.WritingPromptPage());
            server.state.NextState();
        }

    }
}
