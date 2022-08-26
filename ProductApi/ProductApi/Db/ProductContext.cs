using Microsoft.EntityFrameworkCore;

namespace ProductApi.Db
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
    }
}
