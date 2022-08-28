using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStockApi.Models
{
    public class ProductStock
    {
        [Key]
        [Column("productStock_id")]
        public int Id { get; set; }
        [Column("productStock_productId")]
        public int ProductId { get; set; }
        [Column("productStock_newAddedProductCount")]
        public int NewAddedProductCount { get; set; }
        [Column("productStock_newSoldProductCount")]
        public int NewSoldProductCount { get; set; }
        [Column("productStock_stockCount")]
        public int StockCount { get; set; }
    }
}
