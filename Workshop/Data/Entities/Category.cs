using System.Collections.Generic;

namespace MVCWorkshop.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<News> News{ get; set; }
    }
}
