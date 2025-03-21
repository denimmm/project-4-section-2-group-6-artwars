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
        public WritingPromptPage()
        {
            InitializeComponent();
        }
        public void updatePlayerWritingThePrompt()
        {
            if (((App)Application.Current).server != null) // Check if server is initialized
            {
                //promptTextBox.Text = ((App)Application.Current).server.state.chsenPlayer.name + "is writing the prompt...";
                PlayerTextBox.Text = "Example Player is writing the prompt";
            }
            else
            {
                PlayerTextBox.Text = "OOPS. Something went wrong!";
            }


        }
}
