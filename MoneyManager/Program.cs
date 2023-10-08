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
            //Transaction.TransactionList.Add(new Transaction("Tip", 1, 1, true));
            //Transaction.TransactionList.Add(new Transaction("Rent", 8000, 0, false));
            //Transaction.TransactionList.Add(new Transaction("Electricity", 1000, 0, false));
            //Transaction.TransactionList.Add(new Transaction("Food", 2000, 0, false));
            //Transaction.TransactionList.Add(new Transaction("Taxes", 100000, 13, false));
            Transaction.TransactionList.Add(new Transaction("Tax Return", 50000, 2, true));

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
                else if (Regex.IsMatch(input, "^(4|o)$"))   // Options
                {
                    ApplicationSettings();
                }
                else if (Regex.IsMatch(input, "^(5|x)$"))   // Quit
                {
                    Display.Print("\n\n     Exiting application!\n\n", CC.Red);
                    break;
                }
            }

        }
        public static void ApplicationSettings()  //// TODO
        {
            Display.Print($"\n      Application Settings".PadLeft(10), CC.DarkYellow);
            Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
            Display.Print($" 1. Change interest modifier\n" +
                            " 2. Change inflation modifier\n" +
                            " 3. Change how many years to project\n" +
                            " 4. Back to Main Menu\n", CC.Cyan);
            Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
            Display.Print(" Select an option: ", CC.Green);
            string input = Display.GetKey();
            if (input == "1")
            {
                Display.Print("Enter the interest rate: ", CC.Cyan);
                input = Display.GetLine();
                decimal value;
                while (!Decimal.TryParse(input, out value) || value < 0 || value > 20)
                {
                    Display.Print($"\n\n Enter a valid number between 0 - 20\n\n", CC.Red);
                    Display.Print("Enter the interest rate: ", CC.Cyan);
                    input = Display.GetLine();
                }
                Transaction.Interest = value;
                Display.Print(Transaction.Interest.ToString());
                return;
            }
            else if (input == "2")
            {
                Display.Print("Enter the inflation rate: ", CC.Cyan);
                input = Display.GetLine();
                decimal value;
                while (!Decimal.TryParse(input, out value) || value < 0 || value > 20)
                {
                    Display.Print($"\n\n Enter a valid number between 0 - 20\n\n", CC.Red);
                    Display.Print("Enter the interest rate: ", CC.Cyan);
                    input = Display.GetLine();
                }
                Transaction.Inflation = value;
                return;
            }
            else if (input == "3")
            {
                Display.Print("Enter how many years to project: ", CC.Cyan);
                input = Display.GetLine();
                int value;
                while (!Int32.TryParse(input, out value) || value < 0 || value > 50)
                {
                    Display.Print($"\n\n Enter a valid number between 0 - 50\n\n", CC.Red);
                    Display.Print("Enter the interest rate: ", CC.Cyan);
                    input = Display.GetLine();
                }
                Transaction.YearsToProject = value;
                return;
            }
            else if (input == "4") return;
            else Display.Print($"\n\n        {input} is not a valid option\n\n", CC.Red);

        }
    }
}