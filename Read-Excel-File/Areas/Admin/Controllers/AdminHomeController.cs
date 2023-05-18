using Microsoft.AspNetCore.Mvc;

namespace Read_Excel_File.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/home")]
    public class AdminHomeController : Controller
    {
      
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
