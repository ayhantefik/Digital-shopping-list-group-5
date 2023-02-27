using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Dynamic;

namespace Digital_shopping_list_group_5
{

    // Purchase is the old "Receipts"...
    public class Purchase
    {
        DateTime dateCheck;
        int ID = -1;
        List<PurchaseList> listOfPurchases = new List<PurchaseList>();
        double totalPrice;

        public Purchase() { }

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
        public DateTime DateCheck { get; } 
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

        public void MakeAPurchase(Database db, Consumer consumer)
        {
            string userInput;
            //Database db = new Database();
            db.LoadAllFromDatabase();

            var makePurchase = new Purchase();
            var newListOfReceipts = new List<PurchaseList>();

            Console.WriteLine("Choose an existing purschase list.");
            db.Display(db, db.GetConsumer.ListOfPurchases, true);
            userInput = Console.ReadLine();

            if (db.GetPurchaseListId != null)
            {
                //Show the purchase list with the correct purchase id
                //PurchaseList.SelectPurchase();
                Console.WriteLine("Do you want to make a purchase?");
                Console.WriteLine("Write [Y] for YES and [N] for NO.");
                userInput = Console.ReadLine();
                if(userInput == "Y")
                {
                    // assign the unique ID to the receipt
                   int lastExistingID = 0;
                    foreach (Purchase p in db.ListOfReceipts)
                    {
                        if (p.ID > lastExistingID) lastExistingID = p.ID;
                    }
                    lastExistingID += 1;
                    ID = makePurchase.SetID(lastExistingID);
                    
                    makePurchase = (ID, DateCheck); 
                    //Create an purchase with datetime and listOfPurchases and totalPrice
                   // add the purchase to the listofreceipts

                    consumer.ListOfReceipts.Add(makePurchase);// add list of receipts to consumer?
                }

            }
            else { Console.WriteLine($"Unknown input: {userInput}"); }
        }
    }

}
