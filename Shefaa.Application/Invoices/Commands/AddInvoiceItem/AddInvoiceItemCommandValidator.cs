using FluentValidation;

namespace Shefaa.Application.Invoices.Commands.AddInvoiceItem;

public class AddInvoiceItemCommandValidator : AbstractValidator<AddInvoiceItemCommand>
{
    public AddInvoiceItemCommandValidator()
    {
        RuleFor(x => x.InvoiceId)
            .NotEmpty()
            .WithMessage("Invoice ID is required.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Description is required and must not exceed 200 characters.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be at least 1.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0) // نسمح بـ 0 (خدمة مجانية) لكن نمنع السالب
            .WithMessage("Unit price cannot be negative.");
    }
}