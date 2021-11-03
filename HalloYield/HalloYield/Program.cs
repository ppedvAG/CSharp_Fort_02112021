using System;
using System.Collections.Generic;

namespace HalloYield
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            foreach (int zahl in GetZahlen())
            {
                Console.WriteLine(zahl);
            }
        }

        static IEnumerable<int> GetZahlen()
        {
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;

            //var liste = new List<int>();
            //liste.Add(1);
            //liste.Add(2);
            //liste.Add(3);
            //liste.Add(4);
            //liste.Add(5);
            //liste.Add(6);
            //liste.Add(7);
            //liste.Add(8);
            //return liste;
        }
    }
}
