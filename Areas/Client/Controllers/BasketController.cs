using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Areas.Client.ViewCompanents;
using Read_Excel_File.Areas.Client.ViewModels.Basket;
using Read_Excel_File.Areas.Client.ViewModels.Cookie;
using Read_Excel_File.Database;
using Read_Excel_File.Services.Abstracts;
using Read_Excel_File.Services.Concretes;

namespace Read_Excel_File.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ICookieService _cookieService;
        private readonly IUserService _userService;
        private readonly IJiraService _jiraService;
        public BasketController(DataContext dataContext,
            ICookieService cookieService,
            IUserService userService,
            IJiraService jiraService)
        {
            _dataContext = dataContext;
            _cookieService = cookieService;
            _userService = userService;
            _jiraService = jiraService;
        }

        [HttpGet("add", Name = "client-basket-add")]
        public async Task<IActionResult> AddProduct( CookieViewModel cookeViewModel)
        {
            var product = await _dataContext.Models.FirstOrDefaultAsync(p => p.Name == cookeViewModel.Name);
            if (product is null)
            {
                return NotFound();
            }

            if (product.ModelCount < cookeViewModel.Quantity)
            {
                return NotFound();
            }

            await _jiraService.PostRequestJsonFormatAsync(cookeViewModel.Name, cookeViewModel.Quantity.ToString());
            _cookieService.AddProductCookie(cookeViewModel);
            //if (productCookiViewModel.Any())
            //{
            //    return ViewComponent(nameof(MiniBasket), productCookiViewModel);
            //}
            return ViewComponent(nameof(MiniBasket), product);
        }
    }
}
