using ArtWarsServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for VotingPage.xaml
    /// </summary>
    public partial class VotingPage : Page
    {
        public class Artwork //Structure to hold image path and player name (Binded to the XAML file)
        {
            public string ImagePath { get; set; }
            public string PlayerName { get; set; }
        }

        public VotingPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Get all existing images (you might want to modify this path)
            var artworks = new List<Artwork>();
            Server server = ((App)Application.Current).server;  
            for (int i = 0; i<server.Players.Count; i++)
            {
                int playerId = server.Players[i].ID;
                string path = $"/View/Resources/Images/{playerId}.jpg"; //Change to another path if needed
                // In real application, check if file exists using File.Exists()
                artworks.Add(new Artwork
                {
                    ImagePath = path,// image path
                    PlayerName = server.Players[i].Name // player name
                });
            }


            var panel = (UniformGrid)ImagesItemsControl.ItemsPanel.LoadContent();
            if (artworks.Count > 2)// Calculating amount of Columns needed
            {
                panel.Columns = 2;
            }
            else
            {
                panel.Columns = 1;
            }
                
            panel.Rows = (artworks.Count + 1) / 2;// Calculating amount of Rows needed

            ImagesItemsControl.ItemsSource = artworks;
        }
    }
}
