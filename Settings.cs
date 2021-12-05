using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Settings
    {
        public int Players, Decks;

        //TODO: Settings for burning cards; Settings for money
        public Settings()
        {
            AskPlayerAmount();
            AskDeckAmount();
        }
        /// <summary>
        /// asks for amount of players playing
        /// </summary>
        /// <returns>user input player amount<int></returns>
        private void AskPlayerAmount()
        {
            Console.Write("How many players are playing? (Awnser in numbers only): ");
            Players = Int32.Parse(Console.ReadLine());
            Console.Clear();
        }
        /// <summary>
        /// asks how many card decks to use
        /// </summary>
        /// <returns>user intput card deck amount<int></returns>
        private void AskDeckAmount()
        {
            Console.Write("How many decks do you want to play with? (Awnser in numbers only): ");
            Decks = Int32.Parse(Console.ReadLine());
            Console.Clear();
        }
    }
}
