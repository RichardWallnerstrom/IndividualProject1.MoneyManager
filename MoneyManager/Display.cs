using MoneyManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using CC = System.ConsoleColor;

namespace MoneyManager
{
    public static class Display  // I will need many different Option classes derived from this class
    {
        private static string GetKey()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); //Hide the key from the console
            char userInput = char.ToLower(keyInfo.KeyChar);
            return (userInput.ToString() != null) ? userInput.ToString() : string.Empty;
        }
        public static string GetLine()
        {
            string userInput = Console.ReadLine();
            return (userInput.ToString() != null) ? userInput.ToString() : string.Empty;
        }
        public static string MainMenu()
        {
            Program.Print($"\n      Main Menu".PadLeft(10), CC.DarkYellow);
            Program.Print("\n----------------------------------------------------------------------------\n", CC.DarkBlue);
            Program.Print( $" 1. Add new transaction\n" +
                            " 2. View transactions\n" +
                            " 3. Save and Exit\n", CC.Cyan);
            Program.Print("----------------------------------------------------------------------------\n", CC.DarkBlue);
            Program.Print(" Select an option: ", CC.Green);
            return GetKey();
        }
        public static string EditOptions()
        {
            Program.Print($"\n      Edit Menu".PadLeft(10), CC.DarkYellow);
            Program.Print("\n----------------------------------------------------------------------------\n", CC.DarkBlue);
            Program.Print( $" 1. Edit Description\n" +
                            " 2. Edit Date\n" +
                            " 3. Edit Amount\n" +
                            " 4. Remove Transaction\n" +
                            " 5. Back to Main Menu\n", CC.Cyan);
            Program.Print("----------------------------------------------------------------------------\n", CC.DarkBlue);
            Program.Print(" Select an option: ", CC.Green);
            return GetKey();
        }
        public static string ChooseTransaction()
        {
            Console.Clear();
            Program.Print($"\n      Choose Transaction Type".PadLeft(10), CC.DarkYellow);
            Program.Print("\n----------------------------------------------------------------------------\n", CC.DarkBlue);
            Program.Print( $" 1. Add Income \n" +
                            " 2. Add Expense \n" +
                            " 3. Back to Main Menu\n", CC.Cyan);
            Program.Print("----------------------------------------------------------------------------\n", CC.DarkBlue);
            Program.Print(" Select an option: ", CC.Green);
            return GetKey();
        }
        public static void StartAnimation()
        {
            string message = "  Welcome to the Money Manager ";
            string topLine = "\n\n --------------------------------- \n |";      // <-- method breaks if these dont
            string bottomLine = "|\n ---------------------------------\n\n";     // <-- have an even amount of chars
            Console.ForegroundColor = ConsoleColor.DarkBlue;    
            for (int i = 0; i < topLine.Length; i+=2)  // i+=2 only way i could increase animation speed
            {                                          // Sleep(1) and (10) seems to be the same
                Console.Write(topLine[i].ToString() + topLine[i+1].ToString());
                Thread.Sleep(10);
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < message.Length; i++) 
            {
                Console.Write(message[i].ToString());
                Thread.Sleep(15);
            }
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < bottomLine.Length; i+=2)
            {
                Console.Write(bottomLine[i].ToString() + bottomLine[i+1].ToString());
                Thread.Sleep(10);
            }

        }
    }      
}
