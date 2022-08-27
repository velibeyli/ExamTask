using Microsoft.EntityFrameworkCore;
using ProductStockApi.Models;

namespace ProductStockApi.Db
{
    public class ProductStockContext : DbContext
    {
        public ProductStockContext(DbContextOptions<ProductStockContext> options) : base(options) { }
        public DbSet<ProductStock> ProductStocks { get; set; }
    }
}
