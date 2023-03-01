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

namespace Digital_shopping_list_group_5
{

    // Purchase is the old "Receipts"...
    public class Purchase
    {
        string email;
        DateTime dateCheck;
        int ID = -1;
        List<PurchaseList> listOfPurchases = new List<PurchaseList>();
        double totalPrice;

        public Purchase() { }
        public Purchase(string email, int iD, DateTime dateCheck, List<PurchaseList> listOfPurchases)
        {
            this.email = email;
            this.dateCheck = DateTime.Now;
            this.ID = iD;
            this.listOfPurchases = listOfPurchases;
        }

        public Purchase(int iD, DateTime dateCheck, List<PurchaseList> listOfPurchases, double totalPrice)
        {
            this.dateCheck = DateTime.Now;
            this.ID = iD;
            this.listOfPurchases = listOfPurchases;
            this.totalPrice = totalPrice;
        }
        public Purchase(int iD, DateTime dateCheck, List<PurchaseList> listOfPurchases)
        {
            this.dateCheck = DateTime.Now;
            this.ID = iD;
            this.listOfPurchases = listOfPurchases;
        }

        //=======================================================================================
        public int Id => ID;
        public DateTime DateCheck { get; }
        public void SetDateTime(DateTime value) => dateCheck =value;
        public int SetID(int value) => ID = value;
        public double SetTotalPrice(double value) => totalPrice = value;
        public List<PurchaseList> ListOfPurchases => listOfPurchases; public void SetListOfPurchases(List<PurchaseList> value) => listOfPurchases = value;
        //=======================================================================================
        public override string ToString() 
        {
            return $" {ID};{DateTime.Now};{listOfPurchases};{totalPrice}";
        }

        void SaveToDb(Object obj) // Move to Database class
        {
            string str = $"{DateTime.Now};{ID};{listOfPurchases};{totalPrice};";

            using (var streamWriter = new StreamWriter(@"Path/listOfReceipts.csv", true))
            {
                streamWriter.WriteLine(str);
            }
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.Write("SUCCESS: ");
            //Console.WriteLine(str);
            //Console.ResetColor();

        }
        List<Object> LoadFromDb() // move to Database class
        {
            List<Object> listOfItems = new List<Object>();

            using (StreamReader str = new StreamReader(@"Path/listOfReceipts.csv"))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {

                    listOfItems.Add(line);
                }

            }
            return listOfItems;
        }

        public void Display() // Will this be the same as PrintReceipt?
        {
            //NYI
            ToString();
        }
        public void Remove()
        {
            throw new NotImplementedException();
        }

        //public void MakeAPurchase(Database db, Consumer consumer)
        //{
        //    string userInput;
        //    db.LoadAllFromDatabase();

        //    var makePurchase = new Purchase();
        //    var newListOfReceipts = new List<PurchaseList>();

        //    db.Display(consumer.ListOfPurchases);
        //    Console.WriteLine("Choose an existing purschase list. Enter the ID number of the purchase list: ");
        //    PurchaseList.SelectPurchaseList(db, consumer);
        //    userInput = Console.ReadLine();

        //    if (db.GetPurchaseListId != null)
        //    {
        //        //Show the purchase list with the correct purchase id
        //        Console.WriteLine();
        //        db.Display(consumer.ListOfPurchases);
        //        Console.Write("Enter the ID number of the purchase list: ");
        //        int.TryParse(Console.ReadLine(), out int input);

        //        // Loops through consumer.ListOfPurchases to find List based on List.Id(input)
        //        foreach (PurchaseList pL in consumer.ListOfPurchases)
        //        {
        //            if (pL.Id == input) return pL;
        //        }
        //        return null;

        //        Console.WriteLine("Do you want to make a purchase?");
        //        Console.WriteLine("Write [Y] for YES and [N] for NO.");
        //        userInput = Console.ReadLine();
        //        if (userInput == "Y")
        //        {
        //            // assign the unique ID to the receipt
        //            int lastExistingID = 0;
        //            foreach (Purchase p in db.ListOfReceipts)
        //            {
        //                if (p.ID > lastExistingID) lastExistingID = p.ID;
        //            }
        //            lastExistingID += 1;
        //            ID = makePurchase.SetID(lastExistingID);

        //            makePurchase = (ID, DateTime.Now, );
        //            //Create an purchase with datetime and listOfPurchases and totalPrice
        //            // add the purchase to the listofreceipts

        //            /* consumer.ListOfReceipts.Add(makePurchase);*/// add list of receipts to consumer?
        //        }

        //    }
        //    else { Console.WriteLine($"Unknown input: {userInput}"); }
        //

        public void MakePurchase(Database db, Consumer consumer)
        {
            Console.WriteLine("Choose an existing purschase list. Enter the ID number of the purchase list: ");
            Console.WriteLine();
            db.Display(consumer.ListOfPurchases, true);
            Console.WriteLine();
            int userInput = Int32.Parse(Console.ReadLine());
            int index = -1;
            foreach (PurchaseList pl in consumer.ListOfPurchases)
            {
                if (userInput == pl.Id)
                {
                    index = consumer.ListOfPurchases.IndexOf(pl);
                    db.Display(pl);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Do you want to make a purchase?");
            Console.WriteLine("Write [6] for YES and [7] for NO.");
            //string uInput = Console.ReadLine();
            int userInput2 = Int32.Parse(Console.ReadLine());
            var newListOfReceipts = new List<PurchaseList>();
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
                using (var sw = new StreamWriter("Path/listOfReceipts.csv", true))
                {
                    foreach (PurchaseList l in consumer.ListOfPurchases)
                    {
                        if (l.Id == userInput)
                        {
                            sw.WriteLine($"{consumer.Email};{lastExistingID};{newpurchasedate.ToString("dd/M/yyy")};{l}");
                            sw.Close();
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Purchase is done and receipt is saved!");


            }
            else if (userInput2 == 7)
            {
                Console.WriteLine();
                Console.WriteLine("Ok, you do not want to make a purchase.");
                Console.WriteLine("Please go back to the menu and choose your next action.");
            }

            Console.WriteLine();
        }
    }
}


