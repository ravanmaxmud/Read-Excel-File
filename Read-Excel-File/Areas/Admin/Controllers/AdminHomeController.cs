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
                ModelState.AddModelError("", "Lütfen bir Excel dosyası seçin.");
                return Ok("Sehflik Var");
            }

            using (var package = new ExcelPackage(excelFile.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                List<Category> categories = new List<Category>();

                //var categoryNameSet = new HashSet<string>(); // Kategori adlarını takip

                Dictionary<string, Category> kategoriDict = new Dictionary<string, Category>(); // Kategori adı ve ilgili Kategori nesnesini takip etmek için bir sözlük oluşturulur

                // Verileri oku ve Kategori ve Model nesnelerine dönüştür
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    string categoryName = worksheet.Cells[row, 1].Value.ToString();
                    string modelName = worksheet.Cells[row, 2].Value.ToString();
                    int modelCount = Convert.ToInt32(worksheet.Cells[row, 3].Value);


                    Category existingCategory = _dataContext.Categories.FirstOrDefault(k => k.Name == categoryName);
                    if (existingCategory == null)
                    {
                        // Kategori veritabanında yoksa yeni bir kategori oluştur
                        existingCategory = new Category { Name = categoryName };
                        _dataContext.Categories.Add(existingCategory);
                    }

                    Model existingModel = existingCategory.Models.FirstOrDefault(m => m.Name == modelName);

                    if (existingModel == null)
                    {
                        // Model veritabanında yoksa yeni bir model oluştur
                        existingModel = new Model { Name = modelName, ModelCount = modelCount };
                        existingCategory.Models.Add(existingModel);
                    }
                    else
                    {
                        // Model veritabanında varsa, count değeri güncellenir
                        existingModel.ModelCount += modelCount;
                    }


                    // Kategori ve modelleri sözlüğe ekle
                    if (!kategoriDict.ContainsKey(categoryName))
                    {
                        kategoriDict.Add(categoryName, existingCategory);
                    }
                    


                    //if (categoryNameSet.Contains(categoryName))
                    //{
                    //    // Eğer kategori adı zaten HashSet'te bulunuyorsa, sadece modeli kaydet
                    //    var existingKategori = _dataContext.Categories.FirstOrDefault(k => k.Name == categoryName);

                    //    if (existingKategori != null)
                    //    {
                    //        var model = new Model { Name = modelName, ModelCount = modelCount, Category = existingKategori };
                    //        existingKategori.Models.Add(model);
                    //    }
                    //}
                    //else
                    //{
                    //    // Yeni kategori yarat ve modeli kaydet
                    //    var kategori = new Category { Name = categoryName, Models = new List<Model>() };
                    //    var model = new Model { Name = modelName, ModelCount = modelCount, Category = kategori };
                    //    kategori.Models.Add(model);

                    //    _dataContext.Categories.Add(kategori);
                    //    categoryNameSet.Add(categoryName);
                    //}

                    //if (categoryNameList.Contains(categoryName))
                    //{
                    //    // Eğer kategori adı zaten listeye eklenmişse, bu kategoriyi atlayabiliriz
                    //    continue;
                    //}

                    //categoryNameList.Add(categoryName); // Kategori adını listeye ekler


                    //var allCate = await _dataContext.Categories.ToListAsync();

                    //if (!kategoriDict.ContainsKey(categoryName))
                    //{

                    //    Category exsistingCate = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
                    //    if (categoryName is null)
                    //    {
                    //        Category kategori = new Category { Name = categoryName, Models = new List<Model>() };
                    //        kategoriDict.Add(categoryName, kategori);

                    //    }
                    //    else
                    //    {
                    //        kategoriDict.Add(categoryName, exsistingCate);
                    //    }

                    //}

                    ////Model model = new Model { Name = modelName, ModelCount = modelCount, Category = kategoriDict[categoryName] };
                    ////kategoriDict[categoryName].Models.Add(model);


                    //Model existingModel = kategoriDict[categoryName].Models.FirstOrDefault(m => m.Name == modelName);
                    //if (existingModel == null)
                    //{
                    //    Model model = new Model { Name = modelName, ModelCount = modelCount, Category = kategoriDict[categoryName] };
                    //    kategoriDict[categoryName].Models.Add(model);
                    //}
                    //else
                    //{
                    //    existingModel.ModelCount += modelCount;
                    //}




                    // Kategori var mı kontrol et
                    //var category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
                    //if (category is null)
                    //{
                    //    category = new Category
                    //    {
                    //        Name = categoryName,
                    //        Models = new List<Model>()
                    //    };
                    //    await _dataContext.Categories.AddAsync(category);
                    //}

                    //var exsistingModel = category.Models.FirstOrDefault(m=> m.Name == modelName);

                    //if (exsistingModel is null)
                    //{
                    //    var model = new Model
                    //    {
                    //        Name = modelName,
                    //        ModelCount = modelCount,
                    //        Category = category
                    //    };
                    //    category.Models.Add(model);
                    //}
                    //else
                    //{
                    //    exsistingModel.ModelCount += modelCount;
                    //}


                }

                foreach (Category kategori in kategoriDict.Values)
                {
                    // Kategori ve Modelleri veritabanına eklenir

                    foreach (Model model in kategori.Models)
                    {
                        Model existingModel = _dataContext.Models.FirstOrDefault(m => m.CategoryId == kategori.Id && m.Name == model.Name);

                        if (existingModel != null)
                        {
                            existingModel.ModelCount += model.ModelCount;
                        }
                    }

                    _dataContext.Categories.Add(kategori);
                }
                await _dataContext.SaveChangesAsync();




            }

            return Ok($"{excelFile}");
        }
    }
}
