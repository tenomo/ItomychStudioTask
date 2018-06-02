using System.Collections.Generic;
using ItomychStudioTask.Data.Abstractions;
using ItomychStudioTask.Data.Models;

namespace ItomychStudioTask.Tests.DataMock
{
  public  class StorageContext : IStorageContext
    {
        public List<Book> Books { get; set; }
        public List<Category>  Categories { get; set; }
    }
}
