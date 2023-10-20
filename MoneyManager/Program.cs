using System;
using System.Text.RegularExpressions;
using System.Text.Json;
using CC = System.ConsoleColor;

namespace MoneyManager
{
    internal class Program
    {
        public static string FileName;
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
                    Display.Print(" Type \"q\" to exit without saving.\n " +
                        "Type anything else to save: ");
                    input = Display.GetKey();
                    Console.Clear();
                    if (input == "q") 
                    {
                        Display.Print("\n Exiting without saving!");
                        break;
                    }
                    else
                    {
                        SaveToJson();
                        Display.Print("\n Exiting application!");
                        break;
                    }
                }
            }
        }
        private static void SaveToJson()
        {
            Display.Print($" Enter filename to save to (leave blank to use file: {FileName}): ");
            string fileName = Display.GetLine();
            FileName = (fileName == String.Empty) ? FileName : fileName;
            string jsonString = JsonSerializer.Serialize(Transaction.TransactionList, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            try
            {
                Display.Print($"\n\n     Writing to {FileName}\n\n", CC.Green);
                File.WriteAllText(FileName, jsonString);
                Display.Print("\n\n     Transactions saved successfully!\n\n", CC.Green);
            }
            catch (IOException ex)
            {
                Display.Print($" Input error! \n", CC.Red);
                Display.Print($"{fileName} is not valid .\n" +
                                $" Use only letters, numbers and periods for the file name.  \n");
                Display.Print($"{ex.Message} \n", CC.Red);
            }
            catch (UnauthorizedAccessException ex)  //If no permission
            {
                Display.Print($"\n\n Cannot write to {FileName}!! " +
                                "\n\n File may be write protected." +
                                "\n Or you maybe have used / in file name. " +
                                $"\n {ex.Message}\n", CC.Red);
            }
        }
        private static void LoadJson()
        {
            Display.Print($" Enter filename to load (leave blank for default): ");
            string fileName = Display.GetLine();
            fileName = (fileName == String.Empty || fileName == " ") ? "transactions.json" : fileName;
            try
            {
                string jsonString = File.ReadAllText(fileName);
                Transaction.TransactionList = JsonSerializer.Deserialize<List<Transaction>>(jsonString);
                Display.Print($"\n\n Loaded file: {fileName}.\n ", CC.Green);
                FileName = fileName;
            }
            catch (FileNotFoundException)
            {
                Display.Print($"\n\n {fileName} not found.\n ", CC.Red);
                Display.Print($" Will create {fileName} upon saving. \n", CC.Green);
                FileName = fileName;
                Thread.Sleep(3000);
                Console.Clear();
            }
            catch (JsonException ex)
            {
                Display.Print($" Failed to load json file. It might be corrupted. \n", CC.Red);
                Display.Print(" If you have manually edited it try to restore it.\n" +
                                $" Otherwise delete {fileName} and we will create a new one for you. \n");
                Display.Print($"{ex.Message} \n", CC.Red);
                Environment.Exit(0);
            }
            catch (UnauthorizedAccessException ex)
            {
                Display.Print($" Unauthorized action! \n", CC.Red);
                Display.Print(" You prbably tried to use / in your file path.\n" +
                                $" Use only letters, numbers and periods for the file name.  \n");
                Display.Print($"{ex.Message} \n", CC.Red);
                Environment.Exit(0);
            }
            catch (IOException ex)
            {
                Display.Print($" Input error! \n", CC.Red);
                Display.Print($"{fileName} is not valid .\n" +
                                $" Use only letters, numbers and periods for the file name.  \n");
                Display.Print($"{ex.Message} \n", CC.Red);
                Environment.Exit(0);
            }
        }
    }
}