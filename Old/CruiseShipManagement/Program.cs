using CruiseShipManagement.DataAccess;
using CruiseShipManagement;
using System.Security.Policy;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                DbInitializer.Seed(services);
            }
            catch (Exception ex)
            {
                // Log errors
                Console.WriteLine($"Error seeding database: {ex.Message}");
            }
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
