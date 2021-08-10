using System;

namespace Denomination
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n------------------------------------\n ");
            Console.WriteLine("\n   -------  Denomination   ------");
            Console.WriteLine("\n------------------------------------\n ");

           int k = 0;

            int remainder;// = 290;


            int[] amounts = { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

            foreach (int amount in amounts)
            {
                Console.WriteLine("\n------- " + amount + " ------");
                Console.WriteLine(100.ToString() + "\t" + 50.ToString() + "\t" + 10.ToString());
                Console.WriteLine("----\t----\t----");
                remainder = amount;
                for (int i = 0; i <= amount / 100 + 1; i++)
                {
                    remainder = amount - i * 100;
                    for (int j = 0; j <= remainder / 50; j++)
                    {
                        remainder -= j * 50;
                        k = remainder / 10;

                        if (k < 0) continue;
                        Console.WriteLine(i.ToString() + "\t" + j.ToString() + "\t" + k.ToString());
                        remainder = amount - i * 100;
                    }
                }

            }
            Console.Read();
        }
    }
}
