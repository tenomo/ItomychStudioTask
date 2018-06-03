using System.Collections.Generic;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Business.Services.Books
{
    public interface IBookService
    {
        Task Create(Book book);
        Task Delete(long id);
        Task<Book> Get(long id);
        Task<IEnumerable<Book>> GetAll();
        Task<IEnumerable<Book>> GetAll(int page, int rows);
        Task Update(Book book);
 

    }
}