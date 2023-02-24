using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital_shopping_list_group_5
{
    internal class Purchase: IAct
    {
        DateTime dateCheck;
        int ID = -1;
        PurchaseList purchase;
        double totalPrice;


        //=======================================================================================
        public DateTime DateCheck{ get; }
        public int SetID(int value) => ID = value;
        public double SetTotalPrice(double value) => totalPrice = value;
        //=======================================================================================

        public Purchase() { }

        public Purchase(DateTime dateCheck, int iD, PurchaseList purchase, double totalPrice)
        {
            this.dateCheck = DateTime.Now;
            this.ID = iD;
            this.purchase = purchase;
            this.totalPrice = totalPrice;
        }

        public override string ToString()
        {
            return $" {DateTime.Now};{ID};{purchase};{totalPrice};";
        }

        void IAct.SaveToDb(Object obj)
        {
            string str = $"{DateTime.Now};{ID};{purchase};{totalPrice};";

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

        public void Display() // Will this be the same as PrintReceipt?
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
