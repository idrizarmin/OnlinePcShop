using PCWebShop.Core.Infrastructure;
using PCWebShop.Helper;
using PCWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PCWebShop.Core.Interfaces
{
    public  interface IOglasService
    {
      Task<Message> CreateOglasAsMessageAsync(OglasAddVM oglasAdd, CancellationToken cancellationToken);
      Task<Message> GetAlOglasiAsync(SearchObject searchObject, CancellationToken cancellationToken);
    }
}
