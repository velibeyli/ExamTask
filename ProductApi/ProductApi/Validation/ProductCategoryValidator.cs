using FluentValidation;
using ProductApi.Models;

namespace ProductApi.Validation
{
    public class ProductCategoryValidator :AbstractValidator<ProductCategory>
    {
        public ProductCategoryValidator()
        {
            RuleFor(x => x.ProductCategoryName).NotNull().MinimumLength(5);
        }
    }
}
