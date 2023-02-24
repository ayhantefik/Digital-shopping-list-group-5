using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace Digital_shopping_list_group_5
{

    //Description PurchaseList is the old "Purchase"...
    //...
    //...
    public class PurchaseList : IAct
    {
        public int id;
        public Consumer owner;
        public string name { get; set; }
        //string name = "null";
        List<Object> listOfItems = new List<Object>();

        public PurchaseList(int id, Consumer owner, string name, List<object> listOfItems)
        {
            this.id = id;
            this.owner = owner;
            this.name = name;
            this.listOfItems = listOfItems;
        }
        public PurchaseList() { }



        public List<Object> GetList() => listOfItems;


        //================================================
        //recording & retrieving data
        void IAct.SaveToDb(Object obj)
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
        List<Object> IAct.LoadFromDb()
        {
            List<Object> listOfPurchases = new List<Object>();

            using (StreamReader str = new StreamReader(@"Path/listOfPurchases.csv"))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {

                    listOfPurchases.Add(line);
                }

            }
            return listOfPurchases;
        }
        //================================================







        //================================================
        //Folowwing 4 functions TBD
        public override string ToString()
        {
            string str = null;
            foreach (object item in listOfItems)
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


        //Adds new purschases to consumers list of purchases.
        //TBD: Not finalized!
        public static void NewPurchase(Consumer consumer)   // USER,CONSUMER ACCESSIBILITY
        {
            string input;
            bool quit = false;
            var newPurchase = new PurchaseList();
            var newList = new List<Object>();

            while (quit == false)
            {
                Console.WriteLine("[1] Create new purchase list.");
                Console.WriteLine("[2] Create new purchase list from template (existing list).");
                Console.WriteLine("[3] Quit.");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("New empty purchase list created.");
                        quit = true;
                        break;
                    case "2":
                        newPurchase = SelectPurchase(consumer);
                        Console.WriteLine("Purchase list created from existing list.");
                        quit = true;
                        break;
                    case "3": return;
                    default: Console.WriteLine($"Unknown input: {input}"); break;
                }
            }

            quit = false;
            Console.Write($"Enter name of new purchase list \"{newPurchase.name}\": ");
            newPurchase.name = Console.ReadLine();
            while (quit == false)
            {
                Console.WriteLine($"[1] SaveToDb items to \"{newPurchase.name}\".");
                Console.WriteLine($"[2] Remove items from \"{newPurchase.name}\".");
                Console.WriteLine($"[3] Save list (\"{newPurchase.name}\") and quit.");
                Console.WriteLine($"[4] Discard list (\"{newPurchase.name}\") and quit.");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        // METHOD: SaveToDb items to list.
                        break;
                    case "2":
                        // METHOD: Remove items from list.
                        break;
                    case "3":
                        Console.WriteLine($"New list \"{newPurchase.name}\" successfully created and saved.");
                        newPurchase.listOfItems = newList;
                        // ADD: newPurchaseList to customers.
                        // METHOD: Save list to file?
                        break;
                    case "4":
                        Console.WriteLine($"New list \"{newPurchase.name}\" discarded.");
                        quit = true;
                        break;
                    default: Console.WriteLine($"Unknown input: {input}"); break;
                }
            }
        }
        public static PurchaseList SelectPurchase(Consumer consumer)    // USER,CONSUMER ACCESSIBILITY
        {
            ViewPurchase(consumer);
            Console.Write("Enter the number of the purchase list: ");
            int.TryParse(Console.ReadLine(), out int input);

            if (input > 0 && input <= consumer.ListOfPurchases.Count)
            {
                return (PurchaseList)consumer.ListOfPurchases[input - 1];
            }
            else
            {
                Console.WriteLine("Could not find the purchase list.");
            }
            return null;
        }
        public static void ViewPurchase(Consumer consumer)
        {
            int i = 1;
            Console.WriteLine($"{consumer.Name}'s purchase lists: ");
            foreach (PurchaseList p in consumer.ListOfPurchases)
            {
                Console.WriteLine($"{i} . {p.name}");
                i++;
            }
        }
    }
}

