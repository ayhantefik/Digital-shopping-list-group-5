using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Digital_shopping_list_group_5
{
    internal class Database
    {
        public List<string> shoppinglists = new List<string>();
        public List<string> itemlist = new List<string>();
        public void LoadLists()
        {
            string file = "Path/listOfPurchases.csv";
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    shoppinglists.Add(line);
                }
            }
        }
        public void LoadItems()
        {
            string file = "Path/items.csv";
            Console.WriteLine();
            using (StreamReader sr = new StreamReader(file))
            {
                string line1;
                while ((line1 = sr.ReadLine()) != null)
                {
                    itemlist.Add(line1);
                }
            }
        }
        public void ShowLists()
        {
            int a = 1;
            foreach (var samplelist in shoppinglists)
            {
                string[] listnamearray = samplelist.Split(';');
                Console.WriteLine($"[{a++}]{listnamearray[0]}");
            }
            Console.WriteLine();
            int userChoose = Int32.Parse(Console.ReadLine());
            Console.WriteLine(shoppinglists[userChoose - 1]);
        }

        public void EditLists(int userChoose)
        {
            Console.WriteLine("Choose list to edit:");
            Console.WriteLine();
            int z = 1;
            foreach (var samplelist in shoppinglists)
            {
                string[] listnamearray = samplelist.Split(';');
                Console.WriteLine($"[{z++}]{listnamearray[0]}");
            }
            int chooseList = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Choosen list:");
            Console.WriteLine();
            Console.WriteLine(shoppinglists[chooseList - 1]);
            Console.WriteLine("[1] Add item");
            Console.WriteLine("[2] Delete item");
            userChoose = Int32.Parse(Console.ReadLine());
            switch (userChoose)
            {
                case 1:
                    int j = 1;
                    Console.WriteLine("Choose item to add:");
                    foreach (var ilist in itemlist)
                    {
                        string[] itemarray = ilist.Split(';');
                        Console.WriteLine($"[{j++}]{itemarray[1]}");
                    }
                    int itemChoose = Int32.Parse(Console.ReadLine());
                    string text = $"{shoppinglists[chooseList - 1]};{itemlist[itemChoose - 1]}";
                    string addtest = text;
                    shoppinglists.RemoveAt(chooseList - 1);
                    shoppinglists.Insert(chooseList - 1, addtest);
                    Console.WriteLine();
                    Console.WriteLine("Shopping list is updated!");
                    Console.WriteLine();
                    Console.WriteLine(shoppinglists[chooseList - 1]);
                    break;
                case 2:
                    Console.WriteLine("Choose item to delete:");
                    int cnt = 2;
                    int plustre = 3;
                    string lineList = shoppinglists[chooseList - 1];
                    string[] itemsInLine = lineList.Split(';');
                    Console.WriteLine($"[1] {itemsInLine[2]}");
                    for (int i = 1; i <= (itemsInLine.Length / 3) + 1; i++)
                    {
                        plustre++;
                        int sum = plustre + i++;
                        Console.WriteLine($"[{cnt++}] {itemsInLine[sum]}");
                    }
                    int itemDelete = Int32.Parse(Console.ReadLine());
                    if (itemDelete == 1)
                    {
                        itemsInLine = itemsInLine.Where((source, index) => index != 1).ToArray();
                        itemsInLine = itemsInLine.Where((source, index) => index != 1).ToArray();
                        itemsInLine = itemsInLine.Where((source, index) => index != 1).ToArray();
                    }
                    if (itemDelete > 1)
                    {
                        int count1 = 1;
                        for (int i = 1; i < 20; i++)
                        {
                            count1++;
                            i++;
                            if (itemDelete == count1)
                            {
                                itemsInLine = itemsInLine.Where((source, index) => index != (count1 + i)).ToArray();
                                itemsInLine = itemsInLine.Where((source, index) => index != (count1 + i)).ToArray();
                                itemsInLine = itemsInLine.Where((source, index) => index != (count1 + i)).ToArray();
                            }
                        }
                    }
                    itemsInLine = Array.ConvertAll(itemsInLine, a => a = a + ";");
                    string update = String.Concat(itemsInLine);
                    shoppinglists.RemoveAt(chooseList - 1);
                    shoppinglists.Insert(chooseList - 1, update);
                    Console.WriteLine();
                    Console.WriteLine("Item is deleted! Updated list:");
                    Console.WriteLine();
                    Console.WriteLine(shoppinglists[chooseList - 1]);
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

