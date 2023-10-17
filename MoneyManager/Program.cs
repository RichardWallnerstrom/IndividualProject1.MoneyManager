using System;
using System.Text.RegularExpressions;
using System.Text.Json;
using CC = System.ConsoleColor;

namespace MoneyManager
{
    internal class Program
    {
        private static void SaveToJson(string arg = "transactions.json")
        {
            string jsonString = JsonSerializer.Serialize(Transaction.TransactionList, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(arg, jsonString);
        }
        private static void LoadJson(string arg = "transactions.json")
        {
            try 
            { 
                string jsonString = File.ReadAllText(arg);
                Transaction.TransactionList = JsonSerializer.Deserialize<List<Transaction>>(jsonString);
            }
            catch (FileNotFoundException) 
            {
                Display.Print($"\n\n {arg} not found.\n ");
                Display.Print($"Will create {arg} upon saving and quitting. ", CC.Red); 
                Thread.Sleep(3000); 
                Console.Clear();
            }
        }
        static void Main()
        {
            LoadJson();
            //Transaction.LoadTestObjects();
            Display.StartAnimation();
            while (true) 
            {
                Display.Print($"\n      Main Menu".PadLeft(10), CC.DarkYellow);
                Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print($" 1. Add new transaction\n" +
                        " 2. View transactions\n" +
                        " 3. Edit transactions\n" +
                        " 4. Help and Options\n" +
                        " 5. Save and Exit\n", CC.Cyan);
                Display.Print(" ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print(" Select an option: ", CC.Green);
                string input = Display.GetKey();
                Console.Clear();
                if (Regex.IsMatch(input, "^(1|a)$"))   // Add 
                {
                    Transaction.AddTransaction();
                }
                else if (Regex.IsMatch(input, "^(2|v)$"))   // View
                {
                    Transaction.ViewTransactions();
                    Transaction.ViewOptions();
                }
                else if (Regex.IsMatch(input, "^(3|e)$"))   // Edit
                {
                    Transaction.ViewTransactions();
                    Transaction.EditTransaction();
                }
                else if (Regex.IsMatch(input, "^(4|o)$"))   // Options
                {
                    Display.Help();
                }
                else if (Regex.IsMatch(input, "^(5|x)$"))   // Quit
                {
                    SaveToJson();
                    Display.Print("\n\n     Saving transactions...!\n\n", CC.Green);
                    Display.Print("\n\n     Exiting application!\n\n", CC.Green);
                    break;
                }
            }
        }
    }
}