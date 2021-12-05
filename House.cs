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
