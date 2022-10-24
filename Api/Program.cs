using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            //Access services allows us to create a scope, two services context and Logger
            //Using keyword when method is no longer in scope will dispose of resources not being used
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StoreContext>();

            //Log to terminal any errors
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                //Will create database if it doesn't exist
                context.Database.Migrate();
                //Add prodects in Dbinitializer class
                Dbinitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while running the application.");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
