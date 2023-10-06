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
            Transaction.TransactionList.Add(new Transaction("Salary", 16000, new DateTime(2023,10,26), true));
            Transaction.TransactionList.Add(new Transaction("Tip", 1600, new DateTime(2023,10,26), true));
            Transaction.TransactionList.Add(new Transaction("Rent", 8000, new DateTime(2023,10,26), false));
            Transaction.TransactionList.Add(new Transaction("Electricity", 1000, new DateTime(2023,10,26), false));
            Transaction.TransactionList.Add(new Transaction("Food", 2000, new DateTime(2023,10,26), false));

            //Display.StartAnimation();
            while (true)  // Main loop
            {
                Display.StartMenu();
                string input = Display.GetKey();
                if (Regex.IsMatch(input, "^(1|a)$"))   // Add 
                {
                    Transaction.AddTransaction();
                }
                else if (Regex.IsMatch(input, "^(2|v)$"))   // View
                {
                    Transaction.ViewTransactions();
                    Transaction.EditTransaction();
                }
                else if (Regex.IsMatch(input, "^(3|x)$"))   // Quit
                {
                    Display.Print("\n\n     Exiting application!\n\n", CC.Red);
                    break;
                }
            }

        }
    }
}