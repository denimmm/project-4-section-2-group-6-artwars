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
    /// Interaction logic for WritingPromptPage.xaml
    /// </summary>
    public partial class WritingPromptPage : Page
    {
        Server server;

        public WritingPromptPage()
        {
            InitializeComponent();

            server = ((App)Application.Current).server;
            this.Loaded += Start;
        }
        
        public void Start(object sender, System.Windows.RoutedEventArgs e)
        {
            updatePlayerWritingThePrompt();

        }

        public void updatePlayerWritingThePrompt()
        {
            if (server != null && server.chosenPlayer != null) // Check if server is initialized
            {
                PlayerTextBox.Text = $"Waiting for {server.chosenPlayer.Name} to write the prompt";
            }
            else
            {
                PlayerTextBox.Text = "OOPS. Something went wrong!";
            }


        }
    }
}
