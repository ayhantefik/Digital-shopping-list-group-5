using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital_shopping_list_group_5
{
    internal class Receipt:IAct
    {
        int IDPurchase = -1;
        int quantity;
        string name = "null";
        bool isBought = true;

        private readonly object email;
        private DateTime stamp;

        //=======================================================================================
        public DateTime Stamp { get; }
        public int SetIDPurchase(int value) => IDPurchase = value;
        public int SetQuantity(int value) => quantity = value;
        public string SetName(string value) => name = value;
        public bool SetIsBought(bool value) => isBought = value;
        //=======================================================================================

        public Receipt() { }

        public Receipt(int iDPurchase, int quantity, string name, bool isBought, DateTime Stamp)
        {
            this.IDPurchase = iDPurchase;
            this.quantity = quantity;
            this.name = name;
            this.isBought = isBought;
            this.Stamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $" {IDPurchase};{quantity};{name};{isBought};{DateTime.Now}";
        }

        void IAct.SaveToDb(Object obj)
        {
            string str = $"{IDPurchase};{quantity};{name};{isBought};{DateTime.Now}";

            using (var streamWriter = new StreamWriter(@"Path/listOfReceipts.csv", true))
            {
                streamWriter.WriteLine(str);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("SUCCESS: ");
            Console.WriteLine(str);
            Console.ResetColor();

        }
        List<Object> IAct.LoadFromDb()
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

        public void Display()
        {
            //NYI
            ToString();
        }
        public void Remove()
        {
            throw new NotImplementedException();
        }
    }

}
