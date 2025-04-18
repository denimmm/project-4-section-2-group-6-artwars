﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;



namespace ArtWarsServer.Model
{



    public class Server
    {
        public string code;
        public string IP_Address;

        //list of players in the game
        public List<Player> Players { get; private set; }
        //current state of the game
        public State state { get; set; }
        //last player id
        public int PlayerID_Index { get; set; } //this is used to keep track of the last issued player id

        public int CurrentRound;

        //prompt for the round
        public string prompt;
        //player chosen to write prompt
        public Player ?chosenPlayer;

        //winning player
        public int winner;
        public Frame ?MainFrame {  get; set; }


        //events
        public event EventHandler PrompterChosen;

        //server config settings
        public ServerConfig serverConfig {get; private set;}
        
            public Server()
            {

            //initialize players list
            Players = new List<Player>();

            PlayerID_Index = 0;

            prompt = "PROMPT NOT SET";

            serverConfig = new ServerConfig();

            IP_Address = GetIPAddress();

            //code = MakeRoomCode();
            code = "1234";

            CurrentRound = 0;

            chosenPlayer = null;

            //initialize state
            state = new Connecting(this);



        }

        public void AddPlayer(Player player)
        {
            //assign an id to the new player
            PlayerID_Index++;
            player.ID = PlayerID_Index;

            //add player to the list of players
            Players.Add(player);
        }

        public void RemovePlayer(Player p)
        {
            Players.Remove(p);
        }

        //returns the server's ip address
        private string GetIPAddress()
		{
            string hostName = Dns.GetHostName();
            var host = Dns.GetHostEntry(hostName);
            //Old version: Returnd Ipv6 address
            //string IP = Dns.GetHostEntry(hostName).AddressList[0].ToString();

            //New version: Return Ipv4 address
            IPAddress[] Addresses = host.AddressList;
            foreach (IPAddress ip in Addresses)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        //makes a new room code with a random number
        public string MakeRoomCode()
        {
            Random random = new Random();

            //trim to 4 digits or whatever ROOM_CODE_MAX_NUMBER is
            return (random.Next() % serverConfig.ROOM_CODE_MAX_NUMBER).ToString("D4");
        }

        public bool verifyRoomCode(string roomCode)
        {
            if (this.code == roomCode) return true;

            return false;
        }


        public void Start()
        {
            //Player testPlayer = new Player(new TcpClient(), this);
            //testPlayer.Name = "Denim";
            //AddPlayer(testPlayer);


            state.Start();

        }

        public void Stop()
        {



        }

        public void ResetGame()
        {
            //reset the states
            Connecting.Reset();
            WritingPrompt.Reset();
            Drawing.Reset();
            Voting.Reset();
            Results.Reset();

        }

        //start the next round of the game
        public void nextRound()
        {
            

        }

        //send a message to all players
        public async Task BroadcastToPlayers(Packet packet)
        {

            try
            {
                // Send data to all players asynchronously and await all tasks
                var sendTasks = Players.Select(p => p.sendDataAsync(packet));
                await Task.WhenAll(sendTasks);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error broadcasting packet: {ex.Message}");
            }
        }


        public void UpdatePrompter(Player p)
        {

            chosenPlayer = p; 

            PrompterChosen?.Invoke(this, EventArgs.Empty);
        }
    }
}