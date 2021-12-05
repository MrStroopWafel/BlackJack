using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class CardData
    {
        /// <summary>
        /// jagged array with 52 card objects within. This is the card deck.
        /// </summary>
        public Object[][] Data = new object[52][];

        public CardData()
        {
            //the number of each card. each card jacket having 14 cards
            int cardNummer = 2;
            string cardColor = "Harte";
            for (int i = 0; i < Data.Length; i++)
            {
                //A switch to change the card color based on the index of the card
                switch (i)
                {
                    case 13:
                        cardColor = "Ruite";
                        break;
                    case 26:
                        cardColor = "Klaveren";
                        break;
                    case 39:
                        cardColor = "Schoppen";
                        break;
                }
                ///A switch to differentiate between cards
                switch (cardNummer)
                {
                    case < 11:
                        Data[i] = new Object[] { cardNummer, cardColor, "Nummer" };
                        break;
                    case 11:
                        Data[i] = new Object[] { 10, cardColor, "Boer" };
                        break;
                    case 12:
                        Data[i] = new Object[] { 10, cardColor, "Vrouw" };
                        break;
                    case 13:
                        Data[i] = new Object[] { 10, cardColor, "Koning" };
                        break;
                    case 14:
                        Data[i] = new Object[] { 11, cardColor, "Aas" };
                        break;
                }
                //resets cardnummer if at 14 or adds 1 to cardnumber for the next loop
                if (cardNummer == 14)
                {
                    cardNummer = 2;
                }
                else
                {
                    cardNummer++;
                }
            }
        }
    }
}
