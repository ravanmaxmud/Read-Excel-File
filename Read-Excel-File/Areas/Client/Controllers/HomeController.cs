using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Areas.Client.ViewCompanents;
using Read_Excel_File.Areas.Client.ViewModels.Home;
using ReadExcel.Database;

namespace Read_Excel_File.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> Index(int? categoryId = null)
        {
            ViewBag.CategoryId = categoryId;

            var model = new IndexViewModel
            {
                Categories = await _dataContext.Categories.Select(c => new CategoryViewModel(c.Id, c.Name
                )).ToListAsync()
            };
            return View(model);
        }

        [HttpGet("sort", Name = "client-home-sort")]
        public async Task<IActionResult> Sort([FromQuery] int? categoryId = null)
        {
            return ViewComponent(nameof(SortModel), new { categoryId = categoryId});
        }
    }
}
