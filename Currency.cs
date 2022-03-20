using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoDiary2022
{
    abstract class Currency
    {
        protected string code;

        protected string name;

        public string Code
        {
            get => code;
            set
            {
                if (value.Length > 5)
                {
                    throw new Exception("Currency code cannot be longer than 5, e.g. RSD, EUR, USD, BTC, ETH, USDT, USDC.");
                }
                code = value;
            }
        }

        public string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return name + " (" + code + ")\t=>";
        }
    }
}
