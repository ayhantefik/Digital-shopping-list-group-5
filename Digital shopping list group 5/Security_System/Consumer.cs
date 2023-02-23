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


        public Consumer(string name = "", string email = "", string password = "",bool loggedIn = false, int v1 =-1, int v2=-1, List<object> listOfPurchases = null)
            :base(email,password,loggedIn,name)
        {
            accountLvl = v1;
            points = v2;
            this.listOfPurchases = listOfPurchases;
        }


        //=============================================================================================================
        //Setters,Getters NYI

        public int AccountLvl => accountLvl;
        public int Points => points;

        public override string ToString() => $"{Email};{Password};{Name};{LoggedIn};{AccountLvl};{Points}"; // TBD
        //=============================================================================================================





        //=============================================================================================================
        //recording & retrieving data
        void IAct.SaveToDb(object obj)
        {
            string str = obj.ToString();            
            using (var streamwriter = new StreamWriter(@"Path/accounts.csv", true))
            {
                //if an email existed, it would mean that we want to replace that row with the modified one. TBD 
                //...
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

                    listOfAccounts.Add(line);
                }

            }
            return listOfAccounts;
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


        internal void LoginAccount()
        { } // TBD

        internal void RegisterAccount()
        {
            bool success = false;
            string str = "";
            do
            {
                Console.Write("Your email: ");
                str = Console.ReadLine();
                success = CheckInput(1,str); // 1 - position in the registration proccess 
                if (success)
                {
                    SetEmail(str);

                    Console.Write("Create password: ");
                    str = Console.ReadLine();
                    success = CheckInput(2,str);

                    if (success)
                    {
                        SetPassword(str);

                        Console.Write("Your name: ");
                        str = Console.ReadLine();
                        success = CheckInput(3, str);

                        if (success)
                        {
                            SetName(str);
                            //record Consumer object in string format to accounts.csv
                            //...
                            var consumer = new Consumer(this.Name, this.Email, this.Password);
                            Database db = new Database(consumer);
                            db.SaveToDb(consumer);
                            
                            Console.WriteLine("Success!" + this.ToString());
                        }
                    }

                }
                
                

            }
            while (!success);
        }


        //so far only email
        static bool CheckInput(int positionInTheProcess, string input)
        {
            bool success = false;

            if (input.Trim().Length == 0) return false;


            if (positionInTheProcess == 1)
            {
                // check if no email address already is registered!
                Database db = new Database(new Consumer());
                List<Object> listOfAccounts = db.LoadFromDb();

                bool alreadyExisted = false;
                foreach (Object account in listOfAccounts)
                {
                    string str = account.ToString();
                    string[] arr = str.Split(';');

                    if (arr[0] == input.Trim()) alreadyExisted = true;
                }

                if (alreadyExisted) Console.WriteLine($"Account {input} already exists in our system");
                else success = true;
            }
            else if (positionInTheProcess == 2 || positionInTheProcess == 3) return true;

            return success;
        }


        
    }
}
