using System;
using System.Collections.Generic;
using System.IO;

namespace Digital_shopping_list_group_5
{

    //Description 
    //...
    //...
    public class Item
    {
        int quantity, ID;
        double price;
        string name = "null";
        bool isBought = false;
        

        
        public Item()
        { }
        public Item(int ID, int quantity,int price,string name, bool isBought)
        {
            this.ID = ID;
            this.quantity = quantity;
            this.name = name;
            this.isBought = isBought;
            this.price = price; 
            
        }



        //=====================================================
        //Setters (Getters TBD)
        public int SetID(int value) => ID = value;
        public int SetQuantity(int value) => quantity = value;
        public double SetPrice(double value) => price = value;
        public string SetName(string value) => name = value;
        public bool SetIsBought(bool value) => isBought = value;
        public int Id => ID;
        public int Quantity => quantity;
        public double Price => price;
        public string Name => name;
        public bool IsBought => isBought;

        //=====================================================


        public override string ToString()
        {             
            return $"{ID};{quantity};{price};{name};{isBought}";
        }        
    }        

}
