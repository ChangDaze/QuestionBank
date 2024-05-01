using QuestionBank.POCOs;
using System.Text.Json;

namespace QuestionBank.Middlewares
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 任務調用
        /// </summary>
        /// <param name="context">HTTP 的上下文</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new QuestionBankResult();
            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
    public static class ExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandleMiddleware>();
        }
    }
}
