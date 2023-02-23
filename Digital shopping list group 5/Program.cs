using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
//using static Digital_shopping_list_group_5.Program;

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

            var person1 = new Consumer("Stanislav", "test@mail.ua","password", true, 3, 666, listOfPurchases);
            make = new Do(person1);
            make.SaveToDb(person1);

            List<Object> listOfAccounts = make.LoadFromDb(); //retrieving accounts */

            //=============================================================================================================


            RunMenu();           
        }
        public static void RunMenu()
        {
            Database data1 = new Database();
            Console.WriteLine("[1] Shopping lists");
            Console.WriteLine("[2] Receipts");
            Console.WriteLine("[3] Shopping");
            
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
                            case 1:
                                Console.WriteLine("Code missing..");
                                RunMenu();
                                break;
                            case 2:
                                data1.EditLists(userInput);
                                break;
                            case 3:
                                Console.WriteLine("Code missing..");
                                RunMenu();
                                break;
                            case 4:
                                data1.LoadLists(userInput);
                                userInput = Int32.Parse(Console.ReadLine());
                                break;
                            case 5:
                                Console.WriteLine("Code missing..");
                                RunMenu();
                                break;
                            case 6:
                                Console.WriteLine("Code missing..");
                                RunMenu();
                                break;
                            case 7:
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
                    case 2:
                        Console.WriteLine("Code missing..");
                        RunMenu();
                        break;
                    case 3:
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


        //The function NewPurchaseList() is moved to the class <Purchase>
    }
}
