using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCWorkshop.Models.ViewModels
{
    public class CreateNewsVM
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public IFormFile? Image { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; }

    }

    public class EditNewsVM : CreateNewsVM
    {
        public int Id { get; set; }
    }
}
