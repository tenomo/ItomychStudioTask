using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ItomychStudioTask.Data.Abstractions;
using ItomychStudioTask.Data.Abstractions.ModelAbstractions;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Tests.DataMock.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public void SetStorageContext(IStorageContext storageContext)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAll(Expression<Func<Book, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAll(int page, int rows)
        {
            throw new NotImplementedException();
        }

        public Task<Category> Get(long id)
        {
            throw new NotImplementedException();
        }
    }
}
