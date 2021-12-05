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
        public int Value, Money, HandMoney;
        public List<Card> Hand = new List<Card>();
        protected List<CardDeck> deckList;
        private Card revealedDealerCard;


        public Player(int _Money)
        {
            Money = _Money;
            AskName();
        }

        /// <summary>
        /// Method to ask the created players name
        /// </summary>
        /// <returns>player name<string></returns>
        protected virtual void AskName()
        {
            Console.Write("Player name: ");
            Name = Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Method that loops till the play has made all moves
        /// the method checks for blackjack (21); if the payer goes over 21;
        /// </summary>
        /// <param name="_DeckList">the decklist is given so Hit() can pull a card from the deck</param>
        /// <param name="_RevealedDealerCard">revealed dealer card is given to display on screen (console)</param>
        public void PlayHand(List<CardDeck> _DeckList, Card _RevealedDealerCard)
        {
            revealedDealerCard = _RevealedDealerCard;
            deckList = _DeckList;
            while (IsPlaying)
            {
                switch (CalculateValue())
                {
                    case 21:
                        BlackJack();
                        IsPlaying = false;
                        break;
                    case > 21:
                        Over21();
                        IsPlaying = false;
                        break;
                    default:
                        AskMove();
                        break;
                }
            }
        }
        /// <summary>
        /// method asks for input, this can be: Hit; Stand; Double;
        /// and runs those functions
        /// </summary>
        private void AskMove()
        {
            Console.WriteLine($"Dealer Card value: {revealedDealerCard.Number}");
            Console.WriteLine($"Dealer Card 1: {revealedDealerCard.Color} {(revealedDealerCard.Type == Card.CardType.Nummer ? revealedDealerCard.Number : revealedDealerCard.Type)} \n\n");
            Console.WriteLine($"Player: {Name} \nMoney: {Money} \nBetted money: {HandMoney} \nCard value: {CalculateValue()}");
            Console.Write(FormatCardToText());
            Console.WriteLine("\n\n");
            Console.WriteLine("Kies een van de volgende opties door ze te tiepen:\n\n");
            Console.WriteLine("Hit");
            Console.WriteLine("Stand");
            Console.WriteLine("Double\n");

            switch (Console.ReadLine())
            {
                case "Hit":
                    Console.Clear();
                    Hit();
                    break;
                case "Stand":
                    IsPlaying = false;
                    Console.Clear();
                    break;
                case "Double":
                    IsPlaying = false;
                    Console.Clear();
                    Double();
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
        /// <summary>
        /// method to display text on screen when the players goes over 21 
        /// </summary>
        private void Over21()
        {
            Console.WriteLine($"Player: {Name} \nCard value: {CalculateValue()}");
            Console.Write(FormatCardToText());
            Console.WriteLine("\n\n");
            Console.WriteLine("Helaas over 21! Hit enter om door te gaan.");
            Console.ReadLine();
            Console.Clear();
        }
        /// <summary>
        /// method to display text on screen when the player gets blackjack (21)
        /// </summary>
        private void BlackJack()
        {
            Console.WriteLine($"Player: {Name} \nCard value: {CalculateValue()}");
            Console.Write(FormatCardToText());
            Console.WriteLine("\n\n");
            Console.WriteLine("BlackJack!!! Hit enter om door te gaan.");
            Console.ReadLine();
            Console.Clear();
        }
        /// <summary>
        /// method to add a card to the player hand
        /// </summary>
        protected void Hit()
        {
            Hand.Add(PullCard(true, deckList));
        }
        /// <summary>
        /// method to double a players bet, and to give him 1 extra card though the Hit() method
        /// </summary>
        private void Double()
        {
            Money += HandMoney * -1;
            HandMoney += HandMoney;
            Hit();

            Console.WriteLine($"Player: {Name} \nCard value: {CalculateValue()}");
            Console.Write(FormatCardToText());
            Console.WriteLine("\n\n\n\n");
            Console.WriteLine("Hit enter om door te gaan.");
            Console.ReadLine();
            Console.Clear();
        }
        /// <summary>
        /// Formats the card list into a printable string 
        /// </summary>
        /// <returns>player hand in string from for displaying on screen</returns>
        public string FormatCardToText()
        {
            string text = "";
            int i = 1;
            foreach (Card card in Hand)
            {
                if (card.Type == Card.CardType.Nummer)
                {
                    text += $"Kaart {i}: {card.Color} {card.Number} \n";
                }
                else
                {
                    text += $"Kaart {i}: {card.Color} {card.Type} \n";
                }
                i++;
            }
            return text;
        }
        /// <summary>
        /// calculates the value of cards in a players hand
        /// can change the value of a ace if the value of the hand is over 10 or 21
        /// </summary>
        /// <returns>the value of the cards in the players hands</returns>
        public int CalculateValue()
        {
            bool highAce = false;
            Value = 0;
            foreach (Card card in Hand)
            {
                if (card.Type == Card.CardType.Aas)
                {
                    //if the hand value is over 10, the added ace needs to be counted as a 1
                    if (Value > 10)
                    {
                        Value += 1;
                        //turns a 11 point ace into a 1 point ace if the player value goes over 21
                        if (highAce && Value > 21)
                        {
                            Value += -10;
                            highAce = false;
                        }
                    }
                    else
                    {
                        Value += card.Number;
                        highAce = true;
                    }
                }
                else
                {
                    Value += card.Number;
                    //turns a 11 point ace into a 1 point ace if the player value goes over 21
                    if (highAce && Value > 21)
                    {
                        Value += -10;
                        highAce = false;
                    }
                }
            }
            return Value;
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
