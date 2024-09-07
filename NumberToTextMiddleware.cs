using Microsoft.AspNetCore.Http;
using Humanizer;
using System.Text;
using System.Threading.Tasks;

namespace CW_070_09_P
{
    public class NumberToTextMiddleware
    {
        private readonly RequestDelegate next;

        public NumberToTextMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/number", out var remainingPath) &&
                int.TryParse(remainingPath.Value.Trim('/'), out var number))
            {
                if (number >= 1 && number <= 100000)
                {
                    string numberInWords = number.ToWords().Replace("-", " ");
                    string responseMessage = $"Ваше число {numberInWords}";

                    context.Response.ContentType = "text/plain; charset=utf-8";
                    context.Response.StatusCode = 200;

                    await context.Response.WriteAsync(responseMessage, Encoding.UTF8);
                    return;
                }
            }
            await next(context);
        }
    }
}
