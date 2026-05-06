using Microsoft.EntityFrameworkCore;
using SkillSprint.Data;
using SkillSprint.Services;

namespace SkillSprint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // get the connection string from the appsettings.json
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register the SkillSprintContext with the builder, so I can use when I need it
            builder.Services.AddDbContext<SkillSprintContext>(options => options.UseSqlite(connectionString));

            // add the SQLStorage service
            builder.Services.AddScoped<IChallengeStorage, ChallengesSQLite>();
            builder.Services.AddScoped<IChallengeSource, APIChallenges>();
            // add the httpClient service
            /*builder.Services.AddHttpClient<IChallengeSource, APIChallenges>(options =>
            {
                options.BaseAddress = new Uri("https://the-one-api.dev/v2/");
                options.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                    builder.Configuration["ONEAPIKEY"]);
            });*/

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();

            
            const string AllowLocalHost = "AllowLocalHost";
            builder.Services.AddCors(option =>
            {
                option.AddPolicy(name: AllowLocalHost, policy =>
                {
                    policy.WithOrigins("https://localhost:5173", "http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseCors(AllowLocalHost);
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}