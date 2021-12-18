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
            IsPlaying = true;
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
            Console.Write(Hand[0].FormatCardToText(Hand));
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
            Console.Write(Hand[0].FormatCardToText(Hand));
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
            Console.Write(Hand[0].FormatCardToText(Hand));
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
            //pulls a temp card
            Card tempCard = deckList[0].PullCard(deckList);
            //checks if its a fail card
            if (tempCard.Number != 9999)
            {
                Hand.Add(tempCard);
            }
            else //failcard; means something went wrong or deck needs to be shuffled
            {
                deckList[0].Shuffle(deckList);
                Hand.Add(tempCard);
            }
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
            Console.Write(Hand[0].FormatCardToText(Hand));
            Console.WriteLine("\n\n\n\n");
            Console.WriteLine("Hit enter om door te gaan.");
            Console.ReadLine();
            Console.Clear();
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
    }
}
