using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShoppingCart.Core.IRepository;
using OnlineShoppingCart.Data;

namespace OnlineShoppingCart.Core.Repository
{
    public class ShippingRepository : GenericRepository<Shipping>, IShippingRepository
    {
        public ShippingRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}