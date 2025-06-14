using Askii.backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Askii.backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


            var app = builder.Build();

            app.UseCors("AllowReactApp");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // app.MapGet("/weatherforecast", () =>
            // {
            //     var summaries = new[]
            //     {
            //         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            //     };

            //     var forecast = Enumerable.Range(1, 5).Select(index =>
            //         new WeatherForecast
            //         (
            //             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //             Random.Shared.Next(-20, 55),
            //             summaries[Random.Shared.Next(summaries.Length)]
            //         ))
            //         .ToArray();
            //     return forecast;
            // })
            // .WithName("GetWeatherForecast")
            // .WithOpenApi();

            app.MapControllers();


            //seed database 

            // Seeding database
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
                DataSeeder.Seed(dbContext);
            }
            
            app.Run();
        }
    }
}
