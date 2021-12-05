using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class House : Player
    {
        public House(int _Money) : base(_Money)
        {
        }
        /// <summary>
        /// method to override the askName method of the Player class and change the name to "Dealer'
        /// </summary>
        protected override void AskName()
        {
            Name = "Dealer";
        }
        public void HousePlay(List<CardDeck> _DeckList)
        {
            deckList = _DeckList;
            while (CalculateValue() < 17)
            {
                Hit();
            }
        }
    }
}
