namespace ReadExcel.Infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void ConfigureMiddlewarePipeline(this WebApplication app)
        {
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=exists}/{controller=home}/{action=Index}");

            //app.MapHub<AlertHub>("hubs/alert-hub");
            //app.MapHub<ChatHub>("hubs/chat-hub");
        }
    }
}
