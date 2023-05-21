using Read_Excel_File.Database.Models;

namespace Read_Excel_File.Services.Abstracts
{
    public interface IUserService
    {
        public User CurrentUser { get; }
        public bool IsAuthenticated { get; }

        Task<bool> CheckPasswordAsync(string? email, string? password);
        Task SignInAsync(int id, string? role = null);
        Task SignInAsync(string? email, string? password, string? role = null);
        Task SignOutAsync();
    }
}
