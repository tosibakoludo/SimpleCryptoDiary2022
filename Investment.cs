using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoDiary2022
{
    class Investment
    {
        private int sequenceNumber;

        private string cryptocurrencyCode;

        private double amount;

        private double price;

        private string month;

        public Investment(int sequenceNumber, string cryptocurrencyCode, double amount, double price, string month)
        {
            this.SequenceNumber = sequenceNumber;
            this.CryptocurrencyCode = cryptocurrencyCode;
            this.Amount = amount;
            this.Price = price;
            this.Month = month;
        }

        public int SequenceNumber
        {
            get => sequenceNumber;
            set
            {
                if (value < 1)
                {
                    throw new Exception("A sequence number cannot be a negative number or 0.");
                }
                sequenceNumber = value;
            }
        }

        public string CryptocurrencyCode
        {
            get => cryptocurrencyCode;
            set
            {
                if (value.Length > 5)
                {
                    throw new Exception("Cryptocurrency code cannot be longer than 5, e.g. BTC, ETH, USDT, USDC, LUNA, SOL, AVAX.");
                }
                cryptocurrencyCode = value;
            }
        }

        public double Amount
        {
            get => amount;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Amount cannot be a negative number.");
                }
                amount = value;
            }
        }

        public double Price
        {
            get => price;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Price cannot be a negative number.");
                }
                price = value;
            }
        }

        public string Month
        {
            get => month;
            set
            {
                if (value != "january"
                    && value != "february"
                    && value != "march"
                    && value != "april"
                    && value != "may"
                    && value != "june"
                    && value != "july"
                    && value != "august"
                    && value != "september"
                    && value != "october"
                    && value != "november"
                    && value != "december")
                {
                    throw new Exception("The month can be: January, February, March, April, May, June, July, August, September, October, November, December");
                }
                month = value[0].ToString().ToUpper() + value.Substring(1, value.Length - 1);
            }
        }

        public override string ToString()
        {
            return sequenceNumber + "\t" + cryptocurrencyCode + "\t" + amount + "\t" + price + "\t" + month;
        }
    }
}
