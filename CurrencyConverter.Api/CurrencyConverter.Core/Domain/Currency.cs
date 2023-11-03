using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CurrencyConverter.Core.Domain
{
    public class Currency
    {
        private const char GroupSeperator = ' ';
        private const char DecimalSeparator = ',';

        public List<int> PrecisionGroups { get; }
        public int Fraction { get; } = 0;

        public bool IsPrecisionSingular
        {
            get
            {
                return PrecisionGroups.Sum() == 1;
            }
        }

        public bool IsFractionSingular
        {
            get
            {
                return Fraction == 1;
            }
        }

        public Currency(string formattedCurrencyText)
        {
            Validate(formattedCurrencyText);

            var splittedMoneyParts = formattedCurrencyText.Split(DecimalSeparator);
            if (splittedMoneyParts.Length > 1)
            {
                Fraction = int.Parse(splittedMoneyParts[1]);
                Fraction = splittedMoneyParts[1].Length < 2 ? Fraction * 10 : Fraction;
            }

            PrecisionGroups = splittedMoneyParts[0].Split(GroupSeperator).Select(int.Parse).ToList();
        }

        private void Validate(string formattedCurrencyText)
        {
            Regex regex = new Regex("^(0|[1-9][0-9]{0,2})(\\s\\d{3})*(\\,\\d{1,2})?$");

            if (!regex.IsMatch(formattedCurrencyText))
            {
                throw new ArgumentException("Given currency is not valid");
            }
        }
    }
}
