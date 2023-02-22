using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital_shopping_list_group_5
{
    internal class Database
    {
        public void LoadLists(int userChoose)
        {
            List<string> sample = new List<string>();
            string file = "Path/listOfPurchases.csv";
            Console.WriteLine("Choose shopping list:");
            Console.WriteLine();
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                int b = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    char[] charsToTrim3 = { ';'};
                    string[] field = line.Split(';');
                    Console.WriteLine($"[{b++}] {field[0]}");
                    string addinlist = $"{line.Trim(charsToTrim3)}";
                    sample.Add(addinlist);
                }
                
            }
            Console.WriteLine();
            userChoose = Int32.Parse(Console.ReadLine());
            if (userChoose == 1)
            {
                Console.WriteLine(sample[userChoose - 1]);
            }
            if (userChoose == 2)
            {
                Console.WriteLine(sample[userChoose - 1]);
            }
            if (userChoose == 3)
            {
                Console.WriteLine(sample[userChoose - 1]);
            }
        }
    }
}

