using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Table
    {
        private List<Player> playerList = new List<Player>();
        private List<CardDeck> deckList = new List<CardDeck>();
        private House house = new House();

        public Table(Settings _settings)
        {
            TableInit(_settings);
            //while (true)
            //{
            TableRound();
            //}

        }
        /// <summary>
        /// function to initialize the table
        /// </summary>
        /// <param name="_Settings"></param>
        private void TableInit(Settings _Settings)
        {
            //loop for the amount of players given and add them to the player list
            for (int i = 0; i < _Settings.Players; i++)
            {
                playerList.Add(new Player());
            }
            //loop for the amount of card decks given and add them to the deck list
            for (int i = 0; i < _Settings.Decks; i++)
            {
                deckList.Add(new CardDeck());
            }
        }

        /// <summary>
        /// the method that runs every round
        /// </summary>
        private void TableRound()
        {
            DealCards();

            foreach (Player player in playerList)
            {
                player.PlayHand(deckList, house.Hand[0]);
            }


        }

        private void DealCards()
        {
            //add 2 starting cards for each player
            foreach (Player player in playerList)
            {
                player.Hand.Add(player.PullCard(true, deckList));
                player.Hand.Add(player.PullCard(true, deckList));
            }
            //add 2 starting cards for the house, false paramater is for hidden card
            house.Hand.Add(house.PullCard(true, deckList));
            house.Hand.Add(house.PullCard(false, deckList));
        }

    }
}
