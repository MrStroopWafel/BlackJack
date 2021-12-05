using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Card
    {
        //making enums
        public enum CardColor
        {
            Harte,
            Ruite,
            Schoppen,
            Klaveren
        }
        public enum CardType
        {
            Nummer,
            Boer,
            Vrouw,
            Koning,
            Aas
        }

        //declaring variables
        public int Number;
        public bool Status;
        public CardColor Color;
        public CardType Type;

        public Card(int _Number, string _Color, string _Type)
        {
            Number = _Number;
            Status = true;
            //switches to turn the strings into propper enums
            switch (_Color)
            {
                case "Harte":
                    Color = CardColor.Harte;
                    break;
                case "Ruite":
                    Color = CardColor.Ruite;
                    break;
                case "Klaveren":
                    Color = CardColor.Klaveren;
                    break;
                case "Schoppen":
                    Color = CardColor.Schoppen;
                    break;
            }
            switch (_Type)
            {
                case "Nummer":
                    Type = CardType.Nummer;
                    break;
                case "Boer":
                    Type = CardType.Boer;
                    break;
                case "Vrouw":
                    Type = CardType.Vrouw;
                    break;
                case "Koning":
                    Type = CardType.Koning;
                    break;
                case "Aas":
                    Type = CardType.Aas;
                    break;
            }
        }
    }
}
