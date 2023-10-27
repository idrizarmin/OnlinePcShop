using PCWebShop.Core.Infrastructure;
using PCWebShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PCWebShop.Core.Interfaces
{
    public interface INarudzbaService
    {
        Task<Message> AddNarudzba(KorpaVM request, CancellationToken cancellationToken);
    }
}
