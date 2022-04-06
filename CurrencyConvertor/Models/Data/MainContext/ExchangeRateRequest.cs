using System;
using System.Collections.Generic;

#nullable disable

namespace CurrencyConvertor.Models.Data.MainContext
{
    public partial class ExchangeRateRequest
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string Response { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
