using ArtWarsServer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;


namespace ArtWarsServer.Model
{
    class WritingPrompt : State
    {

        private Server server { get; set; }

        //should be null the first time it is ran
        public static List<Player> ?EligiblePrompters;

        public WritingPrompt(Server server)
        {
            this.server = server;

            EligiblePrompters = new List<Player>();

            //get eligable prompter
            if(EligiblePrompters.Count == 0)
            {
                EligiblePrompters = new List<Player>(server.Players);
            }
            //choose the player
            selectPlayerToWritePrompt();


        }


        public async Task Start()
        {   
            if(server.chosenPlayer == null || server.chosenPlayer.ClientSocket.Connected == false)
            {
                return;
            }

            //broadcast the prompt request to all players
            PromptPacket PromptRequest = new PromptPacket(server.code, "", server.chosenPlayer.ID);
            //send to all players
            await server.BroadcastToPlayers(PromptRequest);

            //receive the prompt from the user
            PromptPacket returnPacket = new PromptPacket(await server.chosenPlayer.ReceiveDataAsync());

            //get the prompt
            server.prompt = returnPacket.prompt;

            //update the packet with the new prompt
            PromptRequest.prompt = server.prompt;

            //send the prompt to all users
            await server.BroadcastToPlayers(PromptRequest);


            //Move to DrawingScreen
            server.MainFrame?.Navigate(new View.DrawingPage());
            //go to the next state
            NextState();

        }

        public void NextState()
        {
            //make and assign new state to server
            server.state = new Drawing(server);

        }


        //choose a player for the prompt
        private void selectPlayerToWritePrompt()
        {
            try
            {



                Player? p = null;
                while (p == null) {
                    //choose a player from the list
                    int index = new Random().Next(EligiblePrompters.Count - 1);

                    p = EligiblePrompters[index];
                    //remove it from the list
                    EligiblePrompters.Remove(p);
                }
                server.UpdatePrompter(p);

            }
            catch(Exception e)
            {
                Debug.WriteLine($"Failed to select player to write prompt: {e}");

            }

        }

        //reset the eligible players list once the game is complete
        public static void Reset()
        {
            if(EligiblePrompters != null)
            {
                EligiblePrompters.Clear();
            }
        }

    }
}
