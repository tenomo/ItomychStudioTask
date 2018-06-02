using System.Collections.Generic;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Business.Services.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetAll(int page, int rows);
    }
}