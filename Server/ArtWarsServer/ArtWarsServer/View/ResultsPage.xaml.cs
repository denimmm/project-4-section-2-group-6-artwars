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
using System.IO;
using Path = System.IO.Path; // To avoid confusion with System.Windows.Shapes.Path


namespace ArtWarsServer.View
{
    /// <summary>
    /// Interaction logic for ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {   public class Winner
        {
            public string Name { get; set; }

            public int ID { get; set; }
            public string Path { get; set; }
        }
        public ResultsPage()
        {
            InitializeComponent();
            
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Get server instance
            Server server = ((App)Application.Current).server;
            Winner winner = new Winner();
            // Get winner information (replace with your actual winner logic)
            var player = server.Players[0]; // Implement this method in your server class
            string imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images"); // Change directory here if needed // Currently configured to Access Images folder in Base directory (Debug)
            string path = Path.Combine(imagesDir, $"{player.ID}.jpg");

            if (!File.Exists(path))
            {
                path = $"/View/Resources/NotFound.jpg";
            }
            // Set winner name
            winner = new Winner
            {
                Name = player.Name,
                ID = player.ID,
                Path = path
            };
            DataContext = winner; // Set DataContext to the winner object

            // Set image path using pack URI format
        }
    }
}
