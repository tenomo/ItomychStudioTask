using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ItomychStudioTask.Data.Abstractions
{
    public interface ICollectionsPagination <T> where T : class
    {
        Task<IEnumerable<T>> GetAll(int page, int rows) ;
    }
}
