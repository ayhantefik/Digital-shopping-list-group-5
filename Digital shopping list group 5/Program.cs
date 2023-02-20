using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Digital_shopping_list_group_5.Program;

namespace Digital_shopping_list_group_5
{
    internal class Program
    {
        enum CustomerLevel
        { Beginner,Bronze,Silver,Guld}; 


        // All of the 3 actions are common for both an item (en vara) and for a purchase (inköpslistan).
        // Therefore, we do an interface for those concrete classes Item and Purchase
        public interface IAct
        { 
            void Display();
            void Add(Object item);
            void Remove();
        }


        //Disconnected class (löst kopplad klass).
        //When initiated, it takes advantage of the interface <<IAct>> through the concrete classes <Item> and <Purchase>
        public class Do
        {
            private readonly IAct act;
            public Do(IAct act)
            {
                this.act = act;
            }
            public void Add(Object obj)
            {
                act.Add(obj);
            }

            //TO BE IMPLEMENTED...
            //...
            //...
        }




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

                using (var streamWriter = new StreamWriter(@"Path/items.txt", true))
                {
                    streamWriter.WriteLine(str);                    
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("SUCCESS: ");
                Console.WriteLine(str);
                

            }
        }        
        public class Purchase : IAct
        {
            string name = "null";
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
                using (var streamwriter = new StreamWriter(@"Path/listOfPurchases.txt", false))
                {
                    streamwriter.WriteLine(str);
                }
                System.IO.File.WriteAllText(@"Path/items.txt", string.Empty);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("SUCCESS: ");
                Console.WriteLine(str);

            }

        }


        static void Main(string[] args)
        {
            //The proccess of writing down the purchase lists into the file. 
            // TO BE EXTENDED 

            Item item = new Item("milk",1, false);
            var V1 = new Do(item);
            V1.Add(item);

            item = new Item("bread",2,false);
            V1.Add(item);

            List<Item> list = LoadItemsFromFile();
            Purchase purchaseList = new Purchase("Willys", list);         

            V1 = new Do(purchaseList);           
            V1.Add(purchaseList); 
        }
        static List<Item> LoadItemsFromFile()
        {
            List<Item> items = new List<Item>();
            using (var streamreader = new StreamReader(@"Path/items.txt"))
            {
                string str;

                while((str = streamreader.ReadLine()) != null)
                {
                    string[] arr = str.Split(';');
                    Item item = new Item();
                    item.SetID(Int32.Parse(arr[0]));
                    item.SetName(arr[1]);
                    item.SetIsBought(bool.Parse(arr[2]));
                    items.Add(item);
                }                
            }
            return items;
        }
    }
}
