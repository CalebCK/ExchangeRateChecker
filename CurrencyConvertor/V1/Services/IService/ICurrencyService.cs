using CurrencyConvertor.V1.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConvertor.V1.Services.IService
{
    public interface ICurrencyService
    {
        Task<(bool success, object response)> GetRatesAsync(string symbols, string user);
        public List<Currency> GetCurrencies();
    }
}
