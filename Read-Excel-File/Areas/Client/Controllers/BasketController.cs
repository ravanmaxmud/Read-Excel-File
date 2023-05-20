using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Areas.Client.ViewCompanents;
using Read_Excel_File.Areas.Client.ViewModels.Basket;
using Read_Excel_File.Database;
using Read_Excel_File.Services.Abstracts;

namespace Read_Excel_File.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        public BasketController(DataContext dataContext, IBasketService basketService, IUserService userService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
        }

        [HttpGet("add/{modelName}", Name = "client-basket-add")]
        public async Task<IActionResult> AddProduct([FromRoute] string modelName,BasketViewModel basketViewModel)
        {
            var product = await _dataContext.Models.FirstOrDefaultAsync(p=> p.Name == modelName);
            if (product is null)
            {
                return NotFound();
            }

            if (product.ModelCount < basketViewModel.Quantity)
            {
                return Ok("Olmaz");
            }
            var productCookiViewModel = await _basketService.AddBasketProductAsync(product, basketViewModel);
            //if (productCookiViewModel.Any())
            //{
            //    return ViewComponent(nameof(MiniBasket), productCookiViewModel);
            //}
            return ViewComponent(nameof(MiniBasket), product);
        }
    }
}
