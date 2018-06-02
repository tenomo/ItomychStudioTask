using System.Collections.Generic;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Data.Abstractions.ModelAbstractions
{
    public interface IBookRepository : IRepository, ICollectionsPagination<Book>
    {
        Task<IEnumerable<Book>> GetAll();
  
         Task<Book> Get(long id);
        Task Create(Book item);
        Task Update(Book item);
        Task Delete(long id);
    }
}
