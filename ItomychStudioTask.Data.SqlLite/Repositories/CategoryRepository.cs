using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Abstractions;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ItomychStudioTask.Data.SqlLite.Repositories
{
    class CategoryRepository : ICategoryRepository
    {
        private StorageContext storageContext;
        private DbSet<Category> categorySet;

        public CategoryRepository(IStorageContext storageContext)
        {
            this.storageContext = storageContext as StorageContext;
            this.categorySet = this.storageContext.Set<Category>();
        }

        public async Task<IEnumerable<Category>> GetAll(int page, int rows)
        {
           return  await  categorySet.Skip(page*rows).Skip(rows).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await categorySet.ToListAsync();
        }

        public async Task<Category> Get(long id)
        {

            return await categorySet.FirstOrDefaultAsync(category => category.Id == id );
        }
    }
}
