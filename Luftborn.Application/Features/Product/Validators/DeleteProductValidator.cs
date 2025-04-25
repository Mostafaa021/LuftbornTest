using FluentValidation;
using Luftborn.Application.Features.Product.Command;

namespace Luftborn.Application.Features.Product.Validators;

public class DeleteProductCommandValidator : AbstractValidator<DeleteExisitingProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.id)
            .GreaterThan(0).WithMessage("A valid product ID must be provided.");
    }
}