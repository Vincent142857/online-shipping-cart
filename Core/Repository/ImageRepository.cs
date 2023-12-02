using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using OnlineShoppingCart.Core.IRepository;
using OnlineShoppingCart.Data;

namespace OnlineShoppingCart.Core.Repository
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}