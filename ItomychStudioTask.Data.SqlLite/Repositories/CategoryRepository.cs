using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Abstractions;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ItomychStudioTask.Data.SqlLite.Repositories
{
   public class CategoryRepository : ICategoryRepository
    {
        private readonly StorageContext _storageContext;
        private readonly DbSet<Category> _categorySet;

        public CategoryRepository(IStorageContext storageContext)
        {
            this._storageContext = storageContext as StorageContext;
            this._categorySet = this._storageContext.Set<Category>();
        }

        public async Task<IEnumerable<Category>> GetAll(int page, int rows)
        {
           return  await  _categorySet.Skip(page*rows).Take(rows).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categorySet.ToListAsync();
        }

        public async Task<Category> Get(long id)
        {

            return await _categorySet.FirstOrDefaultAsync(category => category.Id == id );
        }
    }
}
