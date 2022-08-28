using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductApi.Models
{
    public class ProductCategory
    {
        [Key]
        [Column("category_Id")]
        public int ProductCategoryId { get; set; }
        [Column("category_name")]
        public string ProductCategoryName { get; set; }
        [JsonIgnore]
        public IEnumerable<Product>? Products { get; set; }
    }
}
