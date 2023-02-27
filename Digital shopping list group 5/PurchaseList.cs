using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace Digital_shopping_list_group_5
{

    //Description PurchaseList is the old "Purchase"...
    //...
    //...
    public class PurchaseList
    {
        //The security system was embedded to NewPurchase() & RemovePurchaseList()

        // SelectPurchase() & ViewPurchase, to be further developed

        int id;
        string name { get; set; }
        List<Item> listOfItems = new List<Item>();


        public PurchaseList() { }
        public PurchaseList(int id, string name,List<Item> listOfItems)
        {
            this.id = id;
            this.name = name;
            this.listOfItems = listOfItems;
        }


        //======================================================================
        //Setters & Getters
        public int Id => id; public void SetID(int value) => id = value;
        public string Name => name; public void SetName(string value) => name = value;
        public List<Item> ListOfItems => listOfItems; public void SetListOfItems(List<Item> value) => listOfItems = value;
        //=======================================================================


        public override string ToString()
        {
            string str = id + ";" + name + ";";
            foreach (Item item in listOfItems)
            { str += item.ToString() + ";"; }
            return str;
        }




        //Adds new purschases to consumers list of purchases.
        //TBD: Not finalized!
        public void NewPurchase(Database db, Consumer consumer) 
        {
            string input;
            bool quit = false;
            var newPurchase = new PurchaseList(); 
            var newItemList = new List<Item>();

            while (quit == false)
            {
                Console.WriteLine("[1] Create new purchase list."); // it works
                Console.WriteLine("[2] Create new purchase list from template (existing list)"); // TBD
                Console.WriteLine("[3] Quit.");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":

                        Console.Write($"Name: ");
                        input = Console.ReadLine();
                        if (!String.IsNullOrEmpty(input))
                        {
                            newPurchase.SetName(input);

                            // assign the unique ID
                            int lastExistingID = 0;
                            foreach (PurchaseList pl in db.ListOfPurchases)
                            {
                                if (pl.id > lastExistingID) lastExistingID = pl.Id;
                            }
                            lastExistingID += 1;
                            newPurchase.SetID(lastExistingID);
                            newPurchase.SetListOfItems(newItemList);

                            consumer.ListOfPurchases.Add(newPurchase); // add the newly created purchase list to Consumer
                            consumer.IdsOfPurchaseLists.Add(lastExistingID);//add the newly created purchase list´s ID to Consumer
                            db.AddObjectToDatabase(newPurchase); // add the newly created purchase list to <listOfPurchases.csv> file
                            db.EditObjectInDatabase(consumer);//update Consumer in the <accounts.csv> file
                            Console.WriteLine($"<{newPurchase.Name}> [ID: {newPurchase.Id}] successfully added to {db.GetConsumer.Email}");
                            
                        }
                        quit = true;
                        break;
                    case "2": // TBD
                        newPurchase = SelectPurchase(db,consumer);
                        Console.WriteLine("Purchase list created from existing list.");
                        quit = true;
                        break;
                    case "3": return;
                    default: Console.WriteLine($"Unknown input: {input}"); break;
                }
            }
            quit = false;
            
            while (quit == false) // TBD
            {
                //update all the other depended objects and update DB afterwards.

                Console.WriteLine($"[1] Add items to \"{newPurchase.Name}\"."); 
                Console.WriteLine($"[2] Remove items from \"{newPurchase.Name}\".");
                Console.WriteLine($"[3] Discard list (\"{newPurchase.Name}\") and quit."); // The same as <[3] Delete a purchase list> in the previous menu
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        //TBD                        
                        break;
                    case "2":
                        // METHOD: Remove items from list.
                        break;
                    case "3":
                        //Remove purchase list from Consumer and then from DB
                        //(remove purchase list´s ID from  <accounts.csv>, and remove purchaselist itself from <listOfPurchaseLists.csv>)

                        Console.WriteLine($"New list \"{newPurchase.name}\" discarded.");
                        quit = true;
                        break;
                    default: Console.WriteLine($"Unknown input: {input}"); break;
                }
            }
        }
        public void RemovePurchaseList(Database db, Consumer consumer) // the old DeletePurchaseList() method from <Database> class.
        {
            Console.WriteLine("Choose an ID that you want to delete:");
            Console.WriteLine();

            db.Display(db, consumer.ListOfPurchases, true); //< true > shows the purchase list´s IDs and the names,NO items. < false > includes the items for every purchase list


            Console.WriteLine();
            int userInput = Int32.Parse(Console.ReadLine()); // check input, ID 667
            int index = -1;
            foreach (PurchaseList pl in consumer.ListOfPurchases)
            {
                if (userInput == pl.Id)
                { 
                    index = consumer.ListOfPurchases.IndexOf(pl);                    
                }
            }
            if (index != -1)
            {
                db.ListOfPurchases.RemoveAt(index); // removing the purchase list from the <List> of purchases
            }else Console.WriteLine($"ID [{userInput}] not found");


            index = -1;
            foreach(int i in consumer.IdsOfPurchaseLists)
            {
                if (i == userInput)
                { 
                    index = consumer.IdsOfPurchaseLists.IndexOf(i);
                }
            }
            if (index != -1)
            { 
                consumer.IdsOfPurchaseLists.RemoveAt(index); // removing purchase list´s ID from Consumer
            }
            db.UpdateFileInDataBase(1); // updating <listOfPurchases.csv>
            db.UpdateFileInDataBase(2); // updating <accounts.csv>
        }




        public static PurchaseList SelectPurchase(Database db,Consumer consumer) // TBD
        {
            ViewPurchase(db,consumer);
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
        public static void ViewPurchase(Database db,Consumer consumer) //TBD
        {
            int i = 1;
            Console.WriteLine($"{consumer.Name}'s purchase lists: ");
            foreach (PurchaseList p in consumer.ListOfPurchases)
            {
                Console.WriteLine($"{i} . {p.name}");
                i++;
            }
        }
        public void ShareList(Database db, Consumer consumer)
        {
            Console.WriteLine();
            db.Display(db, db.GetConsumer.ListOfPurchases, true);
            Console.WriteLine();
            Console.WriteLine("Choose list number that you want to share:");
            int userInput = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Choosen list:");
            foreach (PurchaseList l in consumer.ListOfPurchases)
            {
                if (l.id == userInput)
                {
                    Console.WriteLine(l);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Send to:");
            Console.WriteLine();
            Console.WriteLine("[1] Existing member");
            Console.WriteLine("[2] Non existing member");
            int index = 1;
            int chooseex = Int32.Parse(Console.ReadLine());
            switch (chooseex)
            {
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("Choose member to send to:");
                    foreach (Consumer w in db.ListOfConsumers)
                    {
                        if (consumer.Email != w.Email)
                        {
                            Console.WriteLine($"[{index++}] {w.Name} {w.Email}");
                        }
                    }
                    int choosemember = Int32.Parse(Console.ReadLine());
                    using (StreamWriter sw = new StreamWriter("Path/inbox.csv"))
                    {
                        foreach (PurchaseList l in consumer.ListOfPurchases)
                        {
                            if (l.id == userInput)
                            {
                                sw.WriteLine($"{consumer.Email};{db.ListOfConsumers[choosemember].Email};{l}");
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("The list is shared!");
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("To email:");
                    string emailto = Console.ReadLine();
                    using (StreamWriter sw = new StreamWriter("Path/inbox.csv"))
                    {
                        foreach (PurchaseList l in consumer.ListOfPurchases)
                        {
                            if (l.id == userInput)
                            {
                                sw.WriteLine($"{consumer.Email};{emailto};{l}");
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("The list is shared!");
                    break;
            }
            


        }
    }
}

