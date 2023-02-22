using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace Digital_shopping_list_group_5
{

    //Description
    //...
    //...
    public class Purchase : IAct
    {
        public string name { get; set; }
        //string name = "null";
        List<Object> listOfItems = new List<Object>();

        public Purchase(string name, List<object> listOfItems)
        {
            this.name = name;
            this.listOfItems = listOfItems;
        }
        public Purchase() { }



        public List<Object> GetList() => listOfItems;


        //================================================
        //recording & retrieving data
        void IAct.SaveToDb(Object obj)
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
        List<Object> IAct.LoadFromDb()
        {
            List<Object> listOfPurchases = new List<Object>();

            using (StreamReader str = new StreamReader(@"Path/listOfPurchases.csv"))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {

                    listOfPurchases.Add(line);
                }

            }
            return listOfPurchases;
        }
        //================================================







        //================================================
        //Folowwing 4 functions TBD
        public override string ToString()
        {
            string str = null;
            foreach (object item in listOfItems)
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
 

        //Adds new purschases to customers list of purchases.
        //TBD: Not finalized!
        public void NewPurchaseList()
         {
             bool quit = false;
             var newPurchaseList = new Purchase();
             var newList = new List<Object>();

         Console.WriteLine("[1] Create new purchase list.");
         Console.WriteLine("[2] Use existing list as template.");
         Console.WriteLine("[3] Quit.");
         string input = Console.ReadLine();

         switch (input)
         {
             case "1":
                 Console.WriteLine("New empty purchase list created.");
                 break;
             case "2":
                 // METHOD: View/Select purchase lists from customers lists.
                 // newList = ... // newPurchaseList = ...
                 Console.WriteLine("Purchase list created from existing list.");
                 break;
             case "3": return;
             default: Console.WriteLine($"Unknown input: {input}"); break;
         }

         Console.Write("Enter name of new purchase list: ");
         newPurchaseList.name = Console.ReadLine();

         while (quit == false)
         {
             Console.WriteLine($"[1] SaveToDb items to \"{newPurchaseList.name}\".");
             Console.WriteLine($"[2] Remove items from \"{newPurchaseList.name}\".");
             Console.WriteLine($"[3] Save list (\"{newPurchaseList.name}\") and quit.");
             Console.WriteLine($"[4] Discard list (\"{newPurchaseList.name}\") and quit.");

             switch (input)
             {
                 case "1":
                     // METHOD: SaveToDb items to list. 
                     break;
                 case "2":
                     // METHOD: Remove items from list.
                     break;
                 case "3":
                     Console.WriteLine($"New list \"{newPurchaseList.name}\" successfully created and saved.");
                     newPurchaseList.listOfItems = newList;
                     // ADD: newPurchaseList to customers.
                     // METHOD: Save list to file? 
                     break;
                 case "4":
                     Console.WriteLine($"New list \"{newPurchaseList.name}\" discarded.");
                     quit = true;
                     break;
                 default: Console.WriteLine($"Unknown input: {input}"); break;
             }
         }
     }
        //==================================================
         


    }

}

 