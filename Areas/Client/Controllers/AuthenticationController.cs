using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Areas.Client.ViewModels.Authentication;
using Read_Excel_File.Contracts.Identity;
using Read_Excel_File.Database;
using Read_Excel_File.Services.Abstracts;

namespace Read_Excel_File.Areas.Client.Controllers
{
    [Area("client")]
    [Route("Authentication")]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
       
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(DataContext dbContext, IUserService userService, ILogger<AuthenticationController> logger)
        {
            _dbContext = dbContext;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("~/", Name = "client-Authentication-login")]
        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            if (_userService.IsAuthenticated)
            {
                return RedirectToRoute("client-home-index");
            }

            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "client-auth-login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password is not correct");
                _logger.LogWarning($"({model.Email}{model.Password}) This Email and Password  is not correct.");
                return View(model);
            }
            if (await _dbContext.Users.AnyAsync(u => u.Email == model.Email && u.Role.Name == RoleNames.ADMIN))
            {
                await _userService.SignInAsync(model!.Email, model!.Password, RoleNames.ADMIN);
                return RedirectToRoute("admin-home-index");
            }

            await _userService.SignInAsync(model!.Email, model!.Password, RoleNames.CLIENT);

            return RedirectToRoute("client-home-index");
        }

        [HttpGet("logout", Name = "client-auth-logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("client-Authentication-login");
        }

    }
}
