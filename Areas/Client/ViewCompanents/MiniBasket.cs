using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Areas.Client.ViewModels.Basket;
using Read_Excel_File.Database;
using Read_Excel_File.Services.Abstracts;
using System.Text.Json;

namespace Read_Excel_File.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "MiniBasket")]
    public class MiniBasket : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;


        public MiniBasket(DataContext dataContext, IUserService userService = null)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<BasketViewModel>? viewModels = null)
        {

            var model = await _dataContext.BasketProducts.Where(p => p.Basket.UserId == _userService.CurrentUser.Id)
                .Select(p =>
                new BasketViewModel(p.ModelName, p.ModelQuantity)).ToListAsync();

            return View(model);



        }
    }
}
