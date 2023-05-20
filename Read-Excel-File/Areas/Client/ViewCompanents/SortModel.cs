using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Areas.Client.ViewModels.Models;
using ReadExcel.Database;

namespace Read_Excel_File.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "SortModel")]
    public class SortModel : ViewComponent
    {
        private readonly DataContext _dataContext;

        public SortModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync([FromQuery] int? categoryId = null)
        {
            var modelQuery = _dataContext.Models.AsQueryable();
            if (categoryId is not null) 
            {
                modelQuery = modelQuery
                        .Where(p => p.CategoryId == categoryId);
            }
            else
            {
                modelQuery = modelQuery.OrderBy(p => p.Name);
            }
            var model = await modelQuery.Select(m=> new ModelListItemViewModel(m.Id,m.Name)).ToListAsync();

            return View(model);
        }
    }

}
