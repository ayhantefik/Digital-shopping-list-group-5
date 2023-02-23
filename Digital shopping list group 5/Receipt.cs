using System;
using System.IO;

namespace Digital_shopping_list_group_5
{
   internal class Receipt
    {
        public Receipt()
        {
            int iDPurchase;
            int iDCustomer;
            int quantity;
            string name;
            bool isBought;
            public Guid Id { get; set; }

            public Receipt() { }

            protected Receipt(int ID, int quantitiy, string name, bool isBought)
            {
                this.iDPurchase = iDPurchase;
                this.iDCustomer = iDCustomer;
                this.name = name;
                this.isBought = isBought;
            }
            public int ID { get; set; }
            public int quantity { get; set; }
            public string name { get; set; }
            public bool isBought { get; set; }
        }
    }
}