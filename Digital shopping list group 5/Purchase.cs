using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace Digital_shopping_list_group_5
{

    //Description
    //...
    //...
    public class Purchase : IAct
    {
        public string name { get; set; }
        //string name = "null";
        List<Item> listOfItems = new List<Item>();

        public Purchase(string name, List<Item> listOfItems)
        {
            this.name = name;
            this.listOfItems = listOfItems;
        }

        public Purchase() { }

        public List<Item> GetList() => listOfItems;
        public override string ToString()
        {
            string str = null;
            foreach (Item item in listOfItems)
            { str += item.ToString(); }
            return str;
        }
        public void Display()
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        void IAct.Add(Object obj)
        {
            string str = name + ";" + obj.ToString();
            using (var streamwriter = new StreamWriter(@"Path/listOfPurchases.csv", true))
            {
                streamwriter.WriteLine(str);
            }
            System.IO.File.WriteAllText(@"Path/items.csv", string.Empty);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SUCCESS: ");
            Console.WriteLine(str);

        }

            // Adds new purschases to customers list of purchases. TBD: Not finalized!
            public void NewPurchaseList()
            {
                bool quit = false;
                var newPurchaseList = new Purchase();
                var newList = new List<Item>();

            Console.WriteLine("[1] Create new purchase list.");
            Console.WriteLine("[2] Use existing list as template.");
            Console.WriteLine("[3] Quit.");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("New empty purchase list created.");
                    break;
                case "2":
                    // METHOD: View/Select purchase lists from customers lists.
                    // newList = ... // newPurchaseList = ...
                    Console.WriteLine("Purchase list created from existing list.");
                    break;
                case "3": return;
                default: Console.WriteLine($"Unknown input: {input}"); break;
            }

            Console.Write("Enter name of new purchase list: ");
            newPurchaseList.name = Console.ReadLine();

            while (quit = false)
            {
                Console.WriteLine($"[1] Add items to \"{newPurchaseList.name}\".");
                Console.WriteLine($"[2] Remove items from \"{newPurchaseList.name}\".");
                Console.WriteLine($"[3] Save list (\"{newPurchaseList.name}\") and quit.");
                Console.WriteLine($"[4] Discard list (\"{newPurchaseList.name}\") and quit.");

                switch (input)
                {
                    case "1":
                        // METHOD: Add items to list. 
                        break;
                    case "2":
                        // METHOD: Remove items from list.
                        break;
                    case "3":
                        Console.WriteLine($"New list \"{newPurchaseList.name}\" successfully created and saved.");
                        newPurchaseList.listOfItems = newList;
                        // ADD: newPurchaseList to customers.
                        // METHOD: Save list to file? 
                        break;
                    case "4":
                        Console.WriteLine($"New list \"{newPurchaseList.name}\" discarded.");
                        quit = true;
                        break;
                    default: Console.WriteLine($"Unknown input: {input}"); break;
                }
            }
        }


    }

}

