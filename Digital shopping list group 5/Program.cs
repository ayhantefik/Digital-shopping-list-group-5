using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Digital_shopping_list_group_5.Program;

namespace Digital_shopping_list_group_5
{
    class Program
    {

        //Description TBD

        static void Main(string[] args)
        {
            Database db = new Database();
            db.LoadAllFromDatabase();

            Consumer consumer = new Consumer();
            do
            {
                consumer = consumer.RunSecuritySystem(db); // returns Consumer that either successfully loggedIn or registered
                if (db.GetCurrentConsumer != null) RunMenu(db, consumer);

            } while (db.GetCurrentConsumer == null);

        }
        static void RunMenu(Database db, Consumer consumer)
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[1] Purchase lists");  // it works
            Console.WriteLine("[2] Receipts"); // TBD
            Console.WriteLine("[3] Make a purchase"); // TBD, pay for the existing purchase list
            Console.WriteLine("[0] Log out"); // it works
            while (true)
            {
                try
                {
                    int userInput = Int32.Parse(Console.ReadLine());
                    switch (userInput)
                    {
                        case 0:
                            //db.SetCurrentConsumer(null); 
                            //Console.Clear();
                            db.SetCurrentConsumer(null); consumer.RunSecuritySystem(db); RunMenu(db, db.GetCurrentConsumer);  break;
                        case 1:

                            // displaying the Consumer´s existing purchase lists.
                            // <true> shows the purchase list´s IDs and the names,NO items. <false> includes the items for every purchase list
                            Console.Clear();
                            db.Display(db.GetCurrentConsumer.ListOfPurchases, true);

                            Console.WriteLine();
                            Console.WriteLine("[1] Create a new purchase list"); // it works
                            Console.WriteLine("[2] Edit an existing purchase list"); //TBD, changes the list´s name, the items´s names, their quantity & price
                            Console.WriteLine("[3] Delete a purchase list"); // it works

                            Console.WriteLine("[4] Merge lists"); // it works
                            Console.WriteLine("[5] Share list"); // it works                     
                            Console.WriteLine();
                            Console.WriteLine("[0] Back");
                            userInput = Int32.Parse(Console.ReadLine());
                            PurchaseList pl = new PurchaseList();
                            switch (userInput)
                            {
                                case 0:
                                    RunMenu(db, consumer);
                                    break;
                                case 1:
                                    Console.Clear();
                                    db = pl.NewPurchaseList(db, consumer); //return db with an update Consumer in it 
                                    RunMenu(db, db.GetCurrentConsumer);
                                    break;
                                case 2: // Edit list option

                                    //db.EditLists();
                                    //db.ChangePurchaseListName();
                                    break;

                                case 3:
                                    db = pl.RemovePurchaseList(db, consumer);
                                    RunMenu(db, db.GetCurrentConsumer);
                                    break;

                                case 4:
                                    db = pl.MergeLists(db, consumer);
                                    RunMenu(db, db.GetCurrentConsumer);
                                    break;
                                case 5:

                                    db = pl.ShareList(db, consumer);
                                    RunMenu(db, db.GetCurrentConsumer);
                                    break;
                                default:
                                    Console.Write($"\nInvalid option: ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"{userInput}\n");
                                    Console.ResetColor();
                                    RunMenu(db, consumer);
                                    break;
                            }
                            break;
                        case 2: // Receipts menu option, TBD
                                //db.Display(db.ListOfReceipts, true);
                            db.ShowReceipts(consumer);
                            break;
                        case 3:
                            Purchase newpurchase = new Purchase();
                            newpurchase.MakePurchase(db, consumer);
                            RunMenu(db, consumer);
                            break;
                        default:
                            Console.Write($"\nInvalid option: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{userInput}\n");
                            Console.ResetColor();
                            RunMenu(db, consumer);
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("\nInvalid option.\n");
                    RunMenu(db, consumer);
                }
            }

        }
    }
}
