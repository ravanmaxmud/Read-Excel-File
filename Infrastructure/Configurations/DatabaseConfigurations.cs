using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Database;

namespace Read_Excel_File.Infrastructure.Configurations
{
    public static class DatabaseConfigurations
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("RevanPc"));
            });
        }
    }
}
