using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace L2Rarity.Shared
{
    public class Rankings
    {
        public string? collectionid { get; set; }
        public string? name { get; set; }

        public string? collectionname { get; set; }

        public string? marketplaceurl { get; set; }

        public string? contractAddress { get; set; }
        public int total { get; set; }
        public bool? pe { get; set; }
        public List<Ranking> rankings { get; set; } = new List<Ranking>();

    }
    public class Ranking
    {
        public string? i { get; set; } //id
        public int r { get; set; } //rank
        public decimal s { get; set; } //score

        public string? im { get; set; } //image

        public string? ni { get; set; } //nft id
        public string? ti { get; set; } //token id
        public string? p1 { get; set; } //price 1 hour ago

        private CultureInfo Culture = CultureInfo.InvariantCulture;

        public decimal p1Decimal
        {
            get
            {
                if(string.IsNullOrEmpty(p1))
                {
                    return 0;
                }
                else
                {
                    return Decimal.Parse(@ToString(Double.Parse(p1!), 18), NumberStyles.Number);
                }
            }
        }

        private decimal ToDecimal(Double? balance, int? decimals, decimal conversionRate = 1)
        {
            if (balance == null) return 0;
            return (decimal)(((decimals ?? 0) > 0) ? balance / Math.Pow(10, (double)decimals!) : balance) * conversionRate;
        }

        private string ToString(Double? balance, int? decimals, decimal conversionRate = 1)
        {
            if (balance == null) return "";

            decimal floatBalance = ToDecimal(balance, decimals, conversionRate);
            string format = "";
            if (decimals != null)
            {
                //reduce digits, the larger balance is, i.e. exponent (never reduce digits, if sub zero)
                int formatDecimals = Math.Max(decimals.Value - Math.Max((int)Math.Log10(Math.Abs((double)floatBalance)), 0), 0);
                format = $"#,###0.{new string('#', formatDecimals)}";
            }
            return floatBalance.ToString(format, Culture);
        }

        private decimal ToDecimalWithExponent(decimal amount, out string exponentPrefix)
        {
            exponentPrefix = "";
            if (amount == 0) return amount;

            //get the exponent - sign doesn't matter, i.e. 6 for 1,000,000 aka 1E6
            var exponent = Math.Log10((double)Math.Abs(amount));

            //we since we're only interested in k, M and B, keep it simple
            if (exponent >= 9)
            {
                exponentPrefix = "B";
                return amount / (decimal)1E9;
            }
            else if (exponent >= 6)
            {
                exponentPrefix = "M";
                return amount / (decimal)1E6;
            }
            else if (exponent >= 3)
            {
                exponentPrefix = "k";
                return amount / (decimal)1E3;
            }
            else
                return amount;
        }

        private string ToStringWithExponent(double num, int decimals, decimal conversionRate, string format = "N3")
        {
            string expPrefix = "";
            decimal amount = ToDecimalWithExponent(ToDecimal(num, decimals, conversionRate), out expPrefix);
            return amount.ToString(format, Culture) + expPrefix;
        }

    }


}
