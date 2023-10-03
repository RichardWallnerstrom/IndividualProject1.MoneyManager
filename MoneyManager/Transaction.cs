using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC = System.ConsoleColor;
using MoneyManager;

namespace MoneyManager
{
    public class Transaction
    {
        public Transaction(string title, decimal amount, DateTime date, bool IsIncome)
        {
            Title = title;
            Amount = amount;
            Date = date;
        }

        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; }
        public static (string, decimal, DateTime) StartTransaction()
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
                    Program.Print($"\n\n{input} is not a valid number.\n Income should be above 0\n\n", CC.Red);
                    input = Display.GetLine();
                }
                Program.Print("\n 3. Date? ", CC.Cyan);
                input = Display.GetLine();
                while (!(DateTime.TryParse(input, out date)))
                {
                    Program.Print($"\n\n{input} is not a valid date.\n Try YEAR-MONTH-DAY \n\n", CC.Red);
                    input = Display.GetLine();
                }
                return (title, amount, date);
            }
        }
        public static void ViewTransactions(List<Transaction> transactionList)
        {
            Program.Print($"\n Title Amount Date", CC.DarkYellow);
            foreach (Transaction item in transactionList)
                Program.Print($"\n {item.Title.PadRight(20)} {item.Amount} {item.Date}", CC.Cyan);
        }
    }
}
