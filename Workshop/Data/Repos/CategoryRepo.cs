using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWorkshop.Data.Entities;
using MVCWorkshop.Data.IRepos;
using MVCWorkshop.Data.Repos.Base;
using MVCWorkshop.DbContexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWorkshop.Data.Repos
{
    public class CategoryRepo : BaseRepo<Category, int>, ICategoryRepo
    {
        private readonly NewsDbContext _context;

        public CategoryRepo(NewsDbContext context):base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetListItems()
        {
            var ListItems =  _context.Categories.Select(x=> new SelectListItem() {Text=x.Name,Value=x.Id.ToString() }).ToList();
            return ListItems;
        }
    }
}
