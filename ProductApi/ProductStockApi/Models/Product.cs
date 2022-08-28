using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStockApi.Models
{
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("product_name")]
        public string ProductName { get; set; }
        [Column("product_categoryId")]
        public int ProductCategoryId { get; set; }
        [Column("product_price")]
        public double Price { get; set; }
        [Column("product_createdDate")]
        public DateTime CreatedDate { get; set; }
        [Column("product_state")]
        public string State { get; set; }
        [Column("product_isDeleted")]
        public bool IsDeleted { get; set; }        
    }
}
