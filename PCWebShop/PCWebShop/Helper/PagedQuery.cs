using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.Helper
{
    public class PagedQuery
    {
       
            public PagedQuery()
            {
                PageSize = 20;
            }

            public int PageSize { get; set; }
            public int Page { get; set; }

            public int Skip()
            {
                EnsureValidData();
                return (Page - 1) * PageSize;
            }

            public int CalculatePages(int totalItems)
            {
                EnsureValidData();
                return (totalItems - 1) / PageSize + 1;
            }

            protected void EnsureValidData()
            {
                if (PageSize <= 0)
                    PageSize = 2;

                if (PageSize > 100 && PageSize < 100000)
                    PageSize = 100;

                if (Page <= 0)
                    Page = 1;

                if (Page > 100000)
                    Page = 10000000;
            }
        }
    
}
