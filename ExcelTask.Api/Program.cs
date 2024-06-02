using ExcelTask.Core.Infrastructure.Extensions;

namespace ExcelTask.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Migation 01 - dotnet ef migrations add InitialCreate -p.\ExcelTask.Core\ -s.\ExcelTask.Api\ -o Infrastructure\Data\Migrations

            var configuration = builder.Configuration;

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.Services.AddApplicationService(configuration);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            scope.DatabaseInitialize();

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
