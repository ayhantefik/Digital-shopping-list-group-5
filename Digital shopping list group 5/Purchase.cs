﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Digital_shopping_list_group_5
{
    internal partial class Program
    {

        //Description
        //...
        //...
        public class Purchase : IAct
        {
            public string name { get; set; }
            //string name = "null";
            List<Item> listOfItems = new List<Item>();

            public Purchase(string name, List<Item> listOfItems)
            {
                this.name = name;
                this.listOfItems = listOfItems;
            }


            public List<Item> GetList() => listOfItems;
            public override string ToString()
            {
                string str = null;
                foreach (Item item in listOfItems)
                { str += item.ToString(); }
                return str;
            }
            public void Display()
            {
                throw new NotImplementedException();
            }

            public void Remove()
            {
                throw new NotImplementedException();
            }

            void IAct.Add(Object obj)
            {
                string str = name + ";" + obj.ToString();
                using (var streamwriter = new StreamWriter(@"Path/listOfPurchases.csv", true))
                {
                    streamwriter.WriteLine(str);
                }
                System.IO.File.WriteAllText(@"Path/items.csv", string.Empty);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("SUCCESS: ");
                Console.WriteLine(str);

            }
        }
    }
}
    
