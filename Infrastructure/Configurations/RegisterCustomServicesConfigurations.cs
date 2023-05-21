using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Services.Abstracts;
using Read_Excel_File.Services.Concretes;

namespace Read_Excel_File.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
          
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJiraService, JiraService>();
            services.AddScoped<ICookieService,CookieService>();

        }
    }
}
