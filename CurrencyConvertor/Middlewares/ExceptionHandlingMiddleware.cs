using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using CurrencyConvertor.Extensions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CurrencyConvertor.Middlewares
{
    /// <summary>
    /// Middleware for intercepting exceptions
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate requestDelegate;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, logger);
            }
        }
        private static Task HandleException(HttpContext context, Exception exception, ILogger<ExceptionHandlingMiddleware> logger)
        {
            logger.LogError(exception.ToString());
            var result = JsonConvert.SerializeObject(new { error = exception.Message });

            var interceptResponse = CustomExceptionLogger.InterceptException(exception, context.Request.Path, new StackTrace(exception), context.User.Identity.Name ?? "user");

            if (exception is CustomException || exception is SecurityTokenException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(interceptResponse.errMessage);
        }
    }
}
