
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuestionBank.Middlewares;
using QuestionBank.Repositories;
using QuestionBank.Services;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

namespace QuestionBank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //use Serilog
            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
            // Set start point question id
            IDGeneraterService.SetQuestionID("Server=localhost;Database=question_bank;User Id=postgres;Password=postgres");
            //Add server side cache
            //https://learn.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-8.0
            //https://learn.microsoft.com/zh-tw/dotnet/api/microsoft.extensions.caching.memory.cacheentryextensions?view=net-8.0
            //這不能用，因為這也是設一個相對時間的固定時間點(設完就不變)，所以cache過期時間要彈性還是要單獨設
            //builder.Services.AddMemoryCache(options => new MemoryCacheEntryOptions()
            //    .SetAbsoluteExpiration(TimeSpan.FromSeconds(40))
            //);
            builder.Services.AddMemoryCache();
            ////啟用CORS
            //builder.Services.AddCors();
            //Add controller automatic reply deal
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => new BadRequestObjectResult(new { Message = "Model binding occurs problem." });
            });            
            //Dapper
            builder.Services.AddScoped<IQuestionBankRepository, PgQuestionBankRepository>();
            builder.Services.AddScoped<IQuestionsService, QuestionsService>();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var app = builder.Build();
            
            // 在中介程序中全域處理例外
            app.UseExceptionHandleMiddleware();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //app.UseCors(
            //    builder =>
            //    {
            //        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            //    }
            //); //UseCors 的呼叫必須放在 UseRouting 之後(沒有寫出UseRouting好像沒差)、UseAuthorization 之前
            //   //用swagger+get會看不出header，可能要用test - cors.org之類的測試比較方便
            app.UseAuthorization();

            app.MapControllers();
            
            app.Run();
        }
    }
}
