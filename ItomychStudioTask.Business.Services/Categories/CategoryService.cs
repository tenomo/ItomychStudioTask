using System.Collections.Generic;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Business.Services.Categories
{
   public class CategoryService : ICategoryService
    {
        private readonly IStorage _storage;
        public CategoryService(IStorage storage)
        {
            _storage = storage;
        }

       public Task<IEnumerable<Category>> GetAll()
        {
           return _storage.CategoryRepository.GetAll();
        }
      public  Task<IEnumerable<Category>> GetAll(int page, int rows)
        {
           return _storage.CategoryRepository.GetAll(page,rows);
        }
    }
}
