using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Digital_shopping_list_group_5
{
    //Disconnected class (löst kopplad klass).
    //When initiated, it takes advantage of the interface <<IAct>> through the concrete classes <Item>, <Purchase>, <Consumer> & <Receipt>
    internal class Database 
    {
        List<string> sample = new List<string>();
        List<string> itemlist = new List<string>();

        List<Consumer> listOfAccounts = new List<Consumer>();

        private readonly IAct act;

        public Database() { }
        public Database(IAct act)
        { 
            this.act = act;
        }


        public List<Consumer> GetListOfAccounts() => listOfAccounts;

        public List<Object> LoadFromDb()
        {
            //Merge the function LoadFromDb()  from the classes Item,Purchase,Account.
            //The same code in all 3 classes.

            List<Object> list = act.LoadFromDb();
            return list;
        }
        public void SaveToDb(object obj)
        {
            act.SaveToDb(obj);
        }






        public void LoadLists()
        {
            string file = "Path/listOfPurchases.csv"; 
            Console.WriteLine("Choose shopping list:");
            Console.WriteLine();
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                int b = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    char[] charsToTrim3 = { ';'};
                    string[] field = line.Split(';');
                    Console.WriteLine($"[{b++}] {field[0]}");
                    string addinlist = $"{line.Trim(charsToTrim3)}";
                    sample.Add(addinlist);
                }
                
            }
            Console.WriteLine();
            int userChoose = Int32.Parse(Console.ReadLine());
            if (userChoose == 1)
            {
                Console.WriteLine(sample[0]);
            }
            if (userChoose == 2)
            {
                Console.WriteLine(sample[1]);
            }
            if (userChoose == 3)
            {
                Console.WriteLine(sample[2]);
            }
        }



        public void LoadListsOrg()
        {
            string file = "Path/listOfPurchases.csv";
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                int b = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    char[] charsToTrim3 = { ';' };
                    string[] field = line.Split(';');
                    Console.WriteLine($"[{b++}] {field[0]}");
                    string addinlist = $"{line.Trim(charsToTrim3)}";
                    sample.Add(addinlist);
                }
            }
        }
        public void LoadListsAddinList()
        {
            string file = "Path/listOfPurchases.csv";
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                int b = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    char[] charsToTrim3 = { ';' };
                    string[] field = line.Split(';');
                    string addinlist = $"{line.Trim(charsToTrim3)}";
                    sample.Add(addinlist);
                }
            }
        }





        public void LoadItemList()
        {
            string file = "Path/items.csv";
            Console.WriteLine();
            using (StreamReader sr = new StreamReader(file))
            {
                string line1;
                int b = 1;
                while ((line1 = sr.ReadLine()) != null)
                {
                    //char[] charsToTrim3 = { ';' };
                    string[] field1 = line1.Split(';');
                    Console.WriteLine($"[{b++}] {field1[1]}");
                    string addinlist = $"{line1}";
                    itemlist.Add(addinlist);
                }
            }
        }
        public void LoadItemListAddinList()
        {
            string file = "Path/items.csv";
            Console.WriteLine();
            using (StreamReader sr = new StreamReader(file))
            {
                string line1;
                int b = 1;
                while ((line1 = sr.ReadLine()) != null)
                {
                    //char[] charsToTrim3 = { ';' };
                    string[] field1 = line1.Split(';');
                    string addinlist = $"{line1}";
                    itemlist.Add(addinlist);
                }
            }
        }




        public void EditLists(int userChoose)
        {
            Console.WriteLine("Choose list to edit:");
            Console.WriteLine();
            LoadListsOrg();
            int chooseList = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Choosen list:");
            Console.WriteLine();
            Console.WriteLine(sample[chooseList - 1]);
            Console.WriteLine("[1] Add item");
            Console.WriteLine("[2] Delete item");
            userChoose = Int32.Parse(Console.ReadLine());
            switch (userChoose)
            {
                case 1:
                    Console.WriteLine("Choose item to add:");
                    LoadItemList();
                    int itemChoose = Int32.Parse(Console.ReadLine());
                    string text = $"{sample[chooseList - 1]};{itemlist[itemChoose - 1]}";
                    string addtest = text;
                    sample.RemoveAt(chooseList - 1);
                    sample.Insert(chooseList - 1, addtest);
                    Console.WriteLine();
                    Console.WriteLine("Shopping list is updated!");
                    Console.WriteLine();
                    Console.WriteLine(sample[chooseList - 1]);
                    break;
                case 2:
                    Console.WriteLine("Choose item to delete:");
                    int cnt = 2;
                    int plustre = 3;
                    string lineList = sample[chooseList - 1];
                    string[] itemsInLine = lineList.Split(';');
                    Console.WriteLine($"[1] {itemsInLine[2]}");
                    for (int i = 1; i <= (itemsInLine.Length / 3) + 1; i++)
                    {
                        plustre++;
                        int sum = plustre + i++;
                        Console.WriteLine($"[{cnt++}] {itemsInLine[sum]}");
                    }
                    int itemDelete = Int32.Parse(Console.ReadLine());
                    if (itemDelete == 1)
                    {
                        itemsInLine = itemsInLine.Where((source, index) => index != 1).ToArray();
                        itemsInLine = itemsInLine.Where((source, index) => index != 1).ToArray();
                        itemsInLine = itemsInLine.Where((source, index) => index != 1).ToArray();
                    }
                    if (itemDelete > 1)
                    {
                        int count1 = 1;
                        for (int i = 1; i < 20; i++)
                        {
                            count1++;
                            i++;
                            if (itemDelete == count1)
                            {
                                itemsInLine = itemsInLine.Where((source, index) => index != (count1 + i)).ToArray();
                                itemsInLine = itemsInLine.Where((source, index) => index != (count1 + i)).ToArray();
                                itemsInLine = itemsInLine.Where((source, index) => index != (count1 + i)).ToArray();
                            }
                        }
                    }
                    itemsInLine = Array.ConvertAll(itemsInLine, a => a = a + ";");
                    string update = String.Concat(itemsInLine);
                    sample.RemoveAt(chooseList - 1);
                    sample.Insert(chooseList - 1, update);
                    Console.WriteLine();
                    Console.WriteLine("Item is deleted! Updated list:");
                    Console.WriteLine();
                    Console.WriteLine(sample[chooseList - 1]);
                    break;
                default:
                    Console.Write($"\nInvalid option: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{userChoose}\n");
                    Console.ResetColor();
                    break;
            } 
        }

    }
    
}

