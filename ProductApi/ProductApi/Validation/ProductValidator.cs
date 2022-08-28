using FluentValidation;
using ProductApi.Models;

namespace ProductApi.Validation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotNull().NotEmpty().MinimumLength(5).WithMessage("Name must be at least 5 symbol");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Value must be greater than zero");
            RuleFor(p => p.State).NotNull().WithMessage("Can not be null");
            RuleFor(p => p.CreatedDate).NotNull().NotEmpty().WithMessage("Can not be null");
            RuleFor(p => p.IsDeleted).NotNull().WithMessage("Can not be null");
            RuleFor(p => p.ProductCategoryId).NotNull().WithMessage("Can not be null");
        }
    }
}
