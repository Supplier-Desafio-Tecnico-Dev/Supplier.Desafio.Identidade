using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Supplier.Commons.Middlewares
{
    public class ExcecaoMiddleware
    {
        private readonly RequestDelegate _next;

        public ExcecaoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new { sucesso = false, erros = exception.Message };
            var result = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(result);
        }
    }
}
