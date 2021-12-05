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
    }
}
