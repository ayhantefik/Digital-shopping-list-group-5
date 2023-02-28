using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
            //The security system was embedded to RunMenu().

            Database db = new Database();
            db.LoadAllFromDatabase();

            Consumer consumer = new Consumer();
            do
            {
                consumer = consumer.RunSecuritySystem(db); // returns Consumer that either successfully loggedIn or registered
                if (db.GetConsumer != null) RunMenu(db, consumer);

            } while (db.GetConsumer == null);

        }
        static void RunMenu(Database db, Consumer consumer)
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[1] Purchase lists");  // it works
            Console.WriteLine("[2] Receipts"); // TBD
            Console.WriteLine("[3] Make a purchase"); // TBD, pay for the existing purchase list
            Console.WriteLine("[0] Log out"); // it works
            try
            {
                int userInput = Int32.Parse(Console.ReadLine());
                switch (userInput)
                {
                    case 0:
                        db.SetConsumer(null); break;
                    case 1:

                        // displaying the Consumer´s existing purchase lists.
                        // <true> shows the purchase list´s IDs and the names,NO items. <false> includes the items for every purchase list
                        db.Display(db.GetConsumer.ListOfPurchases, true);

                        Console.WriteLine();
                        Console.WriteLine("[1] Create a new purchase list"); // it works
                        Console.WriteLine("[2] Edit an existing purchase list"); //TBD, be able to change the list´s name, the items´s names, their quantity & price
                        Console.WriteLine("[3] Delete a purchase list"); // it works

                        Console.WriteLine("[4] Merge lists"); //TBD
                        Console.WriteLine("[5] Share list"); // TBD                       
                        Console.WriteLine();
                        Console.WriteLine("[0] Back");
                        userInput = Int32.Parse(Console.ReadLine());
                        switch (userInput)
                        {
                            case 0: RunMenu(db, consumer);
                                break;
                            case 1:
                                PurchaseList pl = new PurchaseList();
                                pl.NewPurchaseList(db, consumer);
                                break;
                            case 2: // Edit list option

                                //db.EditLists();
                                //db.ChangePurchaseListName();
                                break;
                            case 3:
                                PurchaseList list = new PurchaseList();
                                list.RemovePurchaseList(db, consumer);
                                break;



                            case 4: // Merge list option
                                Console.WriteLine("Code missing..");
                                RunMenu(db, consumer);
                                break;
                            case 5: // Share list option
                                PurchaseList list1 = new PurchaseList();
                                list1.ShareList(db, consumer);
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
                        Console.WriteLine("Here is a list of all receipts: ");
                        Console.WriteLine("Code missing..");
                        RunMenu(db, consumer);
                        break;
                    case 3: // Make a purchase menu option, TBD
                        Console.WriteLine("Choose an existing purschase list.");
                        db.Display(db, db.GetConsumer.ListOfPurchases, true);
                        userInput = Int32.Parse(Console.ReadLine());
                        
                        Console.WriteLine("Code missing..");
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
            }catch
            {
                Console.WriteLine("\nInvalid option\n");
                RunMenu(db, consumer);
            }
            
        }
    }
}
