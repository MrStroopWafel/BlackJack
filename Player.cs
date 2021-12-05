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
        private void AskName()
        {
            Console.Write("Player name: ");
            Name = Console.ReadLine();
            Console.Clear();
        }

        public void Stand()
        {

        }
        public void Double()
        {

        }
        public void split()
        {

        }
    }
}
