﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Digital_shopping_list_group_5
{
    // Object class PurchaseList
    public class PurchaseList
    {
        // Fields
        private int _id;
        private string _name;
        private List<Item> _listOfItems = new List<Item>();

        // Properties (Setters & Getters)
        public int Id => _id;
        public void SetID(int value) => _id = value;
        public string Name => _name;
        public void SetName(string value) => _name = value;
        public List<Item> ListOfItems => _listOfItems;
        public void SetListOfItems(List<Item> value) => _listOfItems = value;

        string name { get; set; }
        List<Item> listOfItems = new List<Item>();

        // Constructors
        public PurchaseList() { }
        public PurchaseList(int id, string name, List<Item> listOfItems)
        {
            _id = id;
            _name = name;
            _listOfItems = listOfItems;
        }

        // Methods

        public override string ToString()
        {
            string str = _id + ";" + _name + ";";
            foreach (Item item in _listOfItems)
            { str += item.ToString() + ";"; }
            return str;
        }

        // NewPurchaseList(): Creates new PurchaseList and adds to Consumer.ListOfPurchases & Consumer.IdsOfPurchaseLists.
        public void NewPurchaseList(Database db, Consumer consumer)
        {
            string input;
            bool quit = false;
            var newPurchaseList = new PurchaseList();
            var newItemList = new List<Item>();

            Console.Clear();
            Console.WriteLine("NEW PURCHASE LIST:");
            Console.WriteLine();

            // First option selection loop:
            while (quit == false)
            {
                Console.WriteLine("[1] Create new purchase list.");
                Console.WriteLine("[2] Create new purchase list from template (existing list)");
                Console.WriteLine("[3] Quit and return.");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Purchase list created from new list.");

                        quit = true;
                        break;
                    case "2": // TBD
                        var templatePurchaseList = SelectPurchaseList(db, consumer);
                        newItemList = templatePurchaseList.ListOfItems;
                        Console.WriteLine($"Purchase list created from existing list \"{templatePurchaseList.Name}\".");

                        quit = true;
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine($"Unknown input: {input}");
                        break;
                }
            }
            quit = false;

            // Assigns unique ID
            // TBD: Create method?
            int lastExistingID = 0;
            foreach (PurchaseList pl in db.ListOfPurchases)
            {
                if (pl._id > lastExistingID) lastExistingID = pl.Id;
            }
            lastExistingID += 1;
            newPurchaseList.SetID(lastExistingID);

            Console.Write($"Enter name of new purchase list: ");
            input = Console.ReadLine();
            newPurchaseList.SetName(input);

            newPurchaseList.SetListOfItems(newItemList);

            // Second option selection loop.
            while (quit == false)
            {
                Console.WriteLine();
                Console.WriteLine($"[1] Edit items of \"{newPurchaseList.Name}\".");
                Console.WriteLine($"[2] Finalize list \"{newPurchaseList.Name}\" and save");
                Console.WriteLine($"[3] Discard list (\"{newPurchaseList.Name}\") and quit."); // The same as <[3] Delete a purchase list> in the previous menu
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        newPurchaseList.EditPurchaseList(db);

                        break;
                    case "2":
                        consumer.ListOfPurchases.Add(newPurchaseList); // Adds the newly created purchase list to Consumer.
                        consumer.IdsOfPurchaseLists.Add(newPurchaseList.Id); // Adds the newly created purchase list´s ID to Consumer.
                        db.AddObjectToDatabase(newPurchaseList); // Adds the newly created purchase list to "listOfPurchases.csv" file.
                        db.EditObjectInDatabase(consumer);// Updates Consumer in the "accounts.csv" file.
                        Console.WriteLine($"New purchase list \"{newPurchaseList.Name}\" [ID: {newPurchaseList.Id}] successfully added to {db.GetConsumer.Email}");

                        quit = true;
                        break;
                    case "3":
                        Console.WriteLine($"New list \"{newPurchaseList._name}\" discarded.");

                        return;
                    default: 
                        Console.WriteLine($"Unknown input: {input}"); 
                        break;
                }
            }
        }
        public void RemovePurchaseList(Database db, Consumer consumer) // the old DeletePurchaseList() method from <Database> class.
        {
            Console.WriteLine("Choose an ID that you want to delete:");
            Console.WriteLine();

            db.Display(consumer.ListOfPurchases, true); //< true > shows the purchase list´s IDs and the names,NO items. < false > includes the items for every purchase list


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
            }
            else Console.WriteLine($"ID [{userInput}] not found");


            index = -1;
            foreach (int i in consumer.IdsOfPurchaseLists)
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

        // EditPurchaseList(): Views and edits items in PurchaseList.
        public void EditPurchaseList(Database db)
        {
            Console.Clear();
            Console.WriteLine($"LIST OF ITEMS IN \"{_name}\":");
            Console.WriteLine();

            // db.Display(): Displays all items in current PurchaseList.
            if (_listOfItems != null) db.Display(this);
            else Console.WriteLine("<EMPTY LIST>");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("[1] Add new item.");
                Console.WriteLine("[2] Edit existing item.");
                Console.WriteLine("[3] Remove existing item.");
                Console.WriteLine("[4] Quit and return.");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": // NYI: Call method NewItemToList()
                        Console.WriteLine("Not yet implemented!");
                        break;
                    case "2": // NYI: Call method EditItemInList(), change amount & change bought status.
                        Console.WriteLine("Not yet implemented!");
                        break;
                    case "3": // NYI: Call method RemoveItemFromList()
                        Console.WriteLine("Not yet implemented!");
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine($"Unknown input: {input}");
                        break;
                }
            }
        }

        // SelectPurchaseList(): Views Consumer.ListOfPurchaseLists and returns a selected PurchaseList.
        public static PurchaseList SelectPurchaseList(Database db, Consumer consumer)
        {
            // db.Display(): Views consumer.ListOfPurchases
            db.Display(consumer.ListOfPurchases);
            Console.Write("Enter the ID number of the purchase list: ");
            int.TryParse(Console.ReadLine(), out int input);

            // Loops through consumer.ListOfPurchases to find List based on List.Id(input)
            foreach (PurchaseList pL in consumer.ListOfPurchases)
            {
                if (pL.Id == input) return pL;
            }
            return null;
        }

        public void ShareList(Database db, Consumer consumer)
        {
            Console.WriteLine();
            db.Display(db.GetConsumer.ListOfPurchases, true);
            Console.WriteLine();
            Console.WriteLine("Choose list number that you want to share:");
            int userInput = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Choosen list:");
            foreach (PurchaseList l in consumer.ListOfPurchases)
            {
                if (l.Id == userInput)
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
                            if (l.Id == userInput)
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
                            if (l.Id == userInput)
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

        public Database MergeLists(Database db, Consumer consumer)
        {
            Console.Write("IDs of two lists to be merged (write using comma: ex 100,102)");
            string input = Console.ReadLine();

            if (CheckInput(db, consumer, input))
            {
                Console.Write("Select a new name for the merged purchase list: ");
                string newName = Console.ReadLine();

                if (newName.Length > 0)
                {
                    string[] str = input.Split(','); 

                    PurchaseList mergedPurchaseList = new PurchaseList();
                    mergedPurchaseList.SetName(newName);

                    foreach (PurchaseList pl in consumer.ListOfPurchases) //retrieving list of items
                    {
                        if (pl._id == Int32.Parse(str[0])) // 670 == 670
                        {
                            foreach (Item item in pl.ListOfItems)
                            {
                                mergedPurchaseList._listOfItems.Add(item);
                            }
                            int indx = db.ListOfPurchases.IndexOf(pl); 
                            db.ListOfPurchases.RemoveAt(indx); //remove the old Purchase List from Database
                        }
                    }

                    /*foreach (PurchaseList pl in consumer.ListOfPurchases) 
                    {
                        if (pl._id == Int32.Parse(str[0])) // 670 == 670
                        {
                            int indx = consumer.ListOfPurchases.IndexOf(pl);
                            consumer.ListOfPurchases.RemoveAt(indx);
                        }
                    }*/



                    foreach (PurchaseList pl in consumer.ListOfPurchases)
                    {
                        if (pl._id == Int32.Parse(str[1]))
                        {
                            foreach (Item item in pl.ListOfItems)
                            {
                                mergedPurchaseList._listOfItems.Add(item);
                            }
                            int indx = db.ListOfPurchases.IndexOf(pl); //remove the old Purchase List from Database
                            db.ListOfPurchases.RemoveAt(indx);
                        }
                    }


                    int index = -1;
                    foreach (int i in consumer.IdsOfPurchaseLists) //removing first ID from Consumer
                    {
                        if (i == Int32.Parse(str[0])) index = consumer.IdsOfPurchaseLists.IndexOf(i);
                    }
                    if (index != -1)
                    {
                        consumer.IdsOfPurchaseLists.RemoveAt(index);                        
                        //remove also list of purchases itself

                    }
                    index = -1;
                    foreach (int i in consumer.IdsOfPurchaseLists) //removing second ID from Consumer
                    {
                        if (i == Int32.Parse(str[1])) index = consumer.IdsOfPurchaseLists.IndexOf(i);
                    }
                    if (index != -1)
                    {
                        consumer.IdsOfPurchaseLists.RemoveAt(index);                        
                    }

                    int lastExistingID = 0;
                    foreach (PurchaseList pl in db.ListOfPurchases)
                    {
                        if (pl._id > lastExistingID) lastExistingID = pl.Id;
                    }
                    mergedPurchaseList.SetID(lastExistingID+1);  // add ID to MergedList

                    consumer.IdsOfPurchaseLists.Add(lastExistingID+1); // to be able to see the changes without reopening the Console
                    consumer.ListOfPurchases.Add(mergedPurchaseList);


                    //db.SetConsumer(consumer);

                    db.ListOfPurchases.Add(mergedPurchaseList); // add the new merged purchase list to Database
                    db.UpdateFileInDataBase(1); // updating the whole file  <listOfPurchases.csv>
                    db.EditObjectInDatabase(consumer); // editing one line in <accounts.csv>

                    Console.WriteLine($"IDs [{input}] merged successfully into new ID [{mergedPurchaseList.Id}]");
                    Console.WriteLine();

                    db.SetListOfConsumers(new List<Consumer>());
                    db.SetListOfPurchases(new List<PurchaseList>());
                    db.LoadAllFromDatabase();


                    string mail = consumer.Email;

                    foreach (Consumer c in db.ListOfConsumers)
                    {
                        if (mail == c.Email)
                        {
                            Consumer cons = new Consumer(c.Email, c.Password, c.Name, c.AccountLvl, c.Points, c.IdsOfPurchaseLists);

                            List<PurchaseList> plList = new List<PurchaseList>();

                            foreach (int i in cons.IdsOfPurchaseLists)
                            {
                                foreach (PurchaseList pl in db.ListOfPurchases)
                                {
                                    if (i == pl.Id) plList.Add(pl);
                                }
                            }
                            cons.ListOfPurchases = plList;
                            db.SetConsumer(cons);
                        }
                    }
                }
            }
            else MergeLists(db, consumer);
            return db;
        }
        static bool CheckInput(Database db, Consumer consumer,string input)
        {
            bool success = false;
            string[] IDsToBeMerged = input.Split(',');

            if (input.Trim().Length == 0) return false;

           try
           {
                foreach (int i in consumer.IdsOfPurchaseLists)
                {
                    if (i == Int32.Parse(IDsToBeMerged[0]))
                        success = true;
                }
                if (!success) Console.WriteLine($"ID [{IDsToBeMerged[0]}] not registered for {consumer.Email}");
                foreach (int i in consumer.IdsOfPurchaseLists)
                {
                    if (i == Int32.Parse(IDsToBeMerged[1]))
                        success = true;
                }
                if (!success) Console.WriteLine($"ID [{IDsToBeMerged[1]}] not registered for {consumer.Email}");
           }
          catch
            { 
                
            }
           
            return success;
        }
    }
}

