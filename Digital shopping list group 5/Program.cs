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
            //=============================================================================================================
            //The proccess of recording & retrieving data

            /*Item item1 = new Item("milk", 1, false);
            Do make = new Do(item1);
            make.SaveToDb(item1);

            Item item2 = new Item("sugar", 2, false);
            make = new Do(item2);
            make.SaveToDb(item2);

            List<Object> listOfItems = make.LoadFromDb(); //retrieving Items for purchase1
            Purchase purchaseList = new Purchase("Biltema", listOfItems);

            make = new Do(purchaseList);
            make.SaveToDb(purchaseList);


            Item item3 = new Item("coffe", 1, false);
            make = new Do(item3);
            make.SaveToDb(item3);

            Item item4 = new Item("juice", 2, false);
            make = new Do(item4);
            make.SaveToDb(item4);


            List<Object> listOfItems2 = make.LoadFromDb(); //retrieving Items for purchase2
            Purchase purchaseList2 = new Purchase("Willys", listOfItems2);
            make = new Do(purchaseList2);
            make.SaveToDb(purchaseList2);

            
            List<Object> listOfPurchases = make.LoadFromDb(); //retrieving purchases and add them to the account1

            //The proccess of writing down the purchase lists into the file. 
            // TO BE EXTENDED 

            var person1 = new Consumer("Stanislav", "test@mail.ua","password", true, 3, 666, listOfPurchases);
            make = new Do(person1);
            make.SaveToDb(person1);

            List<Object> listOfAccounts = make.LoadFromDb(); //retrieving accounts */

            //=============================================================================================================

            Receipt test = new Receipt(455, 6, "äpplen", true, DateTime.Now);
            Console.WriteLine(test.ToString());



            //RunSecuritySystem(); // TBD
            RunMenu();           
        }
        public static void RunMenu()
        {
            
            Database data1 = new Database();
            data1.LoadLists();
            data1.LoadItems();
            Console.WriteLine("[1] Purchase lists");
            Console.WriteLine("[2] Receipts");
            Console.WriteLine("[3] Make a purchase");
            try
            {
                int userInput = Int32.Parse(Console.ReadLine());
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine("[1] Create list");
                        Console.WriteLine("[2] Edit list");
                        Console.WriteLine("[3] Delete list");
                        Console.WriteLine("[4] Show lists ");
                        Console.WriteLine("[5] Merge lists");
                        Console.WriteLine("[6] Share list");
                        Console.WriteLine("[7] Change list name"); //Det kanske ska ligga i create och edit
                        Console.WriteLine();
                        Console.WriteLine("[0] Back");
                        userInput = Int32.Parse(Console.ReadLine());
                        switch (userInput)
                        {
                            case 1: // Create list option
                                Console.WriteLine("Code missing..");
                                RunMenu();
                                break;
                            case 2: // Edit list option
                                data1.EditLists(userInput);
                                break;
                            case 3: // Delete list option
                                Console.WriteLine("Code missing..");
                                RunMenu();
                                break;
                            case 4: // Show list option
                                data1.ShowLists();
                                break;
                            case 5: // Merge list option
                                Console.WriteLine("Code missing..");
                                RunMenu();
                                break;
                            case 6: // Share list option
                                Console.WriteLine("Code missing..");
                                RunMenu();
                                break;
                            case 7: // Change list name option
                                Console.WriteLine("Code missing..");
                                RunMenu();
                                break;
                            case 0:
                                RunMenu();
                                break;
                            default:
                                Console.Write($"\nInvalid option: ");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{userInput}\n");
                                Console.ResetColor();
                                RunMenu();
                                break;
                        }
                        break;
                    case 2: // Receipts menu option
                        Console.WriteLine("Here is a a list of all receipts: ");
                        Console.WriteLine("Code missing..");
                        RunMenu();
                        break;
                    case 3: // Purchase menu option
                        Console.WriteLine("Code missing..");
                        RunMenu();
                        break;
                    default:
                        Console.Write($"\nInvalid option: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{userInput}\n");
                        Console.ResetColor();
                        RunMenu();
                        break;
                }
            }
            catch
            {
                Console.WriteLine("\nInvalid option\n");
                RunMenu();
            }


        }

        static void RunSecuritySystem()
        {
            Console.WriteLine("[1]Login");
            Console.WriteLine("[2] Registration");
            try
            {
                int userInput = Int32.Parse(Console.ReadLine());

                //only consumers can log in so far. Admins TBD 
                var person = new Consumer(); 
                switch (userInput)
                {                    
                    case 1:
                        person.LoginAccount();
                        break;
                    case 2:                        
                        person.RegisterAccount();
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid option");
                RunSecuritySystem();
            }
        } // TBD

     }
}
