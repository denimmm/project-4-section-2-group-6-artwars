using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtWarsServer.Model
{
    class WritingPrompt : State
    {

        private Server server { get; set; }

        //should be null the first time it is ran
        public static List<Player> ?EligiblePrompters;

        public Player chosenPlayer;

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
            chosenPlayer = selectPlayerToWritePrompt();


        }


        public async Task Start()
        {
            //broadcast the prompt request to all players
            PromptPacket PromptRequest = new PromptPacket(server.code, "", chosenPlayer.ID);
            //send to all players
            await server.BroadcastToPlayers(PromptRequest);

            //receive the prompt from the user
            PromptPacket returnPacket = new PromptPacket(await chosenPlayer.ReceiveDataAsync());

            //get the prompt
            server.prompt = returnPacket.prompt;

            //update the packet with the new prompt
            PromptRequest.prompt = server.prompt;

            //send the prompt to all users
            await server.BroadcastToPlayers(PromptRequest);


            //go to the next state
            NextState();

        }

        public void NextState()
        {
            //make and assign new state to server
            server.state = new Drawing(server);

        }


        //choose a player for the prompt
        private Player selectPlayerToWritePrompt()
        {
            try
            {

                //choose a player from the list
                int index = new Random().Next(EligiblePrompters.Count - 1);

                Player p = EligiblePrompters[index];

                //remove it from the list
                EligiblePrompters.Remove(p);

                return p;

            }
            catch(Exception e)
            {
                Console.WriteLine($"Failed to select player to write prompt: {e}");
                return null;

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
