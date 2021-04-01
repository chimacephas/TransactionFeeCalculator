using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TransactionFeeCalculator
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;


            var innerMessage = GetInnermostMessage(ex);
            var error = new APIError
            {
                Id = Guid.NewGuid().ToString(),
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = $"The system encountered an error while processing request /n {innerMessage}"
            };
            _logger.LogError(ex, innerMessage + " {ErrorId}", error.Id);

            var result = JsonSerializer.Serialize(error);
            return context.Response.WriteAsync(result);
        }

        private string GetInnermostMessage(Exception ex)
        {
            if (ex.InnerException != null)
                return GetInnermostMessage(ex);

            return ex.Message;
        }
    }

    public class APIError
    {
        public string Id { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
