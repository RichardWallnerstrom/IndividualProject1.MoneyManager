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
    public static class Display  
    {
        public static void Print(string text, CC fgColor = CC.White, CC bgColor = CC.Black)
        {
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            Console.Write(text);
        }
        public static string GetKey()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); 
            char userInput = char.ToLower(keyInfo.KeyChar);
            return (userInput.ToString() != null) ? userInput.ToString() : string.Empty;
        }
        public static string GetLine()
        {
            string userInput = Console.ReadLine();
            return (userInput != null) ? userInput : string.Empty;
        }
        public static void Help()
        {
            while (true)
            {
                Display.Print($"\n      Help Menu".PadLeft(10), CC.DarkYellow);
                Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print($" 1. Adding or editing\n" +
                                " 2. Calculating interest\n" +
                                " 3. Display/Hide start animation\n" +
                                " 4. Back to Main Menu\n", CC.Cyan);
                Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print(" Select an option: ", CC.Green);
                string menuInput = Display.GetKey();
                if (menuInput == "4") break;

                if (menuInput == "1")
                {
                    Display.Print("\n Adding transactions \n",CC.DarkYellow);
                    Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
                    Display.Print("\n   When adding a new transaction choose if you are adding an income or expense.\n" +
                                    "   When choosing month, please note that any transaction with an assigned month is considered\n" +
                                    "   a one time transaction for this year only. It will not be taken into account for following years.\n" +
                                    "   If the transaction is recurring use instead the \"monthly\" or \"yearly\" for that transaction.\n\n", CC.Cyan);
                }
                else if (menuInput == "2")
                {
                    Display.Print("\n Calculating Interest \n", CC.DarkYellow);
                    Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
                    Display.Print("\n   When calculating interest you have to specify interest rate, compound frequency and time period.\n" +
                                    "   All transactions with an assigned month will be calculated for the first year alone. \n" +
                                    "   Monthly and Yearly transactions will be calculated for each year in the time period.\n" +
                                    "   If you are not sure about compound frequency use 12. This indicates that the interest" +
                                    "   will be calculated once every 12 months which is quite common.\n\n", CC.Cyan);
                }
                else if (menuInput == "3")
                {
                }
                
            }
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
