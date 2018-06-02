using System.Collections.Generic;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Data.Abstractions.ModelAbstractions
{
    public interface ICategoryRepository : IRepository, ICollectionsPagination<Category>
    {
        Task<IEnumerable<Category>> GetAll(); 
        Task<Category> Get(long id); 

    }
}
