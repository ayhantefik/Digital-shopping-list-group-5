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
        List<string> sample = new List<string>();
        List<string> itemlist = new List<string>();
        public void LoadLists(int userChoose)
        {
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
                Console.WriteLine(sample[0]);
            }
            if (userChoose == 2)
            {
                Console.WriteLine(sample[1]);
            }
            if (userChoose == 3)
            {
                Console.WriteLine(sample[2]);
            }
        }
        public void LoadListsOrg()
        {
            string file = "Path/listOfPurchases.csv";
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                int b = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    char[] charsToTrim3 = { ';' };
                    string[] field = line.Split(';');
                    Console.WriteLine($"[{b++}] {field[0]}");
                    string addinlist = $"{line.Trim(charsToTrim3)}";
                    sample.Add(addinlist);
                }

            }
        }
        public void LoadItemList()
        {
            string file = "Path/items.csv";
            Console.WriteLine();
            using (StreamReader sr = new StreamReader(file))
            {
                string line1;
                int b = 1;
                while ((line1 = sr.ReadLine()) != null)
                {
                    //char[] charsToTrim3 = { ';' };
                    string[] field1 = line1.Split(';');
                    Console.WriteLine($"[{b++}] {field1[1]}");
                    string addinlist = $"{line1}";
                    itemlist.Add(addinlist);
                }
            }
        }
        public void EditLists(int userChoose)
        {
            Console.WriteLine("Choose list to edit:");
            Console.WriteLine();
            LoadListsOrg();
            int chooseList = Int32.Parse(Console.ReadLine());
            Console.WriteLine("[1] Add item");
            Console.WriteLine("[2] Delete item");
            userChoose = Int32.Parse(Console.ReadLine());
            switch (userChoose)
            {
                case 1:
                    Console.WriteLine("Choose item to add:");
                    LoadItemList();
                    int itemChoose = Int32.Parse(Console.ReadLine());
                    string text = $"{sample[chooseList - 1]};{itemlist[itemChoose - 1]}";
                    string addtest = text;
                    sample.RemoveAt(chooseList - 1);
                    sample.Insert(chooseList - 1, addtest);
                    Console.WriteLine();
                    Console.WriteLine("Shopping list is updated!");
                    Console.WriteLine();
                    Console.WriteLine(sample[chooseList - 1]);
                    break;
                case 2:
                    
                    break;
                default:
                    Console.Write($"\nInvalid option: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{userChoose}\n");
                    Console.ResetColor();
                    break;
            } 
        }

    }
    
}

