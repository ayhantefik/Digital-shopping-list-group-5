using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Dynamic;
using System.Security.Cryptography;
using System.Runtime.Remoting.Messaging;

namespace Digital_shopping_list_group_5
{

    public class Purchase
    {
        string email;
        DateTime dateCheck;
        int ID = -1;
        List<PurchaseList> _allPurchaseLists = new List<PurchaseList>();

        double totalPrice;
        public List<PurchaseList> ListofPurchasesReceipt => _allPurchaseLists;

        public Purchase() { }
        public Purchase(string email, int iD, DateTime dateCheck, List<PurchaseList> _allPurchaseLists)
        {
            this.email = email;
            this.dateCheck = dateCheck;
            this.ID = iD;
            this._allPurchaseLists = _allPurchaseLists;
        }

        //=======================================================================================
        public string Email => email;
        public int Id => ID;
        public DateTime DateCheck => dateCheck;
        public void SetDateTime(DateTime value) => dateCheck =value;
        public int SetID(int value) => ID = value;
        public double SetTotalPrice(double value) => totalPrice = value;
        public List<PurchaseList> ListOfPurchases => _allPurchaseLists; public void SetListOfPurchases(List<PurchaseList> value) => _allPurchaseLists = value;
        public override string ToString() 
        {
            return $" {ID};{DateTime.Now};{_allPurchaseLists};{totalPrice}";
        }
        //=======================================================================================
        

        public Database MakePurchase(Database db, Consumer consumer)
        {            
            db.Display(consumer.ListOfPurchases, true);
            Console.WriteLine();
            
            //int index = -1;
            /*foreach (PurchaseList pl in consumer.ListOfPurchases)
            {
                if (userInput == pl.Id)
                {
                    index = consumer.ListOfPurchases.IndexOf(pl);
                    db.Display(pl);
                }
            }*/
            Console.WriteLine("Choose an existing purschase list. Enter the ID number of the purchase list: ");           
            int userInput = Int32.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Do you want to make a purchase?");
            Console.WriteLine("Write [6] for YES and [7] for NO.");
            //string uInput = Console.ReadLine();
            int userInput2 = Int32.Parse(Console.ReadLine());
            //var newListOfReceipts = new List<PurchaseList>();
            if (userInput2 == 6)
            {
                // assign the unique ID to the receipt
                int lastExistingID = 0;
                foreach (Purchase p in db.AllPurchases)
                {
                    if (p.Id > lastExistingID) lastExistingID = p.Id;
                }
                lastExistingID += 1;
                DateTime newpurchasedate = DateTime.Now;
                using (var sw = new StreamWriter("Path/Purchases.csv", true))
                {
                    foreach (PurchaseList l in consumer.ListOfPurchases)
                    {
                        if (l.Id == userInput)
                        {
                            l.SetID(lastExistingID);
                            sw.WriteLine($"{consumer.Email};{lastExistingID};{newpurchasedate.ToString("dd-MM-yyyy HH:mm:ss")};{l}");
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Purchase is done and receipt is saved! The items have been ticked off.");


            }
            else if (userInput2 == 7)
            {
                Console.WriteLine();
                Console.WriteLine("Ok, you do not want to make a purchase.");
                Console.WriteLine("Please go back to the menu and choose your next action.");
            }
            Console.WriteLine();



            //UPDATE APPLICATION
            db.SetAllPurchases(new List<Purchase>());
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
        }       
        public Database ShowReceipts(Database db, Consumer consumer)
        {
            //UPDATE APPLICATION
            db.SetAllPurchases(new List<Purchase>());
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



            int numberOfReceits = 0;
            foreach (Purchase pw in db.AllPurchases)
            {
                if (pw.Email == db.GetCurrentConsumer.Email)
                {
                    Console.WriteLine($"[{pw.Id}] {pw.DateCheck}");
                    numberOfReceits++;
                }
                /*else
                {
                    Console.WriteLine();
                    Console.WriteLine("You don't have registered receipts!");
                    Console.WriteLine();
                    noreceipt = true;
                }*/
            }

            if (numberOfReceits > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Choose receipt number:");
                int receiptnumInput = Int32.Parse(Console.ReadLine());
                Console.WriteLine();
                double sum = 0;
                foreach (Purchase pw2 in db.AllPurchases)
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
            else Console.WriteLine($"No receipts registered for {db.GetCurrentConsumer.Email} ");
            return db;
        }
    }
}


