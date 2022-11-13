using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWorkshop.Data.Entities;
using System.Collections.Generic;

namespace MVCWorkshop.Models.ViewModels
{
    public class HomeVM
    {
        public IReadOnlyList<News> News { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }
        public int selectedCategory { get; set; }

    }
}
