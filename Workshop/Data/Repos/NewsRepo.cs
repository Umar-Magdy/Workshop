using MVCWorkshop.Data.Entities;
using MVCWorkshop.Data.IRepos;
using MVCWorkshop.Data.Repos.Base;
using MVCWorkshop.DbContexts;

namespace MVCWorkshop.Data.Repos
{
    public class NewsRepo : BaseRepo<News,int>,INewsRepo
    {
        public NewsRepo(NewsDbContext context):base(context)
        {

        }
    }
}
