using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoDiary2022
{
    class User
    {
        private string name, surname;

        private List<Investment> investments;

        public User(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
            investments = new List<Investment>();
        }

        public string Name { get => name; set => name = value; }

        public string Surname { get => surname; set => surname = value; }

        public List<Investment> Investments { get => investments; }

        //indexer
        public Investment this[int index]
        {
            get
            {
                return investments[index];
            }
            set
            {
                investments[index] = value;
            }
        }

        public int SequenceNumber()
        {
            return investments.Count + 1;
        }

        public string LargestInvestmentCurrency()
        {
            if (investments.Count > 0)
            {
                Dictionary<string, double> PR = new Dictionary<string, double>();
                foreach (Investment investment in investments)
                {
                    if (PR.Keys.Contains(investment.CryptocurrencyCode))
                    {
                        PR[investment.CryptocurrencyCode] += investment.Amount;
                    }
                    else
                    {
                        PR[investment.CryptocurrencyCode] = investment.Amount;
                    }
                }
                double maxAmount = PR.Values.Max();
                string largestInvestmentCurrency = "";
                foreach (string CryptocurrencyCode in PR.Keys)
                {
                    if (PR[CryptocurrencyCode] == maxAmount)
                    {
                        largestInvestmentCurrency = CryptocurrencyCode;
                    }
                }
                return largestInvestmentCurrency;
            }
            else
            {
                return "X";
            }
        }

        public override string ToString()
        {
            return name + " " + surname;
        }
    }
}
