﻿using System;
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
            try
            {
                Display.Print($"\n\n     Writing to {arg}\n\n", CC.Green);
                File.WriteAllText(arg, jsonString);
                Display.Print("\n\n     Transactions saved successfully!\n\n", CC.Green);
            }
            catch (UnauthorizedAccessException ex)
            {
                Display.Print($"\n\n Cannot write to {arg}!! " +
                                "\n\n File may be write protected. " +
                                $"\n {ex.Message}\n", CC.Red);
                try
                {
                    Display.Print("\n Attempting to write to: transactionslist-backup.json", CC.Green);
                    File.WriteAllText("transactionslist-backup.json", jsonString);
                    Display.Print("\n\n     Transactions saved successfully!\n\n", CC.Green);
                }
                catch (UnauthorizedAccessException ex2)
                {
                    Display.Print($"\n\n Cannot write to transactionslist-backup.json either " +
                                    "\n\n You probably dont have permission to write in that directory " +
                                    $"\n {ex2.Message}\n", CC.Red);
                }
            }            
        }
        private static void LoadJson(string arg = "transactions.json")
        {
            try 
            { 
                string jsonString = File.ReadAllText(arg);
                Transaction.TransactionList = JsonSerializer.Deserialize<List<Transaction>>(jsonString);
                Display.Print($"\n\n Loaded file: {arg}.\n ", CC.Green);
            }
            catch (FileNotFoundException) 
            {
                Display.Print($"\n\n {arg} not found.\n ", CC.Red);
                Display.Print($" Will create {arg} upon saving and quitting. \n", CC.Green); 
                Thread.Sleep(3000); 
                Console.Clear();
            }
            catch (JsonException ex)
            {
                Display.Print($" Failed to load json file. It might be corrupted. \n", CC.Red);
                Display.Print(" If you have manually edited it try to restore it.\n" +
                                $" Otherwise delete {arg} and we will create a new one for you. \n");
                Display.Print($"{ex.Message} \n", CC.Cyan);
                Environment.Exit(1);
            }
        }
        static void Main()
        {
            Console.SetWindowSize(77, 40);
            //Transaction.LoadTestObjects();
            Display.StartAnimation();
            LoadJson();
            while (true) 
            {
                Display.Print($"\n      Main Menu".PadLeft(10), CC.DarkYellow);
                Display.Print("\n ----------------------------------------------------------------------------\n", CC.DarkBlue);
                Display.Print($" 1. Add new transaction\n" +
                        " 2. View transactions\n" +
                        " 3. Edit transactions\n" +
                        " 4. Save transactions\n" +
                        " 5. Help and Usage\n" +
                        " 6. Exit\n", CC.Cyan);
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
                else if (Regex.IsMatch(input, "^(4|o)$"))   // Save
                {
                    SaveToJson();
                }
                else if (Regex.IsMatch(input, "^(5|o)$"))   // Help
                {
                    Display.Help();
                }
                else if (Regex.IsMatch(input, "^(6|x)$"))   // Quit
                {
                    SaveToJson();
                    Display.Print("\n\n     Exiting application!\n\n", CC.Green);
                    break;
                }
            }
        }
    }
}