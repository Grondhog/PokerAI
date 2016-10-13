using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace PokerAI
{
    class MainProgram
    {
        
        

        static void Main(string[] args)
        {
            //TestSorting();
            //TestGetValue();
            Card test1 = new Card(2, 1);//club
            Card test2 = new Card(2, 3);//diamond
            Console.WriteLine(test1.CompareSuit(test2));
        }

        private static void TestGetValue()//not working
        {
            Hand myHand = new Hand();
            for(int i = 0; i < 10; i++)
            {
                myHand = new Hand();
                Console.WriteLine("Hand: " + myHand);
                Console.WriteLine(myHand.GetValue());
            }   
            
            
            
        }

        private static void TestSorting()
        {
            Hand myHand = new Hand(5);
            Console.WriteLine("Before Sort: " + myHand);
            //Console.WriteLine();
            myHand.SortHandByNumber();//DONE
            Console.WriteLine("After Sort: " + myHand);
        }

        

    }
}
