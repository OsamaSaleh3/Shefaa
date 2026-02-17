using FluentValidation;

namespace Shefaa.Application.Invoices.Commands.RemoveInvoiceItem;

public class RemoveInvoiceItemCommandValidator : AbstractValidator<RemoveInvoiceItemCommand>
{
    public RemoveInvoiceItemCommandValidator()
    {
        RuleFor(x => x.InvoiceId)
            .NotEmpty();

        RuleFor(x => x.ItemId)
            .NotEmpty()
            .WithMessage("Item ID is required to remove an item.");
    }
}