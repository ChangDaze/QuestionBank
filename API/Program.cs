
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
            //�o����ΡA�]���o�]�O�]�@�Ӭ۹�ɶ����T�w�ɶ��I(�]���N����)�A�ҥHcache�L���ɶ��n�u���٬O�n��W�]
            //builder.Services.AddMemoryCache(options => new MemoryCacheEntryOptions()
            //    .SetAbsoluteExpiration(TimeSpan.FromSeconds(40))
            //);
            builder.Services.AddMemoryCache();
            ////�ҥ�CORS
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
            
            // �b�����{�Ǥ�����B�z�ҥ~
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
            //); //UseCors ���I�s������b UseRouting ����(�S���g�XUseRouting�n���S�t)�BUseAuthorization ���e
            //   //��swagger+get�|�ݤ��Xheader�A�i��n��test - cors.org���������դ����K
            app.UseAuthorization();

            app.MapControllers();
            
            app.Run();
        }
    }
}
