using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Digital_shopping_list_group_5
{
    public class Consumer : User, IAct    // CHANGED ACCESSIBILITY TO PUBLIC
    {
        int accountLvl, points;
        List<Object> purchases;        
        private List<object> listOfPurchases;
        public List<object> ListOfPurchases { get => listOfPurchases; } // ADDED GETTER!
        public Consumer(string name, string email, string password,bool loggedIn, int v1, int v2, List<object> listOfPurchases)
            :base(name,email,password,loggedIn)
        {
            accountLvl = v1;
            points = v2;
            this.listOfPurchases = listOfPurchases;
        }


        //=============================================================================================================
        //Setters,Getters NYI
        public override string ToString() => $"{Name},{Email},{Password};"; // TBD
        //=============================================================================================================





        //=============================================================================================================
        //recording & retrieving data
        void IAct.SaveToDb(object obj)
        {
            string str = obj.ToString();
            using (var streamwriter = new StreamWriter(@"Path/accounts.csv", true))
            {
                streamwriter.WriteLine(str);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("SUCCESS: ");
            Console.WriteLine(str);
        }
        List<object> IAct.LoadFromDb()
        {
            List<Object> listOfAccounts = new List<Object>();

            using (StreamReader str = new StreamReader(@"Path/accounts.csv"))
            {
                string line;
                while ((line = str.ReadLine()) != null)
                {

                    listOfPurchases.Add(line);
                }

            }
            return listOfPurchases;
        }
        //=============================================================================================================





        void IAct.Display()
        {
            throw new NotImplementedException();
        }     
        void IAct.Remove()
        {
            throw new NotImplementedException();
        }

        
    }
}
