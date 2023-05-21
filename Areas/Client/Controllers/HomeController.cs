using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Areas.Client.ViewCompanents;
using Read_Excel_File.Areas.Client.ViewModels.Home;
using Read_Excel_File.Contracts.Identity;
using Read_Excel_File.Database;

namespace Read_Excel_File.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    [Authorize(Roles = RoleNames.CLIENT)]

    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet("index", Name = "client-home-index")]
        public async Task<IActionResult> Index(int? categoryId = null)
        {
            ViewBag.CategoryId = categoryId != null
                ? categoryId 
                : (await _dataContext.Categories.FirstOrDefaultAsync())!.Id;

            var model = new ModelIndexViewModel
            {
                Categories = await _dataContext.Categories.Select(c => new CategoryViewModel(c.Id, c.Name
                )).ToListAsync()
            };
            return View(model);
        }

        [HttpGet("sort", Name = "client-home-sort")]
        public async Task<IActionResult> Sort(int? categoryId = null)
        {
            return ViewComponent(nameof(SortModel), new { categoryId = categoryId });
        }
    }
}
