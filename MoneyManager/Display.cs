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
        public static void Print(string text, CC fgColor = CC.White, CC bgColor = CC.Black)
        {
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            Console.Write(text);
            Console.ResetColor();
        }
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
        public static void StartMenu()
        {
            Print($"\n      Main Menu".PadLeft(10), CC.DarkYellow);
            Print("\n----------------------------------------------------------------------------\n", CC.DarkBlue);
            Print( $" 1. Add new transaction\n" +
                            " 2. View transactions\n" +
                            " 3. Save and Exit\n", CC.Cyan);
            Print("----------------------------------------------------------------------------\n", CC.DarkBlue);
            Print(" Select an option: ", CC.Green);
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
