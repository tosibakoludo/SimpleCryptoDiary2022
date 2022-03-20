using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoDiary2022
{
    class Cryptocurrency : Currency
    {
        private double price;

        private long marketCap;

        private long volume24h;

        public Cryptocurrency(string code, string name, double price, long marketCap, long volume24)
        {
            this.Code = code;
            this.Name = name;
            this.Price = price;
            this.MarketCap = marketCap;
            this.Volume24h = volume24;
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

        public long MarketCap
        {
            get => marketCap;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Market capitalization cannot be a negative number.");
                }
                marketCap = value;
            }
        }

        public long Volume24h
        {
            get => volume24h;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Volume cannot be a negative number.");
                }
                volume24h = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + " Price = " + price + "\t| Market capitalization = " + marketCap + "\t| Volume (24h) = " + volume24h;
        }
    }
}
