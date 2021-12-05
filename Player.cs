using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Player
    {
        public string Name;
        public bool IsPlaying = true;
        public int value;
        //TODO: public int money;
        public List<Card> Hand = new List<Card>();


        public Player()
        {
            AskName();
        }
        /// <summary>
        /// Function to ask the created players name
        /// </summary>
        /// <returns>player name<string></returns>
        protected virtual void AskName()
        {
            Console.Write("Player name: ");
            Name = Console.ReadLine();
            Console.Clear();
        }







        public void PlayHand()
        {
            while (IsPlaying)
            {
                AskMove();
                IsPlaying = false;
            }
        }
        public void AskMove()
        {
            Console.WriteLine($"Player: {Name}");
            Console.Write(FormatCardToText());
        }

        public string FormatCardToText()
        {
            string text = "";
            int i = 1;
            foreach (Card card in Hand)
            {
                if (card.Type == Card.CardType.Nummer)
                {
                    text += $"Kaart {i}: {Convert.ToString(card.Color)} {Convert.ToString(card.Number)} \n";
                }
                else
                {
                    text += $"Kaart {i}: {Convert.ToString(card.Color)} {Convert.ToString(card.Type)} \n";
                }
                i++;
            }
            return text;
        }





        /// <summary>
        /// method to pull a random active card from the card decks
        /// </summary>
        /// <param name="_isVisible">true = card visible; false = not visible(second dealer card)</param>
        /// <returns></returns>
        public Card PullCard(bool _isVisible, List<CardDeck> _deckList)
        {
            Random rnd = new Random();
            int activeCards = 0;
            int activeCardsIndex = 0;
            //card to return if the program fails
            Card failCard = new Card(9999, "Harte", "Aas");
            //loop through the list to get individual decks
            foreach (CardDeck deck in _deckList)
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
            foreach (CardDeck deck in _deckList)
            {
                //loop through the deck list to get all the individual cards; then check the status;
                foreach (Card card in deck.Deck)
                {
                    //checks if the card is active
                    if (card.Status)
                    {
                        //checks if the active card is at the random card index
                        if (activeCardsIndex == randomIndex)
                        {
                            //checks if the card has to be given visible or not
                            if (_isVisible)
                            {
                                Card returnCard = card;
                                card.Status = false;
                                return returnCard; //returns with status true; status false means hidden card for the dealer
                            }
                            else
                            {
                                card.Status = false;
                                return card; //returns with status false
                            }

                        }
                        //up the active card index
                        activeCardsIndex++;
                    }
                }
            }
            //this shouldn't be executed;
            return failCard;
        }
    }
}
