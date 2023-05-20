using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json.Serialization;
using Read_Excel_File.Infrastructure.Configurations;

namespace Read_Excel_File.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
            {
                o.Cookie.Name = "Identity";
                o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                o.LoginPath = "/authentication/login";
                o.AccessDeniedPath = "/admin/auth/login";
            });

            services.AddHttpContextAccessor();

            //services.AddUrlHelper();

            //services.AddScoped<ValidationCurrentUserAttribute>();

            services.AddSignalR();

            //services.AddHostedService<DeleteExpiredUpUsers>();
            //services.AddHostedService<DeleteIsSeenMessages>();

            services.ConfigureMvc();

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


            services.ConfigureDatabase(configuration);

            services.ConfigureOptions(configuration);

            services.ConfigureFluentValidatios(configuration);

            services.RegisterCustomServices(configuration);
        }
    }
}
