using FluentValidation;
using Luftborn.Application.Features.Product.Command;


namespace Luftborn.Application.Features.Product.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>

{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.ProductDto)
            .NotNull().WithMessage("Product data must be provided.");

        When(x => x.ProductDto != null, () =>
        {
            RuleFor(x => x.ProductDto.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(128).WithMessage("Product name must not exceed 128 characters.");

            RuleFor(x => x.ProductDto.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.ProductDto.Description)
                .NotEmpty().WithMessage("Please Add Description"); 

            RuleFor(x => x.ProductDto.ProductType)
                .NotEmpty().WithMessage("Product type is required.")
                .Must(pt => pt is "regular" or "promotional")
                .WithMessage("Product type must be 'regular' or 'promotional'.");
        });
    }
}