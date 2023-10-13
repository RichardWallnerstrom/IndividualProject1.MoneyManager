using System;
using System.Text.RegularExpressions;
using MoneyManager;
using CC = System.ConsoleColor;

namespace MoneyManager
{
    internal class Program
    {
        static void Main()
        {
            Transaction.TransactionList.Add(new Transaction("Salary", 25000, 0, true));
            Transaction.TransactionList.Add(new Transaction("Tip", 1000, 1, true));
            Transaction.TransactionList.Add(new Transaction("Rent", 8000, 0, false));
            Transaction.TransactionList.Add(new Transaction("Electricity", 1000, 0, false));
            Transaction.TransactionList.Add(new Transaction("Food", 2000, 0, false));
            Transaction.TransactionList.Add(new Transaction("Taxes", 100000, 13, false));
            Transaction.TransactionList.Add(new Transaction("Tax Return", 50000, 2, true));

            //Display.StartAnimation();
            while (true) 
            {
                Display.Print($"\n      Main Menu".PadLeft(10), CC.DarkYellow);
                Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print($" 1. Add new transaction\n" +
                        " 2. View transactions\n" +
                        " 3. Edit transactions\n" +
                        " 4. Save and Exit\n", CC.Cyan);
                Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print(" Select an option: ", CC.Green); string input = Display.GetKey();
                if (Regex.IsMatch(input, "^(1|a)$"))   // Add 
                {
                    Console.Clear();
                    Transaction.AddTransaction();
                }
                else if (Regex.IsMatch(input, "^(2|v)$"))   // View
                {
                    Console.Clear();
                    Transaction.ViewTransactions();
                    Transaction.ViewOptions();
                }
                else if (Regex.IsMatch(input, "^(3|e)$"))   // Edit
                {
                    Console.Clear();
                    Transaction.ViewTransactions();
                    Transaction.EditTransaction();
                }
                else if (Regex.IsMatch(input, "^(4|x)$"))   // Quit
                {
                    Display.Print("\n\n     Exiting application!\n\n", CC.Red);
                    break;
                }
            }
        }
    }
}