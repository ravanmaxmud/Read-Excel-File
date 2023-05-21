namespace Read_Excel_File.Infrastructure.Extensions
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
                 pattern: "{area=exists}/{controller=Authentication}/{action=login}");

            //app.MapHub<AlertHub>("hubs/alert-hub");
            //app.MapHub<ChatHub>("hubs/chat-hub");
        }
    }
}
