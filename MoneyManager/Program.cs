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
            Print("\n\n -------------------------------- \n |", CC.DarkBlue);
            Print(" Welcome to the Money Manager ", CC.DarkYellow);
            Print("|\n -------------------------------- \n\n", CC.DarkBlue);
            List<Transaction> transactionList = new List<Transaction>();
            while (true)  // Main loop
            {
                string input = Display.MainMenu();
                if (input == "a" || input == "1")   // Add transaction loop
                {
                    while (true)
                    {
                        input = Display.ChooseTransaction();  // Add Income
                        if (input == "i" || input == "1")
                        {
                            var myTuple = Transaction.StartTransaction();
                            Transaction newTransaction = new Transaction(myTuple.Item1, myTuple.Item2, myTuple.Item3, true);
                            transactionList.Add(newTransaction);
                        }
                        else if (input == "e" || input == "2")  // Add expense
                        {
                            var myTuple = Transaction.StartTransaction();
                            Transaction newTransaction = new Transaction(myTuple.Item1, myTuple.Item2, myTuple.Item3, false);
                            transactionList.Add(newTransaction);
                        }
                        else if (input == "q" || input == "3")  // Return
                        {
                            Print("\n\n Returning to Main Menu\n\n", CC.Magenta);
                            break;
                        }
                        else
                        {
                            Program.Print($"{input} is not a valid option");
                        }
                    }
                }
                else if (input == "v" || input == "2")
                {
                    Transaction.ViewTransactions(transactionList);
                }
                else if (input == "q" || input == "4")
                {
                    Print("\n\n Saving and Exiting application...\n\n", CC.Red);
                    break;
                }
            }

        }
    }
}