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

        private int _accountLvl, _points;
        private bool _loggedIn = false;

        private List<PurchaseList> _purchaseLists;
        private List<Purchase> _purchases;

        private List<int> _idsOfPurchaseLists = new List<int>();
        private List<int> _idsOfPurchases = new List<int>();

        // Properties (Setters & Getters)
        public int AccountLvl => _accountLvl;
        public void SetAccountLvl(int value) => _accountLvl = value;
        public int Points => _points;
        public void SetPoints(int value) => _points = value;
        public bool LoggedIn => _loggedIn;
        public List<PurchaseList> PurchaseLists { get => _purchaseLists; set => _purchaseLists = value; }
        public List<Purchase> Purchases { get => _purchases; set => _purchases = value; }
        public List<int> IdsOfPurchaseLists => _idsOfPurchaseLists;
        public void SetIdsOfPurchaseLists(List<int> value) => _idsOfPurchaseLists = value;
        public void InitiateIdsOfPurchaseLists() => _idsOfPurchaseLists = new List<int>();
        public List<int> IdsOfPurchases => _idsOfPurchases;
        public void SetIdsOfPurchases(List<int> value) => _idsOfPurchases = value;

        // Constructors
        public Consumer(string email = "", string password = "", string name = "", int accountLvl = -1, int points = -1)
            : base(email, password, name)
        {
            _accountLvl = accountLvl;
            _points = points;
            _idsOfPurchaseLists = new List<int>();
            _idsOfPurchases = new List<int>();
            _purchaseLists = new List<PurchaseList>();
            _purchases = new List<Purchase>();
        }
        // TEMP!
        public Consumer(string email = "", string password = "", string name = "", int accountLvl = -1, int points = -1, List<int> idsOfPurchaseLists = null)
            : base(email, password, name)
        {
            _accountLvl = accountLvl;
            _points = points;
            _idsOfPurchaseLists = idsOfPurchaseLists;
            _idsOfPurchases = new List<int>();
            _purchaseLists = new List<PurchaseList>();
            _purchases = new List<Purchase>();
        }
        public Consumer() { }

        // Methods
        public override string ToString()
        {
            string retString = $"{Email};{Password};{Name};{AccountLvl};{Points}";

            if (_idsOfPurchaseLists.Count > 0)
            {
                retString += ";" + StringOfPurchaseListIds();
            }
            if (_idsOfPurchases.Count > 0)
            {
                retString += ";" + StringOfPurchaseIds();
            }
            return retString;  
        }
        private string StringOfPurchaseListIds()
        {
            string str = "";
            foreach (int i in _idsOfPurchaseLists)
            {
                str += i.ToString() + ",";
            }
            return str;
        }
        private string StringOfPurchaseIds()
        {
            string str = "";
            foreach (int i in _idsOfPurchases)
            {
                str += i.ToString() + ",";
            }
            return str;
        }

        // Login- and Registration System
        public Consumer RunSecuritySystem(Database db) // TBD! Admin login!
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
                    str += ";" + Console.ReadLine();
                    success = CheckInput(db, 5, str);
                    if (success)
                    {
                        if (db.GetCurrentConsumer != null)
                        {
                            return db.GetCurrentConsumer;
                        }

                    }
                    else Console.WriteLine("Password and email don´t match");
                }
                else Console.WriteLine($"{str} not found in the database");
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
                success = CheckInput(db, 1, str); // 1 - position in the registration proccess 
                if (success)
                {
                    consumer.SetEmail(str);

                    Console.Write("Create password: ");
                    str = Console.ReadLine();
                    success = CheckInput(db, 2, str);

                    if (success)
                    {
                        consumer.SetPassword(str);

                        Console.Write("Your name: ");
                        str = Console.ReadLine();
                        success = CheckInput(db, 3, str);

                        if (success)
                        {
                            consumer.SetName(str);
                            consumer.SetAccountLvl(0);
                            consumer.SetPoints(0);
                            consumer.InitiateIdsOfPurchaseLists();

                            Console.Clear();
                            Console.WriteLine($"{consumer.Email} was successfully registered in our app. Please login");


                            db.AllConsumers.Add(consumer);
                            db.AddObjectToDatabase(consumer);
                            return consumer;
                        }
                        else Console.WriteLine("Name can not be empty");
                    }
                    else Console.WriteLine("Password can not be empty");
                }
                //else Console.WriteLine("Email can not be empty");                
            } while (!success);

            return consumer;
        }

        static bool CheckInput(Database db, int positionInTheProcess, string input)
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
            else if (positionInTheProcess == 2 || positionInTheProcess == 3) return true;

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
                    if ((str[0] == c.Email) && (str[1] == c.Password))
                    {
                        var purchaseLists = new List<PurchaseList>();
                        var purchases = new List<Purchase>();

                        foreach (int id in c._idsOfPurchaseLists)
                        {
                            foreach (PurchaseList pl in db.AllPurchaseLists)
                            {
                                if (id == pl.Id) purchaseLists.Add(pl);
                            }
                        }
                        foreach (int id in c.IdsOfPurchases)
                        {
                            foreach (Purchase p in db.AllPurchases)
                            {
                                if (id == p.Id) purchases.Add(p);
                            }
                        }

                        c.PurchaseLists = purchaseLists;
                        c.Purchases = purchases;
                        db.SetCurrentConsumer(c);

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Welcome {db.GetCurrentConsumer.Name}");
                        return true;
                    }
                }
            }
            return success;
        }
        public void ShowPurchases(Database db)
        {
            foreach (Purchase p in _purchases)
            {
                Console.WriteLine($"[{p.Id}] {p.PurchaseList.Name} {p.DateCheck.ToString().Remove(10)}");
            }

            Console.WriteLine();
            Console.Write("Enter purchase(receipt) ID to view: ");
            int input = Int32.Parse(Console.ReadLine());
            Console.WriteLine();

            double sum = 0;
            foreach (Purchase p in _purchases)
            {
                if (p.Id == input)
                {
                    foreach (Item item in p.PurchaseList.ListOfItems)
                    {
                        if (item.IsBought == true)
                        {
                            Console.WriteLine($"{item.Name,-20}{item.Quantity}*{item.Price} = {item.Quantity * item.Price}");
                            sum += item.Quantity * item.Price;
                        }
                    }
                    Console.WriteLine();
                    string totalt = $"Total:";
                    Console.WriteLine($"{totalt,+30} {sum:C00}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }
    }
}
