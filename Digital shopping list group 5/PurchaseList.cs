using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;

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

        public Database NewPurchaseList(Database db, Consumer consumer) //Creates new PurchaseList and adds to Consumer.ListOfPurchases & Consumer.IdsOfPurchaseLists.
        {
            string input;
            bool quit = false;
            var newPurchaseList = new PurchaseList();
            var newItemList = new List<Item>();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("NEW PURCHASE LIST:");
            Console.WriteLine();

            // First option selection loop:
            while (quit == false)
            {
                Console.WriteLine("[1] Create new purchase list."); // it works
                Console.WriteLine("[2] Create new purchase list from template (existing list)"); // TBD
                Console.WriteLine("[3] Quit and return."); // it works
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Purchase list created from new list.");
                        quit = true;
                        break;
                    case "2": 
                        // Creates new instances of Item and copies Items attributes from templatePurchaseList
                        var templatePurchaseList = SelectPurchaseList(db, consumer);

                        foreach (Item item in templatePurchaseList.ListOfItems)
                        {
                            var newItem = new Item();
                            newItem.SetID(item.Id);
                            newItem.SetName(item.Name);
                            newItem.SetPrice(item.Price);
                            newItemList.Add(newItem);
                        }

                        Console.WriteLine($"Purchase list created from existing list \"{templatePurchaseList.Name}\".");

                        quit = true;
                        break;
                    case "3":
                        return db;
                    default:
                        Console.WriteLine($"Unknown input: {input}");
                        break;
                }
            }
            quit = false;

            // Assigns unique ID
            // TBD: Create method?
            int lastExistingID = 0;
            foreach (PurchaseList pl in db.AllPurchaseLists)
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
                Console.WriteLine($"[1] Edit items of \"{newPurchaseList.Name}\"."); // TBD
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
                        Console.WriteLine($"New purchase list \"{newPurchaseList.Name}\" [ID: {newPurchaseList.Id}] successfully added to {db.GetCurrentConsumer.Email}");

                        //this code repeats, rewrite
                        db.SetAllConsumers(new List<Consumer>());
                        db.SetListOfPurchases(new List<PurchaseList>());
                        db.LoadAllFromDatabase();

                        
                        foreach (Consumer c in db.AllConsumers) //update <Consumer> class => see the changes without reopening the Console
                        {
                            if (consumer.Email == c.Email)
                            {
                                Consumer cons = new Consumer(c.Email, c.Password, c.Name, c.AccountLvl, c.Points, c.IdsOfPurchaseLists);

                                List<PurchaseList> plList = new List<PurchaseList>();

                                foreach (int i in cons.IdsOfPurchaseLists)
                                {
                                    foreach (PurchaseList pl in db.AllPurchaseLists)
                                    {
                                        if (i == pl.Id) plList.Add(pl);
                                    }
                                }
                                cons.ListOfPurchases = plList;
                                db.SetCurrentConsumer(cons);
                            }
                        }                      
                        return db;
                    case "3":
                        Console.WriteLine($"New list \"{newPurchaseList._name}\" discarded.");
                        return db;
                    default: 
                        Console.WriteLine($"Unknown input: {input}"); return db;
                }
            }
            return db;
        }

        public Database RemovePurchaseList(Database db, Consumer consumer) // the old DeletePurchaseList() method from <Database> class.
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Choose an ID that you want to delete:");
            Console.WriteLine();

            db.Display(consumer.ListOfPurchases, true); //< true > shows the purchase list´s IDs and the names,NO items. < false > includes the items for every purchase list

            Console.WriteLine();
            bool success = int.TryParse(Console.ReadLine(), out int userInput);
            if (success)
            {
                int index = -1;
                foreach (PurchaseList pl in consumer.ListOfPurchases)
                {
                    if (userInput == pl.Id)
                    {
                        index = db.AllPurchaseLists.IndexOf(pl);
                    }
                }
                if (index != -1)
                {
                    db.AllPurchaseLists.RemoveAt(index); // remove the purchase list from <Database> class
                }
                else Console.WriteLine($"ID [{userInput}] not registered to {consumer.Email}");


                index = -1;
                foreach (int i in consumer.IdsOfPurchaseLists)
                {
                    if (i == userInput)
                    {
                        index = consumer.IdsOfPurchaseLists.IndexOf(i);
                        Console.WriteLine($"Purchase List with ID [{i}] successfully deleted from {consumer.Email}");
                    }
                }
                if (index != -1)
                {
                    consumer.IdsOfPurchaseLists.RemoveAt(index);  // remove purchase list´s ID from Consumer
                }

                db.EditObjectInDatabase(consumer); // editing one line in <accounts.csv>
                db.UpdateFileInDataBase(1); // update the whole <listOfPurchases.csv>

                foreach (Consumer c in db.AllConsumers) //update <Consumer> class => to see the changes without reopening the Console
                {
                    if (consumer.Email == c.Email)
                    {
                        Consumer cons = new Consumer(c.Email, c.Password, c.Name, c.AccountLvl, c.Points, c.IdsOfPurchaseLists);

                        List<PurchaseList> plList = new List<PurchaseList>();

                        foreach (int i in cons.IdsOfPurchaseLists)
                        {
                            foreach (PurchaseList pl in db.AllPurchaseLists)
                            {
                                if (i == pl.Id) plList.Add(pl);
                            }
                        }
                        cons.ListOfPurchases = plList;
                        db.SetCurrentConsumer(cons);
                    }
                }
            } else Console.WriteLine("wrong input");

            return db;
        }


        //KOLLA HIT
        public void EditPurchaseList(Database db) // EditPurchaseList(): Views and edits items in PurchaseList.
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
                Console.WriteLine("[2] Remove existing item.");
                Console.WriteLine("[3] Quit and return.");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddItemToPurchaseList(db);
                        break;
                    case "2":
                        DeleteItemFromPurchaseList(db);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine($"Unknown input: {input}");
                        break;
                }
            }
        }

        public Database ShareList(Database db, Consumer consumer)
        {
            Console.WriteLine();
            db.Display(db.GetCurrentConsumer.ListOfPurchases, true);
            Console.WriteLine();
            Console.WriteLine("Choose ID that you want to share:");
            string inputID = Console.ReadLine();
            bool success = CheckInput(consumer, inputID, 2);
            if (success)
            {
                Console.WriteLine();
                Console.WriteLine("Send to:");
                Console.WriteLine();
                Console.WriteLine("[1] Registered member");
                Console.WriteLine("[2] Non registered member");
                string str = Console.ReadLine();

                if (int.TryParse(str, out int result))
                {
                    switch (result)
                    {
                        case 1:
                            Console.WriteLine();
                            Console.WriteLine("Receiver´s email address: ");

                            string receiverEmail = Console.ReadLine();
                            if (!String.IsNullOrEmpty(receiverEmail))
                            {
                                bool emailFound = false;
                                bool receiverAlreadyHaveThatPurchaseList = false;
                                Consumer receiverOfNewPurchaseList;

                                foreach (Consumer c in db.AllConsumers)
                                {
                                    if (receiverEmail.Trim() == c.Email) // finding that account in DB
                                    {
                                        emailFound = true;
                                        foreach (int i in c.IdsOfPurchaseLists)
                                        {
                                            if (i == Int32.Parse(inputID)) // if the ID found...
                                            {
                                                receiverAlreadyHaveThatPurchaseList = true; 
                                                Console.WriteLine($"{receiverEmail} already have that purchase list ID [{i}]");                                                
                                                break;
                                            }
                                        }
                                        if (!receiverAlreadyHaveThatPurchaseList)
                                        {
                                            c.IdsOfPurchaseLists.Add(Int32.Parse(inputID));
                                            db.EditObjectInDatabase(c); // editing one line in <accounts.csv> 
                                        }
                                        
                                    }
                                    
                                }
                                

                                if (!emailFound) Console.WriteLine($"{receiverEmail} not registered in our system");
                                if ((emailFound) && (!receiverAlreadyHaveThatPurchaseList))
                                {                                    
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"ID [{Int32.Parse(inputID)}] successfully shared with {receiverEmail}");
                                    Console.WriteLine();
                                }
                            }

                            // UPDATE APPLICATION 
                            db.SetAllConsumers(new List<Consumer>());
                            db.SetListOfPurchases(new List<PurchaseList>());
                            db.LoadAllFromDatabase();
                            return db;

                        case 2: // <Inbox> file is a simulated emailbox 
                            Console.WriteLine();
                            Console.WriteLine("To email:");
                            string emailTo = Console.ReadLine();
                            if (!String.IsNullOrEmpty(emailTo))
                            {
                                using (StreamWriter sw = new StreamWriter("Path/inbox.csv"))
                                {
                                    foreach (PurchaseList l in consumer.ListOfPurchases)
                                    {
                                        if (l.Id == Int32.Parse(inputID))
                                        {
                                            sw.WriteLine($"{consumer.Email};{emailTo};{l}");
                                        }
                                    }
                                }
                                Console.Clear();
                                Console.WriteLine($"Purchase list [{inputID}] was sent to {emailTo}");
                            }
                            else Console.WriteLine("empty input :(");
                            break;
                        default:
                            Console.WriteLine($"Wrong input {inputID}"); break;
                    }
                }
                else Console.WriteLine("Wrong input!");


            }
            else Console.WriteLine($"ID [{inputID}] not registered at {consumer.Email}");
            return db;
        }
        public Database MergeLists(Database db, Consumer consumer)
        {
            Console.WriteLine("q to go back;");
            Console.Write("IDs of two lists to be merged (write using comma: ex 100,102): ");
            
            string input = Console.ReadLine();

            if ((CheckInput(consumer, input, 1)) && (input.Trim() == "q")) { return db; }

            else if (CheckInput(consumer, input, 1)) // 1 for Merge Lists 
            {
                Console.Write("Select a new name for the merged purchase list: ");
                string newName = Console.ReadLine();

                if (newName.Length > 0)
                {
                    string[] str = input.Split(',');

                    PurchaseList mergedPurchaseList = new PurchaseList();
                    mergedPurchaseList.SetName(newName);

                    foreach (PurchaseList pl in consumer.ListOfPurchases)
                    {
                        if (pl._id == Int32.Parse(str[0]))  //remove that first old Purchase List from <Database> class
                        {
                            foreach (Item item in pl.ListOfItems)
                            {
                                mergedPurchaseList._listOfItems.Add(item);
                            }
                            int indx = db.AllPurchaseLists.IndexOf(pl);
                            db.AllPurchaseLists.RemoveAt(indx);
                        }
                        else if (pl._id == Int32.Parse(str[1]))  //remove that second old Purchase List from <Database> class
                        {
                            foreach (Item item in pl.ListOfItems)
                            {
                                mergedPurchaseList._listOfItems.Add(item);
                            }
                            int indx = db.AllPurchaseLists.IndexOf(pl);
                            db.AllPurchaseLists.RemoveAt(indx);
                        }
                    }



                    int index = -1;
                    foreach (int i in consumer.IdsOfPurchaseLists) //remove the first ID from Consumer
                    {
                        if (i == Int32.Parse(str[0])) index = consumer.IdsOfPurchaseLists.IndexOf(i);
                    }
                    if (index != -1)
                    {
                        consumer.IdsOfPurchaseLists.RemoveAt(index);

                    }
                    index = -1;
                    foreach (int i in consumer.IdsOfPurchaseLists) //remove the second ID from Consumer
                    {
                        if (i == Int32.Parse(str[1])) index = consumer.IdsOfPurchaseLists.IndexOf(i);
                    }
                    if (index != -1)
                    {
                        consumer.IdsOfPurchaseLists.RemoveAt(index);
                    }



                    int lastExistingID = 0;
                    foreach (PurchaseList pl in db.AllPurchaseLists)
                    {
                        if (pl._id > lastExistingID) lastExistingID = pl.Id;
                    }
                    mergedPurchaseList.SetID(lastExistingID + 1);  // add ID to MergedList

                    consumer.IdsOfPurchaseLists.Add(lastExistingID + 1);
                    consumer.ListOfPurchases.Add(mergedPurchaseList);
                    db.EditObjectInDatabase(consumer); // editing one line in <accounts.csv>


                    db.AllPurchaseLists.Add(mergedPurchaseList); // add the new merged purchase list to <Database> class
                    db.UpdateFileInDataBase(1); // update the whole file  <listOfPurchases.csv>

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"IDs [{input}] merged successfully into new ID [{mergedPurchaseList.Id}]");
                    Console.WriteLine();


                    //This code repeats, rewrite.

                    // UPDATE APPLICATION METHOD 
                    db.SetAllConsumers(new List<Consumer>());
                    db.SetListOfPurchases(new List<PurchaseList>());
                    db.LoadAllFromDatabase();



                    foreach (Consumer c in db.AllConsumers) //update <Consumer> class => see the changes without reopening the Console
                    {
                        if (consumer.Email == c.Email)
                        {
                            Consumer cons = new Consumer(c.Email, c.Password, c.Name, c.AccountLvl, c.Points, c.IdsOfPurchaseLists);

                            List<PurchaseList> plList = new List<PurchaseList>();

                            foreach (int i in cons.IdsOfPurchaseLists)
                            {
                                foreach (PurchaseList pl in db.AllPurchaseLists)
                                {
                                    if (i == pl.Id) plList.Add(pl);
                                }
                            }
                            cons.ListOfPurchases = plList;
                            db.SetCurrentConsumer(cons);
                        }
                    }
                }
            }
            else MergeLists(db, consumer);
            return db;
        }


        // TBD: SetQuantity not updating? 
        private void AddItemToPurchaseList(Database db)
        {
            db.Display(db.AllItems);

            Console.Write("Enter id of item to add to list: ");
            bool idParse = int.TryParse(Console.ReadLine(), out int id);

            if (!idParse)
            {
                Console.WriteLine("Wrong format, add item cancelled!");
                return;
            }

            foreach (var item in db.AllItems)
            {
                if (item.Id == id)
                {
                    var newItem = new Item();
                    Console.Write($"Enter amount of \"{item.Name}\" to add to list \"{Name}\": ");
                    bool amountParse = int.TryParse(Console.ReadLine(), out int amount);

                    if (!amountParse)
                    {
                        Console.WriteLine("Wrong format, add item cancelled!");
                        return;
                    }

                    newItem.SetID(item.Id); // UNIQUE ID FOR UNIQUE INSTANCE??
                    newItem.SetName(item.Name);
                    newItem.SetPrice(item.Price);
                    newItem.SetQuantity(amount);
                    newItem.SetIsBought(false);
                    _listOfItems.Add(newItem);

                    Console.WriteLine($"Item \"{item.Name}\" added to list \"{Name}\"");
                    return;
                }
            }
            Console.WriteLine("Item not found, try again!");
        }

        private void DeleteItemFromPurchaseList(Database db)
        {
            db.Display(this);
            Console.Write("Enter ID of item to remove: ");
            bool idParse = int.TryParse(Console.ReadLine(), out int id);

            if (!idParse)
            {
                Console.WriteLine("Wrong format, delete item cancelled!");
                return;
            }

            foreach (var item in ListOfItems)
            {
                if (item.Id == id)
                {
                    Console.WriteLine($"Item \"{item.Name}\" removed from list \"{Name}\"");
                    ListOfItems.Remove(item);
                    return;
                }
            }
            Console.WriteLine("Item not found, try again!");

        }
        static bool CheckInput(Consumer consumer, string input, int actionNumber)
        // <1> to merge; <2> to share list within application, <3> to delete PurchaseList
        {
            bool success1 = false;
            bool success2 = false;
            string[] IDsToBeMerged = input.Split(',');

            if (input.Trim().Length == 0) return false;

            if (actionNumber == 1)
            {
                if (input.Trim() == "q") { return true; }
                try
                {
                    foreach (int i in consumer.IdsOfPurchaseLists)
                    {
                        if (i == Int32.Parse(IDsToBeMerged[0])) success1 = true;
                        else if (i == Int32.Parse(IDsToBeMerged[1])) success2 = true;
                    }
                    if (!success1 || !success2) Console.WriteLine($"ID [{IDsToBeMerged[0]}] or [{IDsToBeMerged[1]}]  not registered for {consumer.Email}");
                }
                catch
                {

                }

                return (success1 && success2);
            }
            else if (actionNumber == 2)
            {
                try
                {
                    int i = Int32.Parse(input);
                    foreach (PurchaseList pl in consumer.ListOfPurchases)
                    {
                        if (i == pl.Id)
                            return true;
                    }
                }
                catch { return false; }
            }
            else if (actionNumber == 3)
            { }

            return false;

        }


        // SelectPurchaseList(): Views Consumer.ListOfPurchaseLists and returns a selected PurchaseList.
        static PurchaseList SelectPurchaseList(Database db, Consumer consumer)
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
    }
}

