using FluentValidation;
using ProductStockApi.Models;

namespace ProductStockApi.Validation
{
    public class ProductStockValidator :AbstractValidator<ProductStock>
    {
        public ProductStockValidator()
        {
            RuleFor(x => x.ProductId).NotNull().NotEmpty()
                .WithMessage("ProductId can not be null");
            RuleFor(x => x.NewAddedProductCount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.NewSoldProductCount).GreaterThanOrEqualTo(0);               
        }
    }
}
