using Read_Excel_File.Areas.Client.ViewModels.Cookie;
using Read_Excel_File.Database;
using Read_Excel_File.Services.Abstracts;
using System.Text.Json;

namespace Read_Excel_File.Services.Concretes;

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly DataContext _dbContext;

    public CookieService(IHttpContextAccessor httpContextAccessor, DataContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public  void AddProductCookie(CookieViewModel model)
    {
        var productCookieValue = _httpContextAccessor.HttpContext!.Request.Cookies["products"];

        var productCookieViewModel = productCookieValue is not null
           ? JsonSerializer.Deserialize<List<CookieViewModel>>(productCookieValue)
           : new List<CookieViewModel> { };

        var cookieViewModel =
            productCookieViewModel!.FirstOrDefault(pc => pc.Name == model.Name );

        if (cookieViewModel is null )
        {
            productCookieViewModel!.Add(new CookieViewModel(model.Name, model.Quantity));
        }
        else
        {
            cookieViewModel.Quantity += model.Quantity;
        }

        _httpContextAccessor.HttpContext.Response.Cookies
            .Append("products", JsonSerializer.Serialize(productCookieViewModel));
    }
    public List<CookieViewModel> GetAddProductCookie()
    {
        var productCookieValue = _httpContextAccessor.HttpContext!.Request.Cookies["products"];

        var productCookieViewModel = productCookieValue is not null
           ? JsonSerializer.Deserialize<List<CookieViewModel>>(productCookieValue)
           : new List<CookieViewModel> { };

        return productCookieViewModel!;
    }
}
