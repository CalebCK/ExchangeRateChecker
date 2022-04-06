using System;
using System.Collections.Generic;

namespace CurrencyConvertor.V1.DataTransferObjects
{
    public class CurrencyRateResponseDto
    {
        public bool Success { get; set; }
        public long TimeStamp { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public IDictionary<string, string> Rates { get; set; }
    }

    public class CurrencyErrorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
