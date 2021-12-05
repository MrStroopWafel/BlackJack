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
        private House house = new House(0);

        public Table(Settings _settings)
        {
            TableInit(_settings);
            while (true)
            {
                TableRound();
            }

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
                playerList.Add(new Player(_Settings.Money));
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
            PlaceBets();
            DealCards();

            foreach (Player player in playerList)
            {
                player.PlayHand(deckList, house.Hand[0]);
            }

            house.HousePlay(deckList);

            Console.WriteLine($"Dealer: \nCard value: {house.CalculateValue()} \n{house.FormatCardToText()}\n");
            //loops though the players to show cards bets and money earned
            foreach (Player player in playerList)
            {
                Console.WriteLine($"Player: {player.Name} \nMoney: {player.Money} \nBetted money: {player.HandMoney} \nCard value: {player.CalculateValue()}  \n{player.FormatCardToText()}");
                //checks if the player has higher cards then the dealer
                if (player.CalculateValue() > house.CalculateValue() && player.CalculateValue() < 22)
                {
                    //checks for blackjack and gives this higher payout
                    if (player.CalculateValue() == 21)
                    {
                        Console.WriteLine($"{player.Name} heeft {Convert.ToInt32(player.HandMoney * 2)} gewonnen!\n");
                        player.Money += Convert.ToInt32(player.HandMoney * 2);
                    }
                    else
                    {
                        Console.WriteLine($"{player.Name} heeft {Convert.ToInt32(player.HandMoney * 1.5)} gewonnen!\n");
                        player.Money += Convert.ToInt32(player.HandMoney * 1.5);
                    }
                }
                else
                {
                    Console.WriteLine($"{player.Name} heeft {Convert.ToInt32(player.HandMoney)} verloren.\n");
                }
            }

            Console.Write("Hit enter to continue");
            Console.ReadLine();
        }
        /// <summary>
        /// method to ask players input to ask bets
        /// </summary>
        private void PlaceBets()
        {
            foreach (Player player in playerList)
            {
                Console.Clear();
                Console.Write($"{player.Name} Money: ${player.Money} \nHow much money do you wanna bet? (Awnser in numbers only): ");
                int tempValue = Int32.Parse(Console.ReadLine());
                Console.Clear();
                player.Money += tempValue * -1;
                player.HandMoney = tempValue;
            }
        }
        /// <summary>
        /// method to give every player 2 card at the start of the round
        /// </summary>
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
