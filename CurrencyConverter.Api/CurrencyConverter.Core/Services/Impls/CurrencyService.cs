using CurrencyConverter.Core.Domain;
using CurrencyConverter.Core.Services.Interfaces;

namespace CurrencyConverter.Core.Services.Impls
{
    public class CurrencyService : ICurrencyService
    {
        public string GetWord(Currency currency)
        {
            return CurrencyWordBuilder.Create(currency, new CurrencyWord()).Build();
        }
    }
}
