using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using Read_Excel_File.Services.Abstracts;
using Read_Excel_File.Database;
using Read_Excel_File.Database.Models;
using Read_Excel_File.Contracts.Identity;

namespace Read_Excel_File.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _currentUser;

        public UserService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor
                                                    )
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated
        {
            get => _httpContextAccessor!.HttpContext.User.Identity!.IsAuthenticated;
        }
        public User CurrentUser
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }
                var idClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == CustomClaimNames.ID);

                _currentUser = _dataContext.Users.First(u => u.Id == int.Parse(idClaim.Value));

                return _currentUser;
            }
        }

        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            var model = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (model is null)
            {
                return false;
            }

            return true;
        }


        public async Task SignInAsync(int id, string? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.ID,id.ToString())
            };
            if (role is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
        }

        public async Task SignInAsync(string? email, string? password, string? role = null)
        {
            var user = await _dataContext.Users.FirstAsync(u => u.Email == email && u.Password == password);

            if (user is not null )
            {
                await SignInAsync(user.Id, role);
            }
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
