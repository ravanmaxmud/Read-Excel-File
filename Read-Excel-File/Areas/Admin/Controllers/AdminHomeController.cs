using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Read_Excel_File.Database.Models;
using ReadExcel.Database;
using Microsoft.EntityFrameworkCore;

namespace Read_Excel_File.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/home")]
    public class AdminHomeController : Controller
    {
        private readonly DataContext _dataContext;

        public AdminHomeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length <= 0)
            {
                ModelState.AddModelError("", "Please select an Excel file.");
                return Ok("Error: No Excel file selected.");
            }

            using (var package = new ExcelPackage(excelFile.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                Dictionary<string, Category> categoryDictionary = new Dictionary<string, Category>();

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    string categoryName = worksheet.Cells[row, 1].Value.ToString();
                    string modelName = worksheet.Cells[row, 2].Value.ToString();
                    int modelCount = Convert.ToInt32(worksheet.Cells[row, 3].Value);

                    if (!categoryDictionary.ContainsKey(categoryName))
                    {
                        var category = await _dataContext.Categories.Include(m=> m.Models).FirstOrDefaultAsync(c => c.Name == categoryName);
                        if (category == null)
                        {
                            category = new Category { Name = categoryName, Models = new List<Model>() };
                            categoryDictionary.Add(categoryName, category);
                        }
                        else
                        {
                            categoryDictionary.Add(categoryName, category);
                        }
                    }

                    Category existingCategory = categoryDictionary[categoryName];
                    Model existingModel = existingCategory.Models.FirstOrDefault(m => m.Name == modelName);
                    if (existingModel == null)
                    {
                        Model model = new Model { Name = modelName, ModelCount = modelCount, Category = existingCategory };
                        existingCategory.Models.Add(model);
                    }
                }

                foreach (Category category in categoryDictionary.Values)
                {
                  
                    var existingCategory = await _dataContext.Categories.Include(m=> m.Models).FirstOrDefaultAsync(c => c.Name == category.Name);
                    if (existingCategory == null)
                    {
                        _dataContext.Categories.Add(category);
                    }
                    else
                    {
                      
                        foreach (Model model in category.Models)
                        {
                            var existingModel = existingCategory.Models.FirstOrDefault(m => m.Name == model.Name);
                            if (existingModel == null)
                            {
                                existingCategory.Models.Add(model);
                            }
                            else
                            {
                                existingModel.ModelCount += model.ModelCount;
                            }
                        }
                    }
                }

                await _dataContext.SaveChangesAsync();
            }

            return Ok("Excel file uploaded and saved to the database successfully.");
        }

    }
}
