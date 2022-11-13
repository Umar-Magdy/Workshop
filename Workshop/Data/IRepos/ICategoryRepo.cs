using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWorkshop.Data.Entities;
using MVCWorkshop.Data.IRepos.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCWorkshop.Data.IRepos
{
    public interface ICategoryRepo : IBaseRepo<Category,int>
    {
        IEnumerable<SelectListItem> GetListItems();
    }
}
