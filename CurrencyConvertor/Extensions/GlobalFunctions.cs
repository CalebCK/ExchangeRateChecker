using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace CurrencyConvertor.Extensions
{
    public static class GlobalFunctions
    {
        public static string GetModelStateErrors(ModelStateDictionary modelState)
        {
            string error = "";

            foreach (var item in modelState.Values)
            {
                foreach (var err in item.Errors)
                {
                    error = error + $"{err.ErrorMessage};" + Environment.NewLine;
                }
            }

            return error;
        }

        public static string GetIdentityErrors(List<IdentityError> errors)
        {
            string error = "";

            foreach (var err in errors)
            {
                error = error + $"{err.Description};" + Environment.NewLine;
            }

            return error;
        }
    }
}
