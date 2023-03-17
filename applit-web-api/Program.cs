using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

    public class Program
    {
        
        public static void Main(string[] args)
        {
                        
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen();

            var app = builder.Build();
            
            // app.UseHttpsRedirection();

            // app.UseAuthorization();

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
 

