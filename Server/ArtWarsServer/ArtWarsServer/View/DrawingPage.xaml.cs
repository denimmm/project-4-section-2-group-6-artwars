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
    /// Interaction logic for DrawingPage.xaml
    /// </summary>
    public partial class DrawingPage : Page
    {
        public DrawingPage()
        {
            InitializeComponent();
            this.DrawingPromptPage_UpdatePrompt();
        }
        public void DrawingPromptPage_UpdatePrompt()
        {
            if (((App)Application.Current).server != null) // Check if server is initialized
            {
                promptTextBox.Text = ((App)Application.Current).server.prompt;
            }
            else
            {
                promptTextBox.Text = "OOPS. Something went wrong!";
            }
        }
    }
}
