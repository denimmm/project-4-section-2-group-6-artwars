using ArtWarsClientWPF.Models;
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
using System.Windows.Shapes;

namespace ArtWarsClientWPF
{
    /// <summary>
    /// Interaction logic for PromptWritingWindow.xaml
    /// </summary>
    public partial class PromptWritingWindow : Window
    {
        private Client _client;
        private TcpHandler _handler;

        public PromptWritingWindow(TcpHandler handler, Client client)
        {
            _handler = handler;
            _client = client;
            InitializeComponent();
        }

        private void SubmitPromptButton_Click(object sender, RoutedEventArgs e)
        {
            string prompt = PromptBox.Text;
            if (prompt != null)
            {
                //_handler.SendPacket(prompt);
                PromptWaitingWindow promptWaitingWindow = new PromptWaitingWindow(_handler, _client);
                promptWaitingWindow.Show();
            }
            else
            {

            }
        }
    }
}
