
using Microsoft.AspNetCore.Mvc;
using QuestionBank.Interfaces;
using QuestionBank.Middlewares;
using QuestionBank.Repositories;
using QuestionBank.Services;

namespace QuestionBank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //IDGeneraterService.SetQuestionID("Server=localhost;Database=question_bank;User Id=postgres;Password=postgres");
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => new BadRequestObjectResult(new { Message = "Model binding occurs problem." });
            });
            //Dapper
            builder.Services.AddScoped<IQuestionBankRepository, PgQuestionBankRepository>();
            builder.Services.AddScoped<IQuestionBankService, QuestionBankService>();
            //General Service
            builder.Services.AddScoped<GeneralService>();
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

            app.UseAuthorization();


            app.MapControllers();
            
            app.Run();
        }
    }
}
