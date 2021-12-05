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
        /// <param name="_settings"></param>
        private void TableInit(Settings _settings)
        {
            //loop for the amount of players given and add them to the player list
            for (int i = 0; i < _settings.Players; i++)
            {
                playerList.Add(new Player());
            }
            //loop for the amount of card decks given and add them to the deck list
            for (int i = 0; i < _settings.Decks; i++)
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
        }

        private void DealCards()
        {
            foreach (Player player in playerList)
            {
                player.Hand.Add(PullCard());
                player.Hand.Add(PullCard());
                Console.WriteLine(player.Hand[0].Number + " " + player.Hand[0].Type);
                Console.WriteLine(player.Hand[1].Number + " " + player.Hand[0].Type);
            }
        }
        /// <summary>
        /// method to randomly pull a active card from the card decks
        /// </summary>
        /// <returns>card object</returns>
        private Card PullCard()
        {
            Random rnd = new Random();
            int activeCards = 0;
            int activeCardsIndex = 0;
            //card to return if the program fails
            Card failCard = new Card(99, "Harte", "Aas");
            //loop through the list to get individual decks
            foreach (CardDeck deck in deckList)
            {
                //loop through the deck list to get all the individual cards; then check the status;
                foreach (Card card in deck.Deck)
                {
                    if (card.Status)
                    {
                        activeCards++;
                    }
                }
            }
            //generate randum number based on the amount of active cards
            int randomIndex = rnd.Next(0, activeCards);

            //loop through the decks; pulls card out of the list based on the index of the active cards, not all cards;
            foreach (CardDeck deck in deckList)
            {
                //loop through the deck list to get all the individual cards; then check the status;
                foreach (Card card in deck.Deck)
                {
                    if (card.Status)
                    {
                        if (activeCardsIndex == randomIndex)
                        {
                            return card;
                        }
                        activeCardsIndex++;
                    }
                }
            }
            //this shouldn't be executed;
            return failCard;

        }



    }
}
