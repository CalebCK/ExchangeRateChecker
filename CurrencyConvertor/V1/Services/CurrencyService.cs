using CurrencyConvertor.Models.Data.MainContext;
using CurrencyConvertor.V1.DataTransferObjects;
using CurrencyConvertor.V1.Repository.IRepository;
using CurrencyConvertor.V1.Services.IService;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UCMSAPI.Constants;

namespace CurrencyConvertor.V1.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IExchangeRateRequestRepository _rateRequestRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CurrencyService(IExchangeRateRequestRepository rateRequestRepository, IWebHostEnvironment hostingEnvironment)
        {
            _rateRequestRepository = rateRequestRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<(bool success, object response)> GetRatesAsync(string symbols, string user)
        {
            RestClient restClient = new RestClient(ExchangeApiConstants.BaseUrl);

            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("access_key", ExchangeApiConstants.ApiKey);
            request.AddParameter("symbols", symbols);

            IRestResponse restResponse = await restClient.ExecuteAsync(request);

            if (restResponse.IsSuccessful)
            {
                CurrencyRateResponseDto contentData = JsonConvert.DeserializeObject<CurrencyRateResponseDto>(restResponse.Content);

                LogRequest(contentData, user);

                return (true, contentData);
            }

            CurrencyErrorResponse errorData = JsonConvert.DeserializeObject<CurrencyErrorResponse>(restResponse.Content);

            LogErrorRequest(errorData, user);

            return (false, errorData);
        }

        private void LogRequest(CurrencyRateResponseDto dto, string user)
        {
            ExchangeRateRequest model = new ExchangeRateRequest
            {
                RequestDate = DateTime.Now,
                IsSuccess = dto.Success,
                Response = JsonConvert.SerializeObject(dto),
                Created = DateTime.Now,
                CreatedBy = user
            };

            _rateRequestRepository.Create(model);
            _rateRequestRepository.Save();
        }

        private void LogErrorRequest(CurrencyErrorResponse dto, string user)
        {
            ExchangeRateRequest model = new ExchangeRateRequest
            {
                RequestDate = DateTime.Now,
                IsSuccess = false,
                Response = dto.ToString(),
                Created = DateTime.Now,
                CreatedBy = user
            };

            _rateRequestRepository.Create(model);
            _rateRequestRepository.Save();
        }

        public List<Currency> GetCurrencies()
        {
            string webrootPath = _hostingEnvironment.WebRootPath;

            var comboPath = Path.Combine(webrootPath, "Currencies.json");

            List<Currency> items = new();

            if (File.Exists(comboPath))
            {
                using (StreamReader r = new StreamReader(comboPath))
                {
                    string json = r.ReadToEnd();
                    var dictionary = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);

                    foreach (var item in dictionary)
                    {
                        items.Add(new Currency { Code = item.Key, Name = item.Value });
                    }
                }
            }

            return items;
        }
    }
}
