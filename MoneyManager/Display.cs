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
        public static string GetKey()
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
                            " 3. Edit transactions\n" +
                            " 4. Save and Exit\n", CC.Cyan);
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
            Program.Print($"\n      Choose Transaction Type".PadLeft(10), CC.DarkYellow);
            Program.Print("\n----------------------------------------------------------------------------\n", CC.DarkBlue);
            Program.Print( $" 1. Add Income \n" +
                            " 2. Add Expense \n" +
                            " 3. Back to Main Menu\n", CC.Cyan);
            Program.Print("----------------------------------------------------------------------------\n", CC.DarkBlue);
            Program.Print(" Select an option: ", CC.Green);
            return GetKey();
        }
    }      
}
