using FluentValidation;
using Luftborn.Application.Features.Product.Command;

namespace Luftborn.Application.Features.Product.Validators;

public class UpdateProductCommandValidator : AbstractValidator<UpdateExisitingProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("A valid product ID must be provided.");

        RuleFor(x => x.ProductDto)
            .NotNull().WithMessage("Product data must be provided.");

        When(x => x.ProductDto != null, () =>
        {
            RuleFor(x => x.ProductDto.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.ProductDto.ProductType)
                .NotEmpty().WithMessage("Product type is required.")
                .Must(pt => pt is "regular" or "promotional")
                .WithMessage("Product type must be 'regular' or 'promotional'.");
        });
    }
}