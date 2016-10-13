using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAI
{
    class Hand
    {
        private static int handSize = 5;
        private static Random rand = new Random();
        private static Card[] cards;


        //values from: http://www.mathcs.emory.edu/~cheung/Courses/170/Syllabus/10/pokerValue.html
        public static int STRAIGHT_FLUSH = 8000000;
        // + valueHighCard()
        public static int FOUR_OF_A_KIND = 7000000;
        // + Quads Card Rank
        public static int FULL_HOUSE = 6000000;
        // + SET card rank
        public static int FLUSH = 5000000;
        // + valueHighCard()
        public static int STRAIGHT = 4000000;
        // + valueHighCard()
        public static int THREE_OF_A_KIND = 3000000;
        // + Set card value
        public static int TWO_PAIRS = 2000000;
        // + High2*14^4+ Low2*14^2 + card
        public static int ONE_PAIR = 1000000;
        // + high*14^2 + high2*14^1 + low

        public Hand(int _handSize)
        {
            if (_handSize > 0)
            {
                SetUp(_handSize);
            }
            else
            {
                SetUp(5);
            }
        }

        public Hand()
        {
            SetUp(5);
            /*handSize = 5;
            rand = new Random();
            cards = new Card[handSize];
            
            cards[0] = new Card(1, 1);
            cards[1] = new Card(3, 10);
            cards[2] = new Card(2, 1);
            cards[3] = new Card(4, 6);
            cards[4] = new Card(2, 4);*/
        }

        private void SetUp(int _size)
        {
            handSize = _size;
            
            cards = new Card[handSize];
            cards = DealHand();
        }

        public Card[] DealHand()//pulls random cards from infinity, instead of pulling from a deck could be changed
        {
            Console.WriteLine("DealHand");
            Card[] ret = new Card[handSize];
            int val = 1;
            int suit = 1;
            for (int i = 0; i < handSize ; i++)
            {
                val = rand.Next(13);
                suit = rand.Next(4);
                //Console.WriteLine(new Card(suit + 1, val + 1));
                ret[i] = new Card(suit + 1 , val + 1);
                //Console.WriteLine(ret[i]);
            }
            return ret;
        }

        override
        public string ToString()
        {
            string ret = "";
            for (int i = 0; i < handSize; i++)
            {
                ret += cards[i].ToString() + ", ";
            }
            return ret;
        }

        private void ThreeWaySwap(int first, int second)
        {
            Card temp = cards[first];
            cards[first] = cards[second];
            cards[second] = temp;

        }

        public void SortHandByNumber()
        {
            for (int i = 0; i < handSize; i++)
            {
                for (int j = 0; j < handSize - i - 1; j++)
                {
                    if (cards[j].CompareNumber(cards[j + 1]) > 0)
                    {
                        //Console.WriteLine("i: " + i + ", j: " + j + ". Cards[j]: " + cards[j] + ", cards[j + 1]: " + cards[j + 1]);
                        //Console.WriteLine("Cards[j]: " + cards[j]);
                        ThreeWaySwap(j, j + 1);
                        //Console.WriteLine("Cards[j]: " + cards[j]);
                    }

                }
            }

        }

        public void SortHandBySuit()//Clubs, diamonds, hearts, spades
        {
            for (int i = 0; i < handSize - 1; i++)
            {
                for (int j = 0; j < handSize - 1; j++)
                {
                    //Console.WriteLine("i: " + i + ", j: " + j + ". Cards[j]: " + cards[j] + ", cards[j + 1]: " + cards[j + 1]);
                    if (cards[j].CompareSuit(cards[j + 1]) > 0)
                    {

                        //Console.WriteLine("Cards[j]: " + cards[j]);
                        ThreeWaySwap(j, j + 1);
                        //Console.WriteLine("Cards[j]: " + cards[j]);
                    }

                }
            }
        }

        public int GetValue()
        {
            if (IsFlush() && IsStraight())
            {
                Console.WriteLine("Straigt Flush");
                return ValueStraightFlush();
            }
            else if (IsFourOfAKind())
            {
                Console.WriteLine("Four of a Kind");
                return ValueFourOfAKind();
            }
            else if (IsFullHouse())
            {
                Console.WriteLine("Full House");
                return ValueFullHouse();
            }
            else if (IsFlush())
            {
                Console.WriteLine("Flush");
                return ValueFlush();
            }
            else if (IsStraight())
            {
                Console.WriteLine("Straight");
                return ValueStraight();
            }
            else if (IsThreeOfAKind())
            {
                Console.WriteLine("Three of a Kind");
                return ValueThreeOfAKind();
            }
            else if (IsTwoPair())
            {
                Console.WriteLine("Two Pair");
                return ValueTwoPairs();
            }
            else if (IsPair())
            {
                Console.WriteLine("Pair");
                return ValueOnePair();
            }
            else
            {
                Console.WriteLine("High Card"); 
                return ValueHighCard();
            }
                
        }

        private bool IsFlush()
        {
            string firstSuit = cards[0].suit;
            for (int i = 1; i < handSize; i++)
            {
                if (!cards[i].suit.Equals(firstSuit))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsStraight()
        {
            SortHandByNumber();
            for (int i = 0; i < handSize - 1; i++)
            {
                if (cards[i].GetNumberAsInt() != cards[i + 1].GetNumberAsInt() - 1)
                {
                    return false;
                }
            }
            return true;
        }

        private int GetCount(int i)
        {
            //Console.WriteLine("GetCount(int i)");
            int count = 1;
            for (int j = 0; j < handSize; j++)
            {
                if (j != i)
                {
                    //Console.WriteLine("cards[i]: " + cards[i].number + ", cards[j]: " + cards[j].number);
                    if (cards[i].number.Equals(cards[j].number))
                    {
                        count++;
                        //Console.WriteLine("count: " + count);
                    }
                }

            }
            return count;
        }

        private bool IsFourOfAKind()
        {
            int count;
            for (int i = 0; i < handSize - 1; i++)
            {
                count = GetCount(i);
                if (count == 4)
                {
                    return true;
                }
                else if (count <= 2)
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsFullHouse()//make sure that there are only two numbers
        {
            //Console.WriteLine("IsFullHouse()");
            int number1 = cards[0].GetNumberAsInt(), number2 = -1;
            for (int i = 1; i < handSize; i++)
            {
               // Console.WriteLine("cards[i].GetnumberAsInt(): " + cards[i].GetNumberAsInt());
                //Console.WriteLine("number1: " + number1 + ", number2: " + number2);
                if (number2 == -1)
                {
                    if(cards[i].GetNumberAsInt() != number1)
                    {
                        number2 = cards[i].GetNumberAsInt();
                    }
                }
                else if (cards[i].GetNumberAsInt() != number1 && cards[i].GetNumberAsInt() != number2)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsThreeOfAKind()
        {
            int count;
            for (int i = 0; i < handSize; i++)
            {
                count = GetCount(i);
                //Console.WriteLine("cards[i]: " + cards[i] + ", count: " + count);
                if (count == 3)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsTwoPair()
        {
            int number1 = -1, count = 0;
            for(int i = 0; i < handSize; i++)
            {
                count = GetCount(i);
                if(count == 4)
                {
                    return false;
                }
                else if(count == 2)
                {
                    if(number1 == -1)
                    {
                        number1 = cards[i].GetNumberAsInt();
                    }
                    else if(number1 != -1 && cards[i].GetNumberAsInt() != number1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsPair()
        {
            int count;
            for(int i = 0; i < handSize; i++)
            {
                count = GetCount(i);
                if(count == 2)
                {
                    return true;
                }
                else if(count == 4)
                {
                    return false;
                }
            }
            return false;
        }

        private int ValueStraightFlush()
        {
            return STRAIGHT_FLUSH + ValueHighCard();
        }

        private int ValueFourOfAKind()
        {
            SortHandByNumber();
            return FOUR_OF_A_KIND + cards[handSize / 2].GetNumberAsInt();
        }

        private int ValueFullHouse()//should only be called if a full house
        {
            int highestNumber = cards[0].GetNumberAsInt();
            for(int i = 1; i < handSize; i++)
            {
                if(cards[i].GetNumberAsInt() > highestNumber)
                {
                    return FULL_HOUSE + highestNumber;
                }
            }
            return FULL_HOUSE + highestNumber;
        }

        private int ValueFlush()
        {
            return FLUSH + ValueHighCard();//only works for texas hold'em
        }

        private int ValueStraight()
        {
            return STRAIGHT + ValueHighCard();
        }

        private int ValueThreeOfAKind()
        {
            SortHandByNumber();
            return THREE_OF_A_KIND + cards[handSize / 2].GetNumberAsInt();
        }

        private int ValueTwoPairs()
        {
            int highPairCard = -1, lowPairCard = -1, unpairedCard = -1, count = 0;
            for(int i = 0; i < handSize; i++)
            {
                count = GetCount(i);
                if(count == 1)
                {
                    unpairedCard = cards[i].GetNumberAsInt();
                }
                else if(count == 2 && highPairCard == -1)
                {
                    highPairCard = cards[i].GetNumberAsInt();
                }
                else if(count == 2)
                {
                    if(highPairCard > cards[i].GetNumberAsInt())
                    {
                        lowPairCard = cards[i].GetNumberAsInt();
                    }
                    else
                    {
                        lowPairCard = highPairCard;
                        highPairCard = cards[i].GetNumberAsInt();
                    }
                }
                
            }
            return TWO_PAIRS + (highPairCard * 14 ^ 2) + (lowPairCard * 14) + unpairedCard;
        }

        private int ValueOnePair()//ONE_PAIR + 14^3 * pairedCard + 14^2 * highUnpairedCard + 14 * middleUnpairedCard + lowUnpairedCard
        {
            SortHandByNumber();
            int pairedCard = -1, highUnpairedCard = -1, middleUnpairedCard = -1, lowUnpairedCard = -1;
            int j = 0, ret = 0;
            for(int i = 0; i < handSize; i++)
            {
                
                if(GetCount(i) == 2)
                {
                    pairedCard = cards[i].GetNumberAsInt();
                }
                else
                {
                    ret += Convert.ToInt32(cards[i].GetNumberAsInt() * Math.Pow(14, j++));
                }
                
            }
            ret += Convert.ToInt32(pairedCard * Math.Pow(14, 3));
            return ret;
        }

        private int ValueHighCard()
        {
            double ret = 0;
            for(int i = 0; i < handSize; i++)
            {
                ret += cards[i].GetNumberAsInt() * Math.Pow(14, i);
            }
            return (int)ret;
        }

    }
}
