using System;
using System.IO;

namespace Digital_shopping_list_group_5
{
    internal partial class Program
    {
        //Description 
        //...
        //...
        public class Item : IAct
        {
            string name = "null";
            int ID = -1;
            bool isBought = false;
            
            public Item()
            { }
            public Item(string name, int iD, bool isBought)
            {
                this.name = name;
                ID = iD;
                this.isBought = isBought;
            }

            public int SetID(int value) => ID = value;
            public string SetName(string value) => name = value;
            public bool SetIsBought(bool value) => isBought = value;

            public override string ToString()
            {
                return $"{ID};{name};{isBought};";
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

            void IAct.Add(Object obj)
            {
                string str = obj.ToString();

                using (var streamWriter = new StreamWriter(@"Path/items.csv", true))
                {
                    streamWriter.WriteLine(str);                    
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("SUCCESS: ");
                Console.WriteLine(str);
                

            }
        }        
    }
}
