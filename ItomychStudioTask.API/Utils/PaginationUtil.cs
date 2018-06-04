using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItomychStudioTask.API.Utils
{
    public static class PaginationUtil
    {
        public static bool ValidatePagination(int page, int rows)
        {
            if (page >0   && rows> 0   ) return true;
            return false;
        }

        public static int GetNormalizedPage(int page)
        {
            return page - 1;
        }
    }
}
