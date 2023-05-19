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



        [HttpPost]
        public IActionResult ImportExcel(IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length <= 0)
            {
                ModelState.AddModelError("", "Lütfen bir Excel dosyası seçin.");
                return Ok("Sehflik Var");
            }

            return Ok(excelFile);
        }
    }
}
