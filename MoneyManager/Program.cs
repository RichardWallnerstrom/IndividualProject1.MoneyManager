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
            Transaction.TransactionList.Add(new Transaction("Salary", 16000, new DateTime(2023,10,26), true));
            Transaction.TransactionList.Add(new Transaction("Tip", 1600, new DateTime(2023,10,26), true));
            Transaction.TransactionList.Add(new Transaction("Rent", 8000, new DateTime(2023,10,26), false));
            Transaction.TransactionList.Add(new Transaction("Electricity", 1000, new DateTime(2023,10,26), false));
            Transaction.TransactionList.Add(new Transaction("Food", 2000, new DateTime(2023,10,26), false));

            Display.StartAnimation();
            while (true)  // Main loop
            {
                Program.Print($"\n      Main Menu".PadLeft(10), CC.DarkYellow);
                Program.Print("\n----------------------------------------------------------------------------\n", CC.DarkBlue);
                Program.Print($" 1. Add new transaction\n" +
                                " 2. View transactions\n" +
                                " 3. Save and Exit\n", CC.Cyan);
                Program.Print("----------------------------------------------------------------------------\n", CC.DarkBlue);
                Program.Print(" Select an option: ", CC.Green);
                string input = Display.GetKey();
                if (Regex.IsMatch(input, "^(1|a)$"))   // Add transaction loop
                {
                    Transaction.AddTransaction();
                }
                else if (Regex.IsMatch(input, "^(2|v)$"))   // View
                {
                    Transaction.ViewTransactions();
                }
                else if (Regex.IsMatch(input, "^(3|x)$"))
                {
                    Program.Print("\n\n     Exiting application!\n\n", CC.Red);
                    break;
                }
            }

        }
    }
}