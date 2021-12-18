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
            TableRound();
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
            //INFINITE LOOP TILL WIN AND LOSE CONDITIONS ARE ADDED                     <------------------------------------------------------------------
            while (true)
            {
                PlaceBets();
                DealCards();

                foreach (Player player in playerList)
                {
                    player.PlayHand(deckList, house.Hand[0]);
                }

                house.HousePlay(deckList);

                Console.WriteLine($"Dealer: \nCard value: {house.CalculateValue()} \n{house.Hand[0].FormatCardToText(house.Hand)}\n");
                //loops though the players to show cards bets and money earned
                foreach (Player player in playerList)
                {
                    Console.WriteLine($"Player: {player.Name} \nMoney: {player.Money} \nBetted money: {player.HandMoney} \nCard value: {player.CalculateValue()}  \n{player.Hand[0].FormatCardToText(player.Hand)}");
                    Console.WriteLine(CheckWin(player));
                }
                ClearHand();
                Console.Write("Hit enter to continue");
                Console.ReadLine();
            }
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
                for (int i = 0; i < 2; i++)
                {
                    //pulls a temp card
                    Card tempCard = deckList[0].PullCard(deckList);
                    //checks if its a fail card
                    if (tempCard.Number != 9999)
                    {
                        player.Hand.Add(tempCard);
                    }
                    else //failcard; means something went wrong or deck needs to be shuffled
                    {
                        deckList[0].Shuffle(deckList);
                        player.Hand.Add(tempCard);
                    }
                }
            }
            //add 2 starting cards for the house
            for (int i = 0; i < 2; i++)
            {
                //pulls a temp card
                Card tempCard = deckList[0].PullCard(deckList);
                //checks if its a fail card
                if (tempCard.Number != 9999)
                {
                    house.Hand.Add(tempCard);
                }
                else //failcard; means something went wrong or deck needs to be shuffled
                {
                    deckList[0].Shuffle(deckList);
                    house.Hand.Add(tempCard);
                }
            }
        }

        private void ClearHand()
        {
            foreach (Player player in playerList)
            {
                player.Hand.Clear();
            }
            house.Hand.Clear();
        }

        private string CheckWin(Player _player)
        {
            //check if player is under 21 and 
            if (_player.CalculateValue() < 21)
            {
                //check if dealer is under 21 
                if (house.CalculateValue() < 21)
                {
                    //checks if the player has higher cards then the dealer
                    if (_player.CalculateValue() > house.CalculateValue())
                    {
                        //checks for blackjack and gives this higher payout
                        if (_player.CalculateValue() == 21)
                        {
                            _player.Money += Convert.ToInt32(_player.HandMoney * 2);
                            return $"{_player.Name} heeft {Convert.ToInt32(_player.HandMoney * 2)} gewonnen!\n";
                        } else //(_player.CalculateValue() != 21)
                        {
                            _player.Money += Convert.ToInt32(_player.HandMoney * 1.5);
                            return $"{_player.Name} heeft {Convert.ToInt32(_player.HandMoney * 1.5)} gewonnen!\n";
                        }
                    } else //(_player.CalculateValue() < house.CalculateValue())
                    {
                        return $"{_player.Name} heeft {Convert.ToInt32(_player.HandMoney)} verloren.\n";
                    }
                } else //(house.CalculateValue() > 21)
                {
                    _player.Money += Convert.ToInt32(_player.HandMoney * 1.5);
                    return $"{_player.Name} heeft {Convert.ToInt32(_player.HandMoney * 2)} gewonnen!\n";
                }
            } else //(_player.CalculateValue() > 21)
            {
                return $"{_player.Name} heeft {Convert.ToInt32(_player.HandMoney)} verloren.\n";
            }  
        }
        
    }
}
