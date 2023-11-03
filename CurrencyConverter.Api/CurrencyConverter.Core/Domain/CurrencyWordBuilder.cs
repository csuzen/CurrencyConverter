using System.Text;

namespace CurrencyConverter.Core.Domain
{
    public class CurrencyWordBuilder
    {
        private readonly Currency _currency;
        private readonly CurrencyWord _currencyWord;

        private CurrencyWordBuilder(Currency currency, CurrencyWord currencyWord)
        {
            _currency = currency;
            _currencyWord = currencyWord;
        }

        public static CurrencyWordBuilder Create(Currency currency, CurrencyWord currencyWord)
        {
            return new CurrencyWordBuilder(currency, currencyWord);
        }

        public string Build()
        {
            StringBuilder sb = new();

            sb = BuildPrecision(sb);

            if (_currency.Fraction > 0)
            {
                sb.Append($" {_currencyWord.Conjunction} ");

                sb = BuildFraction(sb);
            }

            return sb.ToString();
        }

        private StringBuilder BuildPrecision(StringBuilder sb)
        {
            int j = _currency.PrecisionGroups.Count - 1;

            for (int i = 0; i < _currency.PrecisionGroups.Count; i++, j--)
            {
                sb.Append(ConvertGroupToText(_currency.PrecisionGroups[i]));
                sb.Append(' ');

                if (j > 0)
                {
                    sb.Append(_currencyWord.GetUnitWord((int)Math.Pow(10, j * 3)));
                    sb.Append(' ');
                }
            }

            if (_currency.IsPrecisionSingular)
            {
                sb.Append(_currencyWord.CurrencyNameSingular);
            }
            else
            {
                sb.Append(_currencyWord.CurrencyNamePlural);
            }

            return sb;
        }

        private StringBuilder BuildFraction(StringBuilder sb)
        {
            sb.Append(ConvertGroupToText(_currency.Fraction));

            sb.Append(' ');

            if (_currency.IsFractionSingular)
            {
                sb.Append(_currencyWord.CurrencyFractionNameSingular);
            }
            else
            {
                sb.Append(_currencyWord.CurrencyFractionNamePlural);
            }

            return sb;
        }

        private string ConvertGroupToText(int moneyPart)
        {
            if (moneyPart == 0)
            {
                return _currencyWord.GetUnitWord(0);
            }

            StringBuilder sb = new();

            while (moneyPart > 0)
            {
                if (moneyPart >= 100)
                {
                    var hundredPlace = moneyPart / 100;

                    sb.Append($"{_currencyWord.GetUnitWord(hundredPlace)} {_currencyWord.GetUnitWord(100)} ");

                    moneyPart -= (100 * hundredPlace);

                    continue;
                }

                if (moneyPart < 20)
                {
                    sb.Append(_currencyWord.GetUnitWord(moneyPart));

                    break;
                }

                var tenPlace = (moneyPart / 10) * 10;

                sb.Append($"{_currencyWord.GetUnitWord(tenPlace)}");

                moneyPart -= tenPlace;

                if (moneyPart > 0)
                {
                    sb.Append('-');
                }
            }

            return sb.ToString().Trim();
        }
    }
}
