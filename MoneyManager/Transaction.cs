using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC = System.ConsoleColor;
using MoneyManager;
using System.Text.RegularExpressions;
using System.Globalization;

namespace MoneyManager
{
    public class Transaction
    {
        public Transaction(string title, decimal amount, int date, bool isIncome)
        {
            Title = title;
            Amount = amount;
            Date = date;
            IsIncome = isIncome;
        }

        public string Title { get; set; }
        public decimal Amount { get; set; }
        public int Date { get; set; }
        public bool IsIncome { get; set; }
        public static List<Transaction> TransactionList { get; } = new List<Transaction>();
        private static (string, decimal, int) GetTransactionData()  // Get all info for object creation
        {
            string title = GetValidTitle();
            decimal amount = GetValidAmount();
            int date = GetValidMonth();
            Console.Clear();
            return (title, amount, date);
        }
        private static string GetValidTitle()
        {
            Display.Print("\n Enter Title: ", CC.Cyan);
            string title = Display.GetLine();
            while (title == null)
            {
                Console.Clear();
                Display.Print($"\n\n {title} is not a valid title.\n You must type something\n\n", CC.Red);
                Display.Print("\n Enter Title: ", CC.Cyan);
                title = Display.GetLine();
            }
            return title;
        }
        private static decimal GetValidAmount()
        {
            Display.Print("\n Enter Amount: ", CC.Cyan);
            string input = Display.GetLine();
            decimal amount;
            while (!(decimal.TryParse(input, out amount) && amount > 0))
            {
                Console.Clear();
                Display.Print($"\n\n {input} is not a valid number.\n Value must be above 0\n\n", CC.Red);
                Display.Print("\n Enter Amount: ", CC.Cyan);
                input = Display.GetLine();
            }
            return amount;
        }
        private static int GetValidMonth()
        {
            Display.Print("\n Enter month: ", CC.Cyan);
            string input = Display.GetLine();
            while (ParseMonthToInt(input) <= 0 || ParseMonthToInt(input) >= 14 || ParseMonthToInt(input) == null)
            {
                Console.Clear();
                Display.Print($"\n\n {input} is not a valid month.\n Try name of month or number.\n Write 13 or Monthly for recurring transaction \n\n", CC.Red);
                Display.Print("\n Enter month: ", CC.Cyan);
                input = Display.GetLine();
            }
            return ParseMonthToInt(input);
        }
        private static int ParseMonthToInt(string input)  //Accept either month number or month name ex "10" or "october"
        {
            DateTime parsedDate;
            int month;
            if (input == "13" || input.ToLower() == "monthly") return Int32.Parse("13"); //Monthly
            if (Int32.TryParse(input, out month)) return month;
            if (DateTime.TryParseExact(input, "MMMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return month = parsedDate.Month;
            }
            else
            {
                Display.Print($" Failed to parse {input}");
                return month = 0; 
            }
        }
        private static string IntToMonth(int monthNumber) 
        {
            if (monthNumber == 0) return "0";
            
            if (monthNumber == 13) return "Monthly";
            DateTime date = new DateTime(DateTime.Now.Year, monthNumber, 1);
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
            return monthName;
            

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
                    $" {IntToMonth(item.Date)}".PadRight(20), CC.Cyan);
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
                    else if (menuInput == "1") transaction.Title = GetValidTitle();
                    else if (menuInput == "2") transaction.Amount = GetValidAmount();
                    else if (menuInput == "3") transaction.Date = GetValidMonth();
                    else if (menuInput == "4") TransactionList.Remove(transaction);
                    break;
                }
                else Display.Print($" {menuInput} is not a valid option", CC.Red);
            }
        }
    }
}
