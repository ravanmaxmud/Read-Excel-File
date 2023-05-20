using Microsoft.AspNetCore.Mvc;
using Read_Excel_File.Areas.Client.ViewModels.Basket;
using Read_Excel_File.Database.Models;

namespace Read_Excel_File.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<BasketViewModel>> AddBasketProductAsync(Model model,BasketViewModel basketViewModel);

    }
}
