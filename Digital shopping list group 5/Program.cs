using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Digital_shopping_list_group_5.Program;

namespace Digital_shopping_list_group_5
{
    internal partial class Program
    {
        //Description
        //...
        //...

        static void Main(string[] args)
        {
            
            //The proccess of writing down the purchase lists into the file. 
            // TO BE EXTENDED 

            //Item item = new Item("milk", 1, false);
            //var V1 = new Do(item);
            //V1.Add(item);

            //item = new Item("bread", 2, false);
            //V1.Add(item);

            //List<Item> list = LoadItemsFromFile(); // items
            //Purchase purchaseList = new Purchase("Biltema", list);

            //purchaseList.PrintAll();
            //V1 = new Do(purchaseList);
            //V1.Add(purchaseList);  // inköpslistor. 

            RunMenu();           
        }
        private static void RunMenu()
        {
            Database data1 = new Database();
            Console.WriteLine("[1] Shopping lists");
            Console.WriteLine("[2] Receipts");
            Console.WriteLine("[3] Shopping");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    Console.WriteLine("[1] Create list");
                    Console.WriteLine("[2] Edit list");
                    Console.WriteLine("[3] Delete list");
                    Console.WriteLine("[4] Show lists ");
                    Console.WriteLine("[5] Merge lists");
                    Console.WriteLine("[6] Share list");
                    Console.WriteLine("[7] Change list name"); //Det kanske ska ligga i create och edit
                    Console.WriteLine();
                    Console.WriteLine("[b] Back");
                    userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "2":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "3":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "4":
                            data1.LoadLists(userInput);
                            userInput = Console.ReadLine();
                            break;
                        case "5":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "6":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "7":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "b":
                            RunMenu();
                            break;
                        default:
                            Console.Write($"\nInvalid option: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{userInput}\n");
                            Console.ResetColor();
                            RunMenu();
                            break;
                    }

                    break;
                case "2":
                    Console.WriteLine("Code missing..");
                    RunMenu();
                    break;
                case "3":
                    Console.WriteLine("Code missing..");
                    RunMenu();
                    break;
                default:
                    Console.Write($"\nInvalid option: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{userInput}\n");
                    Console.ResetColor();
                    RunMenu();
                    break;
            }

        }
        static List<Item> LoadItemsFromFile()
        {
            List<Item> items = new List<Item>();
            using (var streamreader = new StreamReader(@"Path/items.csv"))
            {
                string str;

                while((str = streamreader.ReadLine()) != null)
                {
                    string[] arr = str.Split(';');
                    Item item = new Item();
                    item.SetID(Int32.Parse(arr[0]));
                    item.SetName(arr[1]);
                    item.SetIsBought(bool.Parse(arr[2]));
                    items.Add(item);
                }                
            }
            return items;
        }
    }
}
