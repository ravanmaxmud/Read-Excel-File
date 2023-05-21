using FluentValidation.AspNetCore;
using FluentValidation;

namespace Read_Excel_File.Infrastructure.Configurations
{
    public static class FluentValidationConfigurations
    {
        public static void ConfigureFluentValidatios(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Program>();
        }
    }
}
