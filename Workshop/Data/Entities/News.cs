
using System;

namespace MVCWorkshop.Data.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category{ get; set; }

    }
}
