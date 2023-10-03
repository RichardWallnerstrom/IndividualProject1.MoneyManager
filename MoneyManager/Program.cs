using System;
using System.Text.RegularExpressions;
using CC = System.ConsoleColor;

namespace MoneyManager
{
    internal class Program
    {
        public static void Print(string text, CC fgColor = CC.White, CC bgColor = CC.Black)
        {
            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;
            Console.Write(text);
            Console.ResetColor();
        }
        static void Main()
        {
            List<Transaction> transactionList = new List<Transaction>();
            Display.StartAnimation();
            while (true)  // Main loop
            {
                string input = Display.MainMenu();
                if (Regex.IsMatch(input, "^(1|a)$"))   // Add transaction loop
                {
                    while (true)
                    {
                        input = Display.ChooseTransaction(); // Add Income
                        if (Regex.IsMatch(input, "^(1|i)$"))
                        {
                            transactionList.Add(Transaction.AddTransaction(Regex.IsMatch(input, "^(1|i)$")));  //create boolean, then object
                        }
                        else if (Regex.IsMatch(input, "^(3|q|x)$"))  // Return
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Program.Print($"\n\n{input} is not a valid option\n\n");
                        }
                    }
                }
                else if (Regex.IsMatch(input, "^(2|v|d)$"))  // View transactions
                {
                    Transaction.ViewTransactions(transactionList);
                    input = Display.EditOptions();
                }
                else if (Regex.IsMatch(input, "^(3|q|x)$"))
                {
                    Print("\n\n Saving and Exiting application...\n\n", CC.Red);
                    break;
                }
            }

        }
    }
}