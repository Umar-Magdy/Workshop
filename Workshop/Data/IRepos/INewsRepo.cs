using MVCWorkshop.Data.Entities;
using MVCWorkshop.Data.IRepos.Base;

namespace MVCWorkshop.Data.IRepos
{
    public interface INewsRepo : IBaseRepo<News,int>
    {
    }
}
