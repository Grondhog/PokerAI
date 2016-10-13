using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAI
{
    public class Card
    {

        public const string SUIT_DIAMOND = "Diamond";
        public const string SUIT_CLUB = "Club";
        public const string SUIT_HEART = "Heart";
        public const string SUIT_SPADE = "Spade";
        public const string TWO = "2";
        public const string THREE = "3";
        public const string FOUR = "4";
        public const string FIVE = "5";
        public const string SIX = "6";
        public const string SEVEN = "7";
        public const string EIGHT = "8";
        public const string NINE = "9";
        public const string TEN = "10";
        public const string JACK = "Jack";
        public const string QUEEN = "Queen";
        public const string KING = "King";
        public const string ACE = "Ace";

        private string pSuit;
        public string suit
        {
            get
            {
                return pSuit;
            }
            set
            {
                if(value == SUIT_DIAMOND || value == SUIT_CLUB || value == SUIT_HEART || value == SUIT_SPADE)
                {
                    pSuit = value;
                }
                else
                {
                    pSuit = SUIT_CLUB;
                }
            }
        }

        private string pNumber;
        private int numberValue;
        public string number
        {
            get { return pNumber; }
            set
            {
                if(value == TWO || value == THREE || value == FOUR || value == FIVE || value == SIX || value == SEVEN || value == EIGHT || value == NINE || value == TEN || value == JACK || value == QUEEN || value == KING)
                {
                    pNumber = value;
                }
                else
                {
                    pNumber = TWO;
                }
            }
        }

        public Card(int _suit, int _number)
        {
            
            if(_suit <= 4)
            {
                switch (_suit)
                {
                    case 1:
                        suit = SUIT_CLUB;
                        break;
                    case 2:
                        suit = SUIT_DIAMOND;
                        break;
                    case 3:
                        suit = SUIT_HEART;
                        break;
                    case 4:
                        suit = SUIT_SPADE;
                        break;
                }
            }
            if(_number <= 13)
            {
                switch (_number)
                {
                    case 1:
                        number = ACE;
                        numberValue = 1;
                        break;
                    case 2:
                        number = TWO;
                        numberValue = 2;
                        break;
                    case 3:
                        number = THREE;
                        numberValue = 3;
                        break;
                    case 4:
                        number = FOUR;
                        numberValue = 4;
                        break;
                    case 5:
                        number = FIVE;
                        numberValue = 5;
                        break;
                    case 6:
                        number = SIX;
                        numberValue = 6;
                        break;
                    case 7:
                        number = SEVEN;
                        numberValue = 7;
                        break;
                    case 8:
                        number = EIGHT;
                        numberValue = 8;
                        break;
                    case 9:
                        number = NINE;
                        numberValue = 9;
                        break;
                    case 10:
                        number = TEN;
                        numberValue = 10;
                        break;
                    case 11:
                        number = JACK;
                        numberValue = 11;
                        break;
                    case 12:
                        number = QUEEN;
                        numberValue = 12;
                        break;
                    case 13:
                        number = KING;
                        numberValue = 13;
                        break;
                }
            }  
        }

        override
        public string ToString()
        {
            return suit + " " + number;
        }

       
        public int CompareSuit(Card that)//Clubs, diamonds, hearts, spades
        {
            if(suit.Equals(SUIT_CLUB))
            {
                if(that.suit.Equals(SUIT_DIAMOND) || that.suit.Equals(SUIT_HEART) || that.suit.Equals(SUIT_SPADE))
                {
                    return -1;
                }
                else
                {
                    return this.CompareNumber(that);
                }
            }
            else if(suit.Equals(SUIT_DIAMOND))
            {
                if (that.suit.Equals(SUIT_HEART) || that.suit.Equals(SUIT_SPADE))
                {
                    return -1;
                }
                else if(that.suit.Equals(SUIT_CLUB))
                {
                    return 1;
                }
                else
                {
                    return this.CompareNumber(that);
                }
            }
            else if (suit.Equals(SUIT_HEART))
            {
                if ( that.suit.Equals(SUIT_SPADE))
                {
                    return -1;
                }
                else if (that.suit.Equals(SUIT_DIAMOND) || that.suit.Equals(SUIT_CLUB))
                {
                    return 1;
                }
                else
                {
                    return this.CompareNumber(that);
                }
            }
            else if (suit.Equals(SUIT_SPADE))
            {
                if (that.suit.Equals(SUIT_DIAMOND) || that.suit.Equals(SUIT_HEART) || that.suit.Equals(SUIT_CLUB))
                {
                    return 1;
                }
                else
                {
                    return this.CompareNumber(that);
                }
            }
            return this.CompareNumber(that);
        } 

        public bool Equals(Card that)
        {
            return this.number.Equals(that.number) && this.suit.Equals(that.suit);
        }

        public int CompareNumber(Card that)
        {
            int ret = this.numberValue - that.numberValue;
            if (ret == 0)
                return this.CompareSuit(that);//uses the suit comparison if the numbers are the same
            return ret;
        }

        public int GetNumberAsInt()
        {
            return numberValue;
        }

    }
}
