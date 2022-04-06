using CurrencyConvertor.Models.Data.MainContext;
using CurrencyConvertor.V1.Repositories;
using CurrencyConvertor.V1.Repository.IRepository;

namespace CurrencyConvertor.V1.Repository
{
    public class ExchangeRateRequestRepository : Repository<ExchangeRateRequest>, IExchangeRateRequestRepository
    {
        public ExchangeRateRequestRepository(CurrencyConvertorDBContext context) : base(context)
        {

        }
    }
}
