﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC = System.ConsoleColor;
using MoneyManager;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Drawing;

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
        public static List<Transaction> TransactionList { get; set; } = new List<Transaction>();
        public static decimal Inflation { get; set; }
        public static decimal Interest { get; set; }
        public static int YearsToProject { get; set; }

        public static void ViewTransactions(string transactionType = "3")
        {
            decimal incomeTotal = 0, expensesTotal = 0, startMoney = 0;
            Display.Print($"\n {"Title",-21} {"Amount",-18} {"Date",-19} {"Type",-18}", CC.DarkYellow); 
            Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
            foreach (Transaction item in TransactionList)
            {
                string incomeOrExpense = item.IsIncome ? "Income" : "Expense";
                ConsoleColor color = item.IsIncome ? ConsoleColor.Green : ConsoleColor.Red;
                if (transactionType == "3") // All transactions
                {
                    Display.Print($"\n {item.Title,-20}{item.Amount,-20:C}{IntToMonth(item.Date),-20}", CC.Cyan);
                    Display.Print(incomeOrExpense, color);
                    // Check if its monthly, yearly or single transaction and then calculate properly
                    if (item.IsIncome)
                    {
                        if (item.Date == 13 || item.Date == 0) // Yearly or monthly
                            incomeTotal += (item.Date == 13) ? item.Amount : item.Amount * 12;
                        else 
                            startMoney += item.Amount;
                    }
                    else
                    {
                        if (item.Date == 13 || item.Date == 0) // Yearly or monthly
                            expensesTotal += (item.Date == 13) ? item.Amount : item.Amount * 12;
                        else 
                            startMoney -= item.Amount;
                    }                     
                }
                else if (item.IsIncome && transactionType == "1")
                {
                    Display.Print($"\n {item.Title,-20}{item.Amount,-20:C}{IntToMonth(item.Date),-20}", CC.Cyan);
                    Display.Print(incomeOrExpense, color);
                    incomeTotal += (item.Date == 13) ? item.Amount : item.Amount * 12;
                }
                else if (!item.IsIncome && transactionType == "2")
                {
                    Display.Print($"\n {item.Title,-20}{item.Amount,-20:C}{IntToMonth(item.Date),-20}", CC.Cyan);
                    Display.Print(incomeOrExpense, color);
                    expensesTotal += (item.Date == 13) ? item.Amount : item.Amount * 12;
                }
            }
            Display.Print(startMoney.ToString());
            string balanceString = (YearsToProject == 0) ? "\n Projected Balance for this year: " : $"\nProjected balance for {YearsToProject} years: ";
            decimal yearlyIncrease = incomeTotal - expensesTotal; 
            decimal totalBalance = CalculateProjection(startMoney, yearlyIncrease);
            ConsoleColor balanceColor = totalBalance > 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
            Display.Print(balanceString);
            Display.Print($" {totalBalance:C}", balanceColor);
            Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
        }
        private static decimal CalculateProjection(decimal startMoney, decimal yearly)
        {
            decimal rate = 1 + Interest / 100;
            decimal result = (startMoney + yearly) * rate; 
            for (int i = 1; i < YearsToProject; i++)
            {
                result += yearly;
                result *= rate;
            }
            return result;
        }
        public static void ViewOptions()
        {
            string menuInput;
            while (true)
            {
                Display.Print($"\n      View Menu".PadLeft(10), CC.DarkYellow);
                Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print($" 1. Display Income\n" +
                                " 2. Display Expenses\n" +
                                " 3. Sort list\n" +
                                " 4. Back to Main Menu\n", CC.Cyan);
                Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print(" Select an option: ", CC.Green);
                menuInput = Display.GetKey();
                Console.Clear();
                if (menuInput == "4") break;
                if (menuInput == "1") ViewTransactions("1");
                else if (menuInput == "2") ViewTransactions("2");
                else if (menuInput == "3") SortTransactions();
                else
                {
                    Console.Clear();
                    Display.Print($"\n\n        {menuInput} is not a valid option\n\n", CC.Red);
                    ViewTransactions();
                }
            }
        }
        private static void SortTransactions()
        {
            Console.Clear();
            ViewTransactions();
            Display.Print("\n\n Sort list by: ", CC.Cyan);  // Sorting section
            string input = Console.ReadLine();
            if (input == null) 
            {
                Display.Print("\n\n     No input detected. \n\n", CC.Red);
                ViewTransactions();
                return;
            }
            Display.Print("\n\n Type: \"R\" to sort reversed.\n Type anything else to continue:  ", CC.Cyan);  // Reversing section
            bool wantsReversed = (Display.GetKey().ToLower() == "r"); 
            if (input.ToLower() == "title")
                TransactionList = (wantsReversed) ? TransactionList.OrderBy(t => t.Title).Reverse().ToList() : TransactionList.OrderBy(t => t.Title).ToList();
            else if (input.ToLower() == "amount")
                TransactionList = (wantsReversed) ? TransactionList.OrderBy(t => t.Amount).Reverse().ToList() : TransactionList.OrderBy(t => t.Amount).ToList();
            else if (input.ToLower() == "date")
                TransactionList = (wantsReversed) ? TransactionList.OrderBy(t => t.Date).Reverse().ToList() : TransactionList.OrderBy(t => t.Date).ToList();
            else if (input.ToLower() == "type")
                TransactionList = (wantsReversed) ? TransactionList.OrderBy(t => t.IsIncome).ToList() : TransactionList.OrderBy(t => t.IsIncome).Reverse().ToList();
            else
            {
                Console.Clear();
                Display.Print($"\n\n       {input} is not a valid attribute\n\n", CC.Red);
                return;
            }
            Console.Clear();
            if (!wantsReversed) Display.Print($"\n\n Sorting ascending by {input} \n", CC.Green);
            else Display.Print($"\n\n Sorting descending by {input}\n", CC.Green);
            ViewTransactions();
        }
        public static void EditTransaction()
        {
            string menuInput;
            while (true)
            {
                Display.Print($"\n      Edit Menu".PadLeft(10), CC.DarkYellow);
                Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print( $" 1. Edit Title\n" +
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
                    Display.Print("\n Which transaction do you wish to edit? ", CC.Cyan);
                    string searchInput = Display.GetLine().ToLower();
                    Transaction transaction = TransactionList.FirstOrDefault(t => t.Title.ToLower() == searchInput);
                    if (transaction == null)
                    {
                        Console.Clear();
                        Display.Print($"\n\n        {searchInput} is not in the list!\n\n", CC.Red);
                        break;
                    }
                    else if (menuInput == "1") transaction.Title = GetValidTitle();
                    else if (menuInput == "2") transaction.Amount = GetValidAmount();
                    else if (menuInput == "3") transaction.Date = GetValidMonth();
                    else if (menuInput == "4") TransactionList.Remove(transaction);
                    break;
                }
                else
                {
                    Console.Clear();
                    ViewTransactions();
                    Display.Print($"\n\n        {menuInput} is not a valid option\n\n", CC.Red);
                }
            }
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
        private static string GetTransactionType()
        {
            while (true)
            {
                Console.Clear();
                Display.Print($"\n      Choose Transaction Type".PadLeft(10), CC.DarkYellow);
                Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print($" 1. Add Income \n" +
                                " 2. Add Expense \n" +
                                " 3. Back to Main Menu\n", CC.Cyan);
                Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print(" Select an option: ", CC.Green);
                string input = Display.GetKey();
                Console.Clear();
                if (Regex.IsMatch(input, "^(1|2)$"))
                    return input == "1" ? "1" : "2";
                else if (input == "3")
                    return "-1";
                else
                    Display.Print($"        {input} is not a valid option \n Select an option between 1-3\n");
            }
        }
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
            string title = Display.GetLine().Trim();
            while (title == null || title.Length > 13)
            {
                Console.Clear();
                Display.Print($"\n\n        Title must be between 3 and 13 letters. \n\n", CC.Red);
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
            while (!(decimal.TryParse(input, out amount) && amount > 0 && amount <= 99999999))
            {
                Console.Clear();
                Display.Print($"\n\n        {input} is not a valid number.\n Value must be between 1 - 99 999 999\n\n", CC.Red);
                Display.Print("\n Enter Amount: ", CC.Cyan);
                input = Display.GetLine();
            }
            return amount;
        }
        private static int GetValidMonth()
        {
            Display.Print("\n Enter month: ", CC.Cyan);
            string input = Display.GetLine();
            while (ParseMonthToInt(input) < 0 || ParseMonthToInt(input) > 13 || ParseMonthToInt(input) == null)
            {
                Console.Clear();
                Display.Print( $"\n\n        {input} is not a valid month.\n\n " +
                                "       Type name of month or its number.\n " +
                                "       Write \"0\" or Monthly for recurring transactions \n\n", CC.Red);
                Display.Print("\n Enter month: ", CC.Cyan);
                input = Display.GetLine();
            }
            return ParseMonthToInt(input);
        }
        private static int ParseMonthToInt(string input)  //Accept either month number or month name ex "10" or "october"
        {
            DateTime parsedDate;
            int month;
            if (input == "0" || input.ToLower() == "monthly") return 0; //Monthly
            if (input == "13" || input.ToLower() == "yearly") return 0; //Yearly
            if (Int32.TryParse(input, out month)) return month;
            if (DateTime.TryParseExact(input, "MMMM", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return month = parsedDate.Month;
            }
            else
            {
                Display.Print($"\n\n        Failed to parse {input}\n\n");
                return month = 0; 
            }
        }
        public static string IntToMonth(int monthNumber) 
        {          
            if (monthNumber == 0) return "Monthly";
            if (monthNumber == 13) return "Yearly";
            DateTime date = new DateTime(DateTime.Now.Year, monthNumber, 1);
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
            return monthName;
        }  
    }
}
