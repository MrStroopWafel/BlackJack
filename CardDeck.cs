using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class CardDeck
    {
        public List<Card> Deck = new List<Card>();

        public CardDeck()
        {
            CardData Data1 = new CardData();
            MakeDeck(Data1.Data);
        }
        /// <summary>
        /// Function that loops though the Card data and makes a list of card objects
        /// </summary>
        /// <param name="_Data1"></param>
        /// <returns>card list<list/Card></returns>
        private void MakeDeck(Object[][] _Data1)
        {
            //loop though jagged array to make card object and add them to the list
            foreach (Object[] card in _Data1)
            {
                Deck.Add(new Card(Convert.ToInt32(card[0]), Convert.ToString(card[1]), Convert.ToString(card[2])));
            }
        }
        /// <summary>
        /// method to pull a random active card from the card decks
        /// </summary>
        /// <param name="_isVisible">true = card visible; false = not visible(second dealer card)</param>
        /// <returns></returns>
        public Card PullCard(List<CardDeck> _deckList)
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
                            Card returnCard = card;
                            card.Status = false;
                            return returnCard; //returns with status true; status false means hidden card for the dealer
                        }
                        //up the active card index
                        activeCardsIndex++;
                    }
                }   
                //checks if there are still active cards left, otherwise shuffle deck
                
            }
            //deck is empty
            if (activeCards == 0)
            {
                return failCard;
            }
            //this shouldn't be executed;
            return failCard;
        }
        public void Shuffle(List<CardDeck> _DeckList)
        {
            foreach (CardDeck CardDeck in _DeckList)
            {
                foreach (Card Card in CardDeck.Deck)
                {
                    Card.Status = true;
                }
            }
        }
    }
}
