using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

    public class Program
    {
        
        public static void Main(string[] args)
        {
                        
            var builder = WebApplication.CreateBuilder(args);
            // var providor = builder.Services.BuildServiceProvidor();
            // var configurations = providor.GetRequiredService<IConfiguration>();

            // Add services to the container.

            builder.Services.AddControllers(); 
            // builder.Services.AddCors(options=>{
            //     var frontendURL = configurations.GetValue<string>("fronend_url");
 
            //     options.AddDefaultPolicy(builder =>{
            //         builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
            //     });

            // });
            var app = builder.Build();
            
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors();
            app.MapControllers();

            app.Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<StartupBase>();
                    webBuilder.UseUrls("http://localhost:5054"); // Replace with your desired URL
                });
                
    }
 

