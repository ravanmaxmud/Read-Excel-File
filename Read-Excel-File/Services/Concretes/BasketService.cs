using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Read_Excel_File.Services.Abstracts;
using Read_Excel_File.Database;
using Read_Excel_File.Database.Models;
using Read_Excel_File.Areas.Client.ViewModels.Basket;

namespace Read_Excel_File.Services.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;



        public BasketService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<List<BasketViewModel>> AddBasketProductAsync(Model model, BasketViewModel basketViewModel)
        {

            await AddToDatabaseAsync();
            return new List<BasketViewModel>();


            async Task AddToDatabaseAsync()
            {
                var basketProduct = await _dataContext.BasketProducts
                     .Include(b => b.Basket)
                     .FirstOrDefaultAsync(bp => bp.Basket.User.Id == _userService.CurrentUser.Id &&
                     bp.ModelName == model.Name);

                if (basketProduct is null)
                {
                    var basket = await _dataContext.Baskets.FirstAsync(p => p.UserId == _userService.CurrentUser.Id);

                    basketProduct = new BasketProduct
                    {
                        ModelQuantity = basketViewModel.Quantity,
                        BasketId = basket.Id,
                        ModelName = model.Name
                    };
                       


                    await _dataContext.BasketProducts.AddAsync(basketProduct);
                }
                else
                {
                    basketProduct.ModelQuantity++;
                }

                model.ModelCount -= basketViewModel.Quantity;

                await _dataContext.SaveChangesAsync();
            }
        }


    }
}
