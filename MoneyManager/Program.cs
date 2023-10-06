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
            Transaction.TransactionList.Add(new Transaction("Salary", 16000, 10, true));
            Transaction.TransactionList.Add(new Transaction("Tip", 1600, 11, true));
            Transaction.TransactionList.Add(new Transaction("Rent", 8000, 6, false));
            Transaction.TransactionList.Add(new Transaction("Electricity", 1000, 4, false));
            Transaction.TransactionList.Add(new Transaction("Food", 2000, 0, false));

            //Display.StartAnimation();
            while (true)  // Main loop
            {
                Display.StartMenu();
                string input = Display.GetKey();
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