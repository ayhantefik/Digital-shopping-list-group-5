using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Digital_shopping_list_group_5
{
    public class Consumer : User 
    {
        //The receipt system NYI
        //The security system was embedded to RunSecuritySystem(),LoginAccount(),RegisterAccount(),CheckInput()

        int accountLvl, points;
        bool loggedIn = false;

        List<PurchaseList> listOfPurchases;
        List<PurchaseList> listOfPurchase;
        List<PurchaseList> listOfReceipts;

        List<int> idsOfPurchaseLists = new List<int>();
        List<int> idsOfReceipts = new List<int>(); //TBD

        public Consumer(string email = "", string password = "", string name = "", int accountLvl = -1, int points = -1, List<int> idsOfPurchaseLists = null, List<int>idsOfReceipts = null)
            : base(email, password, name)
        {
            this.accountLvl = accountLvl;
            this.points = points;
            //this.listOfPurchases = listOfPurchases;
            this.idsOfPurchaseLists = idsOfPurchaseLists;
            this.idsOfReceipts = idsOfReceipts;
        }



        //=============================================================================================================
        //Setters & Getters
        public void SetAccountLvl(int value) => accountLvl = value;
        public void SetPoints(int value) => points = value;
        public List<PurchaseList> ListOfPurchases { get => listOfPurchases; set => listOfPurchases = value; }
        public List<PurchaseList> ListOfPurchase { get => listOfPurchase; set => listOfPurchase = value; }
        public List<PurchaseList> ListOfReceipts { get => listOfReceipts; set => listOfReceipts = value; }

        public List<int> IdsOfPurchaseLists => idsOfPurchaseLists; public void InitiateIdsOfPurchaseLists() => idsOfPurchaseLists = new List<int>();
        public bool LoggedIn => loggedIn;
        public int AccountLvl => accountLvl;
        public int Points => points;
        //=============================================================================================================




        //ToString() only for recording to DB. Not for displaying in the Console. It doesn´t work.
        public override string ToString() => $"{Email};{Password};{Name};{AccountLvl};{Points};" + StringOfIdsOfPurchaseLists(); 
        private string StringOfIdsOfPurchaseLists()
        {
            string str = "";
            foreach (int i in idsOfPurchaseLists)
            {
                str += i.ToString() + ";";
            }
            return str;
        }




        //==============================================================================================================
        // Login- and Registration System
        public Consumer RunSecuritySystem(Database db) //only consumers can log in or register so far. (TBD with admins)
        {
            Consumer consumer = new Consumer();
            Console.WriteLine("[1] Login");
            Console.WriteLine("[2] Registration");
            try
            {
                int userInput = Int32.Parse(Console.ReadLine());
                switch (userInput)
                {
                    case 1:
                        return LoginAccount(db);                        
                    case 2:
                        consumer = RegisterAccount(db);
                        Console.WriteLine(consumer.Points);
                        return consumer;
                    default: 
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid option");
             RunSecuritySystem(db);
            }
            return consumer;
        }
        static Consumer LoginAccount(Database db)
        {
            bool quit = false;
            do
            {                
                Console.Write("Your email: ");
                string str = Console.ReadLine();
                bool success = CheckInput(db, 4, str);
                if (success)
                {
                    Console.Write($"Password to {str}: ");
                    str += ";"+ Console.ReadLine();
                    success = CheckInput(db, 5, str);
                    if (success)
                    {
                        if (db.GetCurrentConsumer != null)
                        {                            
                            return db.GetCurrentConsumer;
                        } 

                    }else Console.WriteLine("Password and email don´t match");
                }else Console.WriteLine($"{str} not found in the database");
            } while (!quit);
            return null;

            
        }
        static Consumer RegisterAccount(Database db)
        {
            Consumer consumer = new Consumer();
            bool success;
            string str;
            do
            {
                Console.Write("Your email: ");
                str = Console.ReadLine();
                success = CheckInput(db,1,str); // 1 - position in the registration proccess 
                if (success)
                {
                    consumer.SetEmail(str);

                    Console.Write("Create password: ");
                    str = Console.ReadLine();
                    success = CheckInput(db,2, str);

                    if (success)
                    {
                        consumer.SetPassword(str);

                        Console.Write("Your name: ");
                        str = Console.ReadLine();
                        success = CheckInput(db,3, str);

                        if (success)
                        {
                            consumer.SetName(str);
                            consumer.SetAccountLvl(0);
                            consumer.SetPoints(0);                            
                            consumer.InitiateIdsOfPurchaseLists();


                            db.AllConsumers.Add(consumer);
                            db.AddObjectToDatabase(consumer);
                            return consumer;
                        }
                        else Console.WriteLine("Name can not be empty");
                    }
                    else Console.WriteLine("Password can not be empty");
                }
                //else Console.WriteLine("Email can not be empty");                
            }while (!success);

           return consumer;
        }        
        static bool CheckInput(Database db,int positionInTheProcess, string input)
        {
            bool success = false;

            if (input.Trim().Length == 0) return false;

            //positions 1-3 are for registration
            if (positionInTheProcess == 1)
            {
                // check if no email address already is registered!
                //Database db = new Database(new Consumer());
                //List<Object> listOfAccounts = db.LoadFromDb();

                bool alreadyExisted = false;
                foreach (Consumer account in db.AllConsumers)
                {
                    if (account.Email == input.Trim())
                    {
                        alreadyExisted = true;                        
                    } 
                }
                if (alreadyExisted) Console.WriteLine($"Account {input} already registered in our system");
                else success = true;
            }
            else if(positionInTheProcess == 2 || positionInTheProcess == 3) return true; 

            //positions 4-5 are for login
            else if (positionInTheProcess == 4)
            {
                foreach (Consumer c in db.AllConsumers)
                {
                    if (input == c.Email)
                    {                        
                        return true;
                    }
                }
            }
            else if (positionInTheProcess == 5)
            {
                string[] str = input.Split(';'); //input contains both email and password
                foreach (Consumer c in db.AllConsumers)
                {
                    if ((str[0] == c.Email) && (str[1]== c.Password))
                    {
                        Consumer cons = new Consumer(c.Email, c.Password, c.Name, c.AccountLvl, c.Points, c.IdsOfPurchaseLists);
                        List<PurchaseList> plList = new List<PurchaseList>();

                        foreach (int i in cons.idsOfPurchaseLists) 
                        {
                            foreach (PurchaseList pl in db.AllPurchaseLists)
                            {
                                if (i == pl.Id) plList.Add(pl);
                            }
                        }
                        cons.ListOfPurchases = plList;
                        db.SetCurrentConsumer(cons);
                       
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Welcome {db.GetCurrentConsumer.Name}");
                        return true;
                    }
                }
            }
            return success;
        }
        //==============================================================================================================
    }
}
