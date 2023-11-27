using CrealutionRealtimeServer.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using CrealutionRealtimeServer.Configurations.Mapping;
using CrealutionRealtimeServer.WebApi.Hubs;

namespace CrealutionRealtimeServer.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSignalR();
            builder.Services.AddCors(builder => builder.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:8080", "https://localhost:8080")
                    .WithMethods("GET", "POST")
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAutoMapper(typeof(CrealutionMappingProfile));
            builder.Services.AddDbContext<CrealutionDb>(options => options.UseInMemoryDatabase("CrealutionDb"));

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration.GetSection("ElsasticSearch")["Url"]))
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();

            builder.Services.AddSingleton(Log.Logger);
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });

            var app = builder.Build();

            app.UseRouting();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AuthorizeHub>("/authorizeHub");
            });
            app.Run();
        }
    }
}