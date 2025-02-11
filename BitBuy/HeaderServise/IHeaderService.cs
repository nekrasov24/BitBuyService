using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitBuy.HeaderServise
{
    public interface IHeaderService
    {
        Guid GetUserId();
    }
}
