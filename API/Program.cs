
using Microsoft.AspNetCore.Mvc;
using QuestionBank.Interfaces;
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
            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
            // Add services to the container.
            IDGeneraterService.SetQuestionID("Server=localhost;Database=question_bank;User Id=postgres;Password=postgres");
            //Add server cache
            builder.Services.AddMemoryCache();
            //�ҥ�CORS
            builder.Services.AddCors();
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
            app.UseCors(
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                }
            ); //UseCors ���I�s������b UseRouting ����(�S���g�XUseRouting�n���S�t)�BUseAuthorization ���e
               //��swagger+get�|�ݤ��Xheader�A�i��n��test - cors.org���������դ����K
            app.UseAuthorization();


            app.MapControllers();
            
            app.Run();
        }
    }
}
