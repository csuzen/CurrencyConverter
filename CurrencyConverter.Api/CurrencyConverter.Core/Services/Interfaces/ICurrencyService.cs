using CurrencyConverter.Core.Domain;

namespace CurrencyConverter.Core.Services.Interfaces
{
    public interface ICurrencyService
    {
        string GetWord(Currency currency);
    }
}
