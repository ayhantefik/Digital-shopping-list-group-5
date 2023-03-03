using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Digital_shopping_list_group_5
{
    public class Database
    {
        // TBD: Remove List<string> fields...
        public List<string> itemlist = new List<string>();
        public List<string> purchaselists = new List<string>();

        // Static fields
        private readonly string _itemsFile = "Path/Items.csv";
        private readonly string _purchaseListsFile = "Path/PurchaseLists.csv";
        private readonly string _purchasesFile = "Path/Purchases.csv";  
        private readonly string _consumersFile = "Path/Consumers.csv";

        // Fields
        List<Item> _allItems = new List<Item>(); // All available items registered.
        List<PurchaseList> _allPurchaseLists = new List<PurchaseList>(); // All purchase lists registered.
        List<Purchase> _allPurchases = new List<Purchase>(); // All purchases(receipts) registered.
        List<Consumer> _allConsumers = new List<Consumer>(); // All consumers registered.
        List<PurchaseList> _donePurchase = new List<PurchaseList>();

        Consumer _currentConsumer = null;
        PurchaseList _currentPurchaseList = null;




        // Properties (Getters & Setters)
        public List<Item> AllItems => _allItems; //public void SetAllItems(List<Item> value) => _allItems = value;
        public void SetAllItems(List<Item> value) => _allItems = value;
        public List<Consumer> AllConsumers => _allConsumers;  
        public void SetAllConsumers(List<Consumer> value) => _allConsumers = value;
        public List<PurchaseList> AllPurchaseLists => _allPurchaseLists; 
        public void AddToPurchaseLists(PurchaseList value) => _allPurchaseLists.Add(value);
        public void SetListOfPurchases(List<PurchaseList> value) => _allPurchaseLists = value;
        public List<Purchase> AllPurchases => _allPurchases; public void SetAllPurchases(List<Purchase> value) => _allPurchases = value;
        public void AddToPurchases(Purchase value) => _allPurchases.Add(value);

        public Consumer GetCurrentConsumer => _currentConsumer;  
        public void SetCurrentConsumer(Consumer value) => _currentConsumer = value;
        public PurchaseList GetCurrentPurchaseList => _currentPurchaseList;






        //==================================================================================================================================
        //File-database interaction

        public void LoadAllFromDatabase() //rewritten and merged
        {            
            StreamReader str;
            string path = "";

            path = _purchaseListsFile;
            using (str = new StreamReader(path))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {
                    string[] splittedObject = line.Split(';');

                    List<Item> listOfItems = new List<Item>();
                    if (splittedObject.Length > 2)
                    {
                        for (int i = 2; i < splittedObject.Length-1; i+=5)
                        {
                            Item item = new Item(Int32.Parse(splittedObject[i]), Int32.Parse(splittedObject[i + 1]),
                                Int32.Parse(splittedObject[i + 2]), splittedObject[i + 3], bool.Parse(splittedObject[i+4]));                            
                            listOfItems.Add(item);    
                        }
                    }
                    PurchaseList pl = new PurchaseList(Int32.Parse(splittedObject[0]),splittedObject[1],listOfItems);                    
                    _allPurchaseLists.Add(pl);
                }
                
            }
            path = _consumersFile;
            using (str = new StreamReader(path))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {
                    string[] splittedObject = line.Split(';');
                    List<int> IDsOfPurchases = new List<int>(); // IDs of purchase lists that belong to the account

                    if (splittedObject.Length > 5)
                    {
                        for (int i = 5; i < splittedObject.Length-1; i++)
                        {
                            IDsOfPurchases.Add(Int32.Parse(splittedObject[i]));
                            //Console.WriteLine
                        }
                    }
                    Consumer acc = new Consumer(splittedObject[0], splittedObject[1], splittedObject[2],
                        Int32.Parse(splittedObject[3]), Int32.Parse(splittedObject[4]), IDsOfPurchases);

                    /*foreach (PurchaseList pl in _allPurchaseLists)
                    {
                        foreach (int i in IDsOfPurchases)
                        {
                            if (i == pl.Id) acc.ListOfPurchases.Add(pl);
                        }
                    }*/
                    
                    _allConsumers.Add(acc);                    
                }
            }
            path = _itemsFile;
            using (str = new StreamReader(path))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {
                    var newItem = new Item();
                    string[] splittedObject = line.Split(';');
                    if (splittedObject.Length > 2)
                    {
                        newItem.SetID(int.Parse(splittedObject[0]));
                        newItem.SetName(splittedObject[1]);
                        newItem.SetPrice(double.Parse(splittedObject[2]));
                        AllItems.Add(newItem);
                    }
                }
            }
            path = _purchasesFile;
            using (str = new StreamReader(path))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {
                    string[] splittedObject = line.Split(';');
                    //List<PurchaseList> listOfPurchases1 = new List<PurchaseList>();
                    List<Item> listOfItems1 = new List<Item>();
                    if (splittedObject.Length > 5)
                    {
                        for (int i = 5; i < splittedObject.Length - 1; i += 5)
                        {
                            Item item1 = new Item(Int32.Parse(splittedObject[i]), Int32.Parse(splittedObject[i + 1]),
                                Int32.Parse(splittedObject[i + 2]), splittedObject[i + 3], true);
                            listOfItems1.Add(item1);
                        }
                    }
                    PurchaseList purchaseList = new PurchaseList(Int32.Parse(splittedObject[3]), splittedObject[4], listOfItems1);
                    _donePurchase.Add(purchaseList);
                    Purchase testafiesta = new Purchase(splittedObject[0], Int32.Parse(splittedObject[1]), DateTime.ParseExact(splittedObject[2], "dd-M-yyyy HH:mm:ss", CultureInfo.InvariantCulture), _donePurchase);
                    _allPurchases.Add(testafiesta);
                    //PurchaseList purchaseList = new PurchaseList(Int32.Parse(splittedObject[0]), splittedObject[1], splittedObject[2], listOfPurchases);

                }
            }

            /*path = "Path/listOfReceipts.csv"; // TBD
            using (str = new StreamReader(path))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {
                    string[] splittedObject = line.Split(';');
                    Purchase receipt = new Purchase(); // TBD
                    listOfReceipts.Add(receipt);
                }
            }*/

        }

        public void AddObjectToDatabase(object obj)
        {
            if (obj.GetType() == typeof(PurchaseList))
            {
                string str = obj.ToString();
                using (var streamwriter = new StreamWriter(_purchaseListsFile, true))
                {
                    streamwriter.WriteLine( str);
                }
            }
             
            else if (obj.GetType() == typeof(Consumer))
            {
                string str = obj.ToString();
                using (var streamwriter = new StreamWriter(_consumersFile, true))
                {                    
                    streamwriter.WriteLine(str);
                }
            }
        }
        public void EditObjectInDatabase(object obj) // edits a line that exists in one of the files
        {
            if (obj.GetType() == typeof(PurchaseList))
            {
                string strObj = obj.ToString();
                string[] arrObj = obj.ToString().Split(';');
                string[] arrLine = File.ReadAllLines(_purchaseListsFile);

                for (int i = 0; i < arrLine.Length; i++)
                {
                    string[] str = arrLine[i].Split(';');
                    if (str[0] == arrObj[0])
                    {
                        arrLine[i] = strObj;
                    }
                }
                File.WriteAllLines(_purchaseListsFile, arrLine);
            }

            else if (obj.GetType() == typeof(Consumer))
            {
                string strObj = obj.ToString();
                string[] arrObj = obj.ToString().Split(';');
                string[] arrLine = File.ReadAllLines(_consumersFile);

                for (int i = 0; i < arrLine.Length; i++)
                {
                    string[] str = arrLine[i].Split(';');
                    if (str[0] == arrObj[0])
                    {
                        arrLine[i] = strObj;
                    }
                }
                File.WriteAllLines(_consumersFile, arrLine);
            }

            else if (obj.GetType() == typeof(Purchase)) // TBD
            { }
        }
        public void UpdateFileInDataBase(int number) // 1 for <listOfPurchases.csv, 2 for <accounts.csv>, 3 for <listOfReceipts.csv>
        {
            if (number == 1)
            {

                using (var streamwriter = new StreamWriter(_purchaseListsFile, false))
                {
                    foreach (PurchaseList pl in _allPurchaseLists)
                    {
                        streamwriter.WriteLine(pl.ToString());
                    }

                }
            }
            else if (number == 2)
            {
                using (var streamwriter = new StreamWriter(_consumersFile, false))
                {
                    //streamwriter.Write(string.Empty);
                    //streamwriter.Flush();                    

                    foreach (Consumer c in _allConsumers)
                    {
                        streamwriter.WriteLine(c.ToString()); ;
                    }

                }
            }

            else if (number == 3) { }// TBD 
            

        }

        //================================================================================================================================







        //================================================================================================================================
        //manipulating with the objects: no interaction with the file-database
        public void Display(object obj,bool displayExtended = false) 
        {
            //Console.Clear();
            // Displays the purchase lists pinned to the loggedIn Consumer, NO items.
            if (obj.GetType() == typeof(List<PurchaseList>) && !displayExtended) 
            {
                foreach (PurchaseList list in GetCurrentConsumer.ListOfPurchases) 
                {
                    Console.WriteLine($"[{list.Id}] <{list.Name}>");
                }
            }

            // Displays the purchase lists pinned to the loggedIn Consumer and their items.
            else if (obj.GetType() == typeof(List<PurchaseList>) && displayExtended)
            {
                foreach (PurchaseList pl in GetCurrentConsumer.ListOfPurchases) 
                {
                    Console.Write($"[{pl.Id}] <{pl.Name}>: ");

                    foreach (Item item in pl.ListOfItems)
                    {
                        Console.Write(item.Name + ", ");
                    }
                    Console.WriteLine();
                    //Console.WriteLine($"[{list.Id}] <{list.Name}>");
                }
            }

            // Displays items in a specific Consumer PurchaseList.
            else if (obj.GetType() == typeof(PurchaseList))
            {
                PurchaseList pL = (PurchaseList)obj;

                Console.WriteLine("ID: |BOUGHT: |AMOUNT: |ITEM:         |PRICE: ");

                foreach (Item item in pL.ListOfItems)
                {
                    Console.WriteLine($"{item.Id,-4}|{item.IsBought,-8}|{item.Quantity,-8}|{item.Name, -14}|{item.Price}");
                }
            }

            // Displays all consumers in ListOfConsumers.
            else if (obj.GetType() == typeof(List<Consumer>))
            {
                foreach (Consumer c in AllConsumers)
                {
                    Console.WriteLine($"{c.Email}, {c.Name}");
                }
            }

            else if (obj.GetType() == typeof(List<Item>))
            {
                Console.WriteLine("ID: |NAME:         |PRICE:");
                foreach (Item i in AllItems)
                {
                    Console.WriteLine($"{i.Id, -4}{i.Name, -14}{i.Price}");
                }
            }

            else if (obj.GetType() == typeof(Purchase)) { }

            //else if (obj.GetType() == typeof(List<Purchase>))
            //{
            //    foreach (Purchase p in GetConsumer.ListOfReceipts)
            //    {
            //        Console.WriteLine($"{p.DateCheck}");
            //        foreach (PurchaseList pl1 in GetConsumer.listOfPurchases)
            //        {
            //            Console.WriteLine($"{pl1.Name}");
            //        }
            //    }
                
            //}

        }

        public void ShowReceipts(Consumer consumer)
        {
            int numberOfReceits = 0;
            foreach (Purchase pw in AllPurchases)
            {
                if (pw.Email == consumer.Email)
                {
                    Console.WriteLine();
                    Console.WriteLine($"[{pw.Id}] {pw.DateCheck}");
                    numberOfReceits++;
                }
            }

            if (numberOfReceits > 0)
            {
                Console.WriteLine();
                Console.Write("Choose receipt number: ");
                int receiptnumInput = Int32.Parse(Console.ReadLine()); //check input
                Console.WriteLine();
                double sum = 0;
                foreach (Purchase pw2 in _allPurchases)
                {
                    if (pw2.Id == receiptnumInput && pw2.Email == consumer.Email)
                    {
                        foreach (PurchaseList rlist in pw2.ListofPurchasesReceipt)
                        {
                            if (rlist.Id == receiptnumInput)
                            {
                                foreach (Item ireceipt in rlist.ListOfItems)
                                {
                                    if (ireceipt.IsBought == true)
                                    {
                                        Console.WriteLine($"{ireceipt.Name,-20}      {ireceipt.Quantity}*{ireceipt.Price} = {ireceipt.Quantity * ireceipt.Price}");
                                        sum += ireceipt.Quantity * ireceipt.Price;
                                    }
                                }
                            }
                        }
                        Console.WriteLine();
                        string totalt = $"Totalt:";
                        Console.WriteLine($"{totalt,+30} {sum}");
                        Console.WriteLine();
                    }

                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"No receipts registered for {GetCurrentConsumer.Email} ");
                Console.WriteLine();
            }
        }


    }
}

