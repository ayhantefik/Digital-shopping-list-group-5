using System;
using System.Collections.Generic;
using System.IO;


namespace Digital_shopping_list_group_5
{

    // Purchase is the old "Receipts"...
    public class Purchase
    {
        int _id;
        string _email;
        DateTime _dateCheck;
        PurchaseList _purchaseList;
        double _totalPrice; // USE??

        public Purchase() { }
        public Purchase(string email, int iD, DateTime dateCheck, PurchaseList purchaseList)
        {
            _email = email;
            _dateCheck = DateTime.Now;
            _id = iD;
            _purchaseList = purchaseList;
        }
        //public Purchase(int iD, DateTime dateCheck, PurchaseList purchaseList, double totalPrice)
        //{
        //    this.dateCheck = DateTime.Now;
        //    this.ID = iD;
        //    this._purchaseList = purchaseList;
        //    this.totalPrice = totalPrice;
        //}
        //public Purchase(int iD, DateTime dateCheck, PurchaseList purchaseList)
        //{
        //    this.dateCheck = DateTime.Now;
        //    this.ID = iD;
        //    this._purchaseList = purchaseList;
        //}
        public string Email => _email;
        public string SetEmail(string value) => _email = value;
        public int Id => _id;
        public DateTime DateCheck => _dateCheck;
        public void SetDateTime(DateTime value) => _dateCheck =value;
        public int SetID(int value) => _id = value;
        public double SetTotalPrice(double value) => _totalPrice = value;
        public PurchaseList PurchaseList => _purchaseList; public void SetPurchaseList(PurchaseList value) => _purchaseList = value;
        public override string ToString() 
        {
            return $"{_email};{_id};{_dateCheck.ToString().Remove(10)};{_purchaseList}";
        }

        //void SaveToDb(Object obj) // Move to Database class
        //{
        //    string str = $"{DateTime.Now};{ID};{_purchaseList};{totalPrice};";

        //    using (var streamWriter = new StreamWriter(@"Path/listOfReceipts.csv", true))
        //    {
        //        streamWriter.WriteLine(str);
        //    }
        //    //Console.ForegroundColor = ConsoleColor.Green;
        //    //Console.Write("SUCCESS: ");
        //    //Console.WriteLine(str);
        //    //Console.ResetColor();

        //}
        //List<Object> LoadFromDb() // move to Database class
        //{
        //    List<Object> listOfItems = new List<Object>();

        //    using (StreamReader str = new StreamReader(@"Path/listOfReceipts.csv"))
        //    {
        //        string line;
        //        while ((line = str.ReadLine()) != null)
        //        {

        //            listOfItems.Add(line);
        //        }

        //    }
        //    return listOfItems;
        //}

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

        // READ TBD!!
        public void MakePurchase(Database db, Consumer consumer)
        {
            Console.WriteLine("Choose an existing purchase list:");
            Console.WriteLine();

            db.Display(consumer.PurchaseLists, true);
            Console.Write("Enter the ID number of the purchase list: ");
            int userInput = Int32.Parse(Console.ReadLine());

            PurchaseList newPurchasePL = new PurchaseList();
            foreach (PurchaseList pl in consumer.PurchaseLists)
            {
                if (userInput == pl.Id)
                {
                    newPurchasePL = pl.CopyPurchaseList();
                    db.Display(newPurchasePL);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Do you want to make a purchase?");
            Console.WriteLine("Write [1] for YES and [2] for NO.");
            int userInput2 = Int32.Parse(Console.ReadLine());
            if (userInput2 == 1)
            {
                var newPurchase = new Purchase();
                // assign the unique ID to the receipt
                int lastExistingID = 0;
                foreach (Purchase p in db.AllPurchases)
                {
                    if (p.Id > lastExistingID) lastExistingID = p.Id;
                }
                lastExistingID += 1;
                newPurchase.SetID(lastExistingID);
                newPurchase.SetDateTime(DateTime.Now.Date);
                newPurchase.SetEmail(consumer.Email);

                // TBD! Select specific items to purchase!
                foreach (Item item in newPurchasePL.ListOfItems)
                {
                    item.SetIsBought(true);
                }
                newPurchase.SetPurchaseList(newPurchasePL);

                consumer.Purchases.Add(newPurchase);
                consumer.IdsOfPurchases.Add(newPurchase.Id);
                db.AllPurchases.Add(newPurchase);
                db.AddObjectToDatabase(newPurchase);
                db.EditObjectInDatabase(consumer);
                Console.WriteLine();
                Console.WriteLine("Purchase is done and receipt is saved!");
            }
            else if (userInput2 == 2)
            {
                Console.WriteLine();
                Console.WriteLine("Ok, you do not want to make a purchase.");
                Console.WriteLine("Please go back to the menu and choose your next action.");
            }
            Console.WriteLine();
        }
    }
}


