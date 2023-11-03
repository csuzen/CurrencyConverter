using CurrencyConverter.Core.Domain;

namespace CurrencyConverter.Core.Test
{
    public class CurrencyWordBuilderTest
    {
        [Theory]
        [InlineData("0", "zero dollars")]
        [InlineData("1", "one dollar")]
        [InlineData("25,1", "twenty-five dollars and ten cents")]
        [InlineData("0,01", "zero dollars and one cent")]
        [InlineData("4,4", "four dollars and forty cents")]
        [InlineData("45 100", "forty-five thousand one hundred dollars")]
        [InlineData("83 059", "eighty-three thousand fifty-nine dollars")]
        [InlineData("451 102,2", "four hundred fifty-one thousand one hundred two dollars and twenty cents")]
        [InlineData("999 999 999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        [InlineData("456 123 002,99", "four hundred fifty-six million one hundred twenty-three thousand two dollars and ninety-nine cents")]
        public void Build(string money, string expected)
        {
            var currency = new Currency(money);

            var currencyBuilder = CurrencyWordBuilder.Create(currency, new CurrencyWord());

            var result = currencyBuilder.Build();

            Assert.Equal(expected, result);
        }
    }
}