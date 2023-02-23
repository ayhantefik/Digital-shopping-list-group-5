using System;
using System.Collections.Generic;
using System.IO;

namespace Digital_shopping_list_group_5
{

    //Description 
    //...
    //...
    public class Item : IAct
    {
        int quantity;
        string name = "null";
        int ID = -1;
        bool isBought = false;
        

        
        public Item()
        { }
        public Item(int quantity,string name, int iD, bool isBought)
        {
            this.quantity = quantity;
            this.name = name;
            ID = iD;
            this.isBought = isBought;
        }



        //=====================================================
        //Setters (Getters TBD)
        public int SetQuantity(int value) => quantity = value;
        public int SetID(int value) => ID = value;
        public string SetName(string value) => name = value;
        public bool SetIsBought(bool value) => isBought = value;

        //=====================================================








        //=====================================================
        //recording & retrieving data
        void IAct.SaveToDb(Object obj)
        {
            string str = $"{ID};{quantity};{name};{isBought};";

            using (var streamWriter = new StreamWriter(@"Path/items.csv", true))
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

            using (StreamReader str = new StreamReader(@"Path/items.csv"))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {

                    listOfItems.Add(line);
                }

            }
            return listOfItems;
        }
        //=====================================================







        //======================================================
        //Following 3 functions TBD
        public override string ToString()
        {
            return $"lol {ID};{quantity};{name};{isBought};";
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
        //======================================================




        
    }        

}
