using Microsoft.EntityFrameworkCore;
using MVCWorkshop.Data.Entities;
using System.Reflection;

namespace MVCWorkshop.DbContexts
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {
        }


        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<News>  News{ get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


    }
}
