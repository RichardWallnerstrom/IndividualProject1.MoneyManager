using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC = System.ConsoleColor;
using MoneyManager;
using System.Text.RegularExpressions;

namespace MoneyManager
{
    public class Transaction
    {
        public Transaction(string title, decimal amount, DateTime date, bool isIncome)
        {
            Title = title;
            Amount = amount;
            Date = date;
            IsIncome = isIncome;
        }

        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }
        public static List<Transaction> TransactionList { get; } = new List<Transaction>();
        private static (string, decimal, DateTime) GetTransactionData()
        {
            string title = GetValidTitle();
            decimal amount = GetValidAmount();
            DateTime date = GetValidDate();
            Console.Clear();
            return (title, amount, date);
        }
        private static string GetValidTitle()
        {
            Display.Print("\n 1. Title? ", CC.Cyan); // add error handling for null
            string title = Display.GetLine();
            while (title == null)
            {
                Display.Print($"\n\n 8{title} is not a valid title.\n You must type something\n\n", CC.Red);
                title = Display.GetLine();
            }
            return title;
        }
        private static decimal GetValidAmount()
        {
            Display.Print("\n 2. Amount? ", CC.Cyan);
            string input = Display.GetLine();
            decimal amount;
            while (!(decimal.TryParse(input, out amount) && amount > 0))
            {
                Display.Print($"\n\n 8{input} is not a valid number.\n Income should be above 0\n\n", CC.Red);
                input = Display.GetLine();
            }
            return amount;
        }
        private static DateTime GetValidDate()
        {
            Display.Print("\n 3. Date? ", CC.Cyan);
            string input = Display.GetLine();
            DateTime date;
            while (!(DateTime.TryParse(input, out date)))
            {
                Display.Print($"\n\n {input} is not a valid date.\n Try YEAR-MONTH-DAY \n\n", CC.Red);
                input = Display.GetLine();
            }
            return date.Date;
        }
        public static void AddTransaction()
        {
            string input = GetTransactionType();
            if (input == "-1") return;
            bool isIncome = input == "1";
            var objectFields = GetTransactionData();
            Transaction newTransaction = new Transaction(objectFields.Item1, objectFields.Item2, objectFields.Item3, isIncome);
            TransactionList.Add(newTransaction);
        }
        public static string GetTransactionType()
        {
            while (true)
            {
                Console.Clear();
                Display.Print($"\n      Choose Transaction Type".PadLeft(10), CC.DarkYellow);
                Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print( $" 1. Add Income \n" +
                                " 2. Add Expense \n" +
                                " 3. Back to Main Menu\n", CC.Cyan);
                Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print(" Select an option: ", CC.Green);
                string input = Display.GetLine();
                if (Regex.IsMatch(input, "^(1|2)$")) 
                    return input == "1" ? "1" : "2";
                else if (input == "3") 
                    return "-1";
                else 
                    Display.Print($" {input} is not a valid option \n Select an option between 1-3\n");
            }
        }
        public static void ViewTransactions()
        {
            decimal incomeTotal = 0, expensesTotal = 0;
            Console.Clear();
            Display.Print("\n Title".PadRight(20) + "Amount".PadRight(20) + "Date".PadRight(20) + "Type".PadRight(20), CC.DarkYellow);
            Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
            foreach (Transaction item in TransactionList)
            {
                
                Display.Print($"\n {item.Title.PadRight(20)} {item.Amount:C}".PadRight(20) + 
                    $" {item.Date.ToShortDateString()}".PadRight(20), CC.Cyan);
                if (item.IsIncome)
                {
                    Display.Print("Income", CC.Green);
                    incomeTotal += item.Amount;
                }
                else
                {
                    Display.Print("Expense", CC.Red);
                    expensesTotal += item.Amount;
                }
            }
            Display.Print("\n ----------------------------------------------------------------------------", CC.DarkBlue);
            Display.Print($"\n Total Balance is: {incomeTotal - expensesTotal}");
                
        }

        public static void EditTransaction()
        {
            string menuInput;
            while (true)
            {
                Display.Print($"\n      Edit Menu".PadLeft(10), CC.DarkYellow);
                Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print($" 1. Edit Title\n" +
                                " 2. Edit Amount\n" +
                                " 3. Edit Date\n" +
                                " 4. Remove Transaction\n" +
                                " 5. Back to Main Menu\n", CC.Cyan);
                Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print(" Select an option: ", CC.Green);
                menuInput = Display.GetKey();
                if (menuInput == "5") break;
                if (Regex.IsMatch(menuInput, "^[1-4]$"))
                {
                    Display.Print("\n Which transaction do you wish to edit? ");
                    string searchInput = Display.GetLine().ToLower();
                    Transaction transaction = TransactionList.FirstOrDefault(t => t.Title.ToLower() == searchInput);
                    if (transaction == null) 
                    {
                        Display.Print($" {searchInput} is not in the list!", CC.Red);
                        break;
                    } 
                    if (menuInput == "1") transaction.Title = GetValidTitle();
                    if (menuInput == "2") transaction.Amount = GetValidAmount();
                    if (menuInput == "3") transaction.Date = GetValidDate();
                }
                else Display.Print($" {menuInput} is not a valid option", CC.Red);
            }
        }
    }
}
