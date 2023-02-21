using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Digital_shopping_list_group_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunMenu();
        }
        private static void RunMenu()
        {
            Console.WriteLine("[1] Shopping lists");
            Console.WriteLine("[2] Receipts");
            Console.WriteLine("[3] Shopping");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    Console.WriteLine("[1] Create list");
                    Console.WriteLine("[2] Edit list");
                    Console.WriteLine("[3] Delete list");
                    Console.WriteLine("[4] Show lists ");
                    Console.WriteLine("[5] Merge lists");
                    Console.WriteLine("[6] Share list");
                    Console.WriteLine("[7] Change list name"); //Det kanske ska ligga i create och edit
                    Console.WriteLine();
                    Console.WriteLine("[b] Back");
                    userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "2":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "3":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "4":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "5":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "6":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "7":
                            Console.WriteLine("Code missing..");
                            RunMenu();
                            break;
                        case "b":
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
                case "2":
                    Console.WriteLine("Code missing..");
                    RunMenu();
                    break;
                case "3":
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
    }
}
