using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoDiary2022
{
    class Program
    {
        public static SimpleCryptoDiary2022DataSetTableAdapters.KorisnikTableAdapter ad1 = new SimpleCryptoDiary2022DataSetTableAdapters.KorisnikTableAdapter();

        public static SimpleCryptoDiary2022DataSetTableAdapters.UlaganjeTableAdapter ad2 = new SimpleCryptoDiary2022DataSetTableAdapters.UlaganjeTableAdapter();

        static void Main()
        {

            List<Cryptocurrency> cryptocurrencies = new List<Cryptocurrency>();

            CurrentMarketState(cryptocurrencies);

            List<User> users = LoadUsersFromDB();
            LoadInvestmentsFromDB(users);

            bool permission = true;
            while (permission)
            {
                Console.WriteLine("Select option: ");
                Console.WriteLine("(1) Log In");
                Console.WriteLine("(2) Register");
                Console.WriteLine("(3) Exit");
                Console.Write(":");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    Console.Clear();
                    CurrentMarketState(cryptocurrencies);
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    bool found = false;
                    foreach (User user in users)
                    {
                        if (name == user.Name)
                        {
                            Console.WriteLine("Hello, " + name + "!");
                            Console.WriteLine("Find a list of your investments below: ");
                            Console.WriteLine();
                            if (user.Investments.Count == 0)
                            {
                                Console.WriteLine("No investments recorded.");
                            }
                            else
                            {                                        
                                Console.WriteLine("SEQ.NO.|CRYPTO |AMOUNT |PRICE  |MONTH ");
                                Console.WriteLine();
                                foreach (Investment i in user.Investments)
                                {
                                    Console.WriteLine(i);
                                }
                            }
                            Console.WriteLine();
                            found = true;
                            bool addInvestment = true;
                            while (addInvestment)
                            {
                                Console.Write("Do you want to add a new investment (Yes/No): ");
                                string answer = Console.ReadLine().ToLower(); 
                                if (answer == "yes")
                                {
                                    Console.Write("Enter cryptocurrency code (e.g. BTC): ");
                                    string cryptocurrencyCode = Console.ReadLine().ToUpper();
                                    Console.Write("Enter investment amount (EUR): ");
                                    double amount;
                                    try
                                    {
                                        amount = double.Parse(Console.ReadLine());
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("The amount must be a number");
                                        continue;
                                    }
                                    Console.Write("Enter the price of the cryptocurrency (USD): ");
                                    double price;
                                    try
                                    {
                                        price = double.Parse(Console.ReadLine());
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("The price must be a number");
                                        continue;
                                    }
                                    Console.Write("Enter the month: (e.g. January): ");
                                    string month = Console.ReadLine().ToLower();
                                    int sequenceNumber = user.SequenceNumber();
                                    Investment ni;
                                    try
                                    {
                                        ni = new Investment(sequenceNumber, cryptocurrencyCode, amount, price, month);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                        continue;
                                    }
                                    ad2.Insert(user.Name, ni.SequenceNumber, ni.CryptocurrencyCode, ni.Amount, ni.Price, ni.Month);
                                    LoadInvestmentsFromDB(users);
                                    Console.Clear();
                                    CurrentMarketState(cryptocurrencies);
                                    Console.WriteLine("Well done, " + name + "!");
                                    Console.WriteLine();
                                    Console.WriteLine("Find a list of your investments below: ");
                                    Console.WriteLine();
                                    if (user.Investments.Count == 0)
                                    {
                                        Console.WriteLine("No investments recorded.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("SEQ.NO.| CRYPTO | AMOUNT | PRICE | MONTH ");
                                        Console.WriteLine();
                                        foreach (Investment i in user.Investments)
                                        {
                                            Console.WriteLine(i);
                                        }
                                    }
                                    Console.WriteLine();
                                }
                                else
                                {
                                    addInvestment = false;
                                    Console.Clear();
                                    CurrentMarketState(cryptocurrencies);
                                }
                            }
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine("User not found, try again or register a new user.");
                    }
                }
                else if (option == "2")
                {
                    Console.Clear();
                    CurrentMarketState(cryptocurrencies);
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Surname: ");
                    string surname = Console.ReadLine();
                    User u = new User(name, surname);
                    ad1.Insert(u.Name, u.Surname);
                    users = LoadUsersFromDB();
                    LoadInvestmentsFromDB(users);
                    Console.WriteLine("You have successfully registered. You can sign up now.");
                }
                else if (option == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Goodbye!\n");
                    permission = false;
                }
            }

            ReportProgramUsage(users);
        }

        private static void ReportProgramUsage(List<User> users)
        {
            string content = "";
            foreach (User user in users)
            {
                content += "User: ";
                content += user.ToString().ToUpper() + "\n";
                if (user.Investments.Count == 0)
                {
                    content += "\nNo investments recorded." + "\n";
                }
                else
                {
                    content += "\nSEQ.NO.| CRYPTO | AMOUNT | PRICE | MONTH \n";
                    foreach (Investment i in user.Investments)
                    {
                        content += i + "\n";
                    }
                }
                content += "\nThe user has invested the most in: " + user.LargestInvestmentCurrency() + "\n";
                content += "\n\n";
            }
            Console.WriteLine(content);
            string file = "report" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".txt";
            System.IO.File.WriteAllText(file, content);
            Console.WriteLine("The report of program usage " + file + " was saved successfully.");
            Console.ReadLine();
        }

        private static void CurrentMarketState(List<Cryptocurrency> cryptocurrencies)
        {
            Console.WriteLine("Sources:");
            try
            {
                string json = System.IO.File.ReadAllText("cryptocurrencies.json");
                cryptocurrencies = JsonConvert.DeserializeObject<List<Cryptocurrency>>(json);
                Console.WriteLine("- cryptocurrencies.json");
            }
            catch
            {
                Cryptocurrency crypto1 = new Cryptocurrency("BTC ", "Bitcoin  ", 40000, 765000000000, 30000000000);
                Cryptocurrency crypto2 = new Cryptocurrency("ETH ", "Ethereum ", 2700, 320000000000, 15000000000);
                Cryptocurrency crypto3 = new Cryptocurrency("USDT", "Tether   ", 1, 79000000000, 51000000000);
                cryptocurrencies.Add(crypto1);
                cryptocurrencies.Add(crypto2);
                cryptocurrencies.Add(crypto3);
                Console.WriteLine("- programmatically generated data");
                System.IO.File.WriteAllText("cryptocurrencies.json", JsonConvert.SerializeObject(cryptocurrencies));
            }

            try
            {
                string[] lines = System.IO.File.ReadAllLines("cryptocurrencies.txt");
                foreach (string linija in lines)
                {
                    string[] arr = linija.Split('\t');
                    Cryptocurrency crypto = new Cryptocurrency(arr[0], arr[1], double.Parse(arr[2]), long.Parse(arr[3]), long.Parse(arr[4]));
                    cryptocurrencies.Add(crypto);
                }
                Console.WriteLine("- cryptocurrencies.txt");
            }
            catch
            {

            }
            Console.WriteLine("\nCURRENT SITUATION ON THE CRYPTO MARKET: ");
            foreach (Cryptocurrency crypto in cryptocurrencies)
            {
                Console.WriteLine(crypto);
            }
            Console.WriteLine();
        }

        private static void LoadInvestmentsFromDB(List<User> users)
        {
            SimpleCryptoDiary2022DataSet.UlaganjeDataTable t = new SimpleCryptoDiary2022DataSet.UlaganjeDataTable();

            ad2.Fill(t);

            foreach (User user in users)
            {
                user.Investments.Clear();
                foreach (DataRow r in t.Rows)
                {
                    if (r[0].ToString() == user.Name)
                    {
                        Investment i = new Investment(int.Parse(r[1].ToString()), r[2].ToString(), double.Parse(r[3].ToString()), double.Parse(r[4].ToString()), r[5].ToString());
                        user.Investments.Add(i);
                    }
                }
            }
        }

        private static List<User> LoadUsersFromDB()
        {
            List<User> users = new List<User>();
            SimpleCryptoDiary2022DataSet.KorisnikDataTable t = new SimpleCryptoDiary2022DataSet.KorisnikDataTable();

            ad1.Fill(t);

            foreach (DataRow r in t.Rows)
            {
                User user = new User(r[0].ToString(), r[1].ToString());
                users.Add(user);
            }

            return users;
        }
    }
}