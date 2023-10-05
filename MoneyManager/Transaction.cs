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
            Program.Print("\n 1. Title? ", CC.Cyan);
            string title = Display.GetLine();
            decimal amount;
            DateTime date;
            while (true) 
            {
                Program.Print("\n 2. Amount? ", CC.Cyan);
                string input = Display.GetLine();
                while (!(decimal.TryParse(input, out amount) && amount > 0))
                {
                    Program.Print($"\n\n 8{input} is not a valid number.\n Income should be above 0\n\n", CC.Red);
                    input = Display.GetLine();
                }
                Program.Print("\n 3. Date? ", CC.Cyan);
                input = Display.GetLine();
                while (!(DateTime.TryParse(input, out date)))
                {
                    Program.Print($"\n\n {input} is not a valid date.\n Try YEAR-MONTH-DAY \n\n", CC.Red);
                    input = Display.GetLine();
                }
                date = date.Date;
                Console.Clear();
                return (title, amount, date);
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
        public static string GetTransactionType()
        {
            while (true)
            {
                Console.Clear();
                Program.Print($"\n      Choose Transaction Type".PadLeft(10), CC.DarkYellow);
                Program.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Program.Print( $" 1. Add Income \n" +
                                " 2. Add Expense \n" +
                                " 3. Back to Main Menu\n", CC.Cyan);
                Program.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Program.Print(" Select an option: ", CC.Green);
                string input = Display.GetLine();
                if (Regex.IsMatch(input, "^(1|2)$")) 
                    return input == "1" ? "1" : "2";
                else if (input == "3") 
                    return "-1";
                else 
                    Program.Print($" {input} is not a valid option \n Select an option between 1-3\n");
            }
        }
        public static void ViewTransactions()
        {
            decimal incomeTotal = 0, expensesTotal = 0; 
            Program.Print("\n Title".PadRight(20) + "Amount".PadRight(20) + "Date".PadRight(20) + "Type".PadRight(20), CC.DarkYellow);
            Program.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
            foreach (Transaction item in TransactionList)
            {
                
                Program.Print($"\n {item.Title.PadRight(20)} {item.Amount:C}".PadRight(20) + 
                    $" {item.Date.ToShortDateString()}".PadRight(20), CC.Cyan);
                if (item.IsIncome)
                {
                    Program.Print("Income", CC.Green);
                    incomeTotal += item.Amount;
                }
                else
                {
                    Program.Print("Expense", CC.Red);
                    expensesTotal += item.Amount;
                }
            }
            Program.Print("\n ----------------------------------------------------------------------------", CC.DarkBlue);
            Program.Print($"\n Total Balance is: {incomeTotal - expensesTotal}");
                
        }
        public static Transaction FindTransaction()
        {
            Program.Print("\n Which transaction do you wish to edit? ");
            string input = Display.GetLine();
            Transaction transaction = TransactionList.FirstOrDefault(t => t.Title == input);
            if ( transaction == null )
            {
                Program.Print($"\n\nCouldn't find {input}\n\n", CC.Red);
                return transaction; //// fix!!
            }
            else return transaction;
        }
        public static void EditTransaction()
        {
            Program.Print($"\n      Edit Menu".PadLeft(10), CC.DarkYellow);
            Program.Print("\n----------------------------------------------------------------------------\n", CC.DarkBlue);
            Program.Print( $" 1. Edit Title\n" +
                            " 2. Edit Date\n" +
                            " 3. Edit Amount\n" +
                            " 4. Remove Transaction\n" +
                            " 5. Back to Main Menu\n", CC.Cyan);
            Program.Print("----------------------------------------------------------------------------\n", CC.DarkBlue);
            Program.Print(" Select an option: ", CC.Green);
            string input = Display.GetKey();
            if (Regex.IsMatch(input, "^(1-4)$"))
            {
                Transaction foundTransaction = FindTransaction();
                input = Display.GetLine();
                if (input != null)
                {

                }


            }
        }
    }
}
