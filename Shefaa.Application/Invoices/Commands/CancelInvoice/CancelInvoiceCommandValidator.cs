using FluentValidation;

namespace Shefaa.Application.Invoices.Commands.CancelInvoice;

public class CancelInvoiceCommandValidator : AbstractValidator<CancelInvoiceCommand>
{
    public CancelInvoiceCommandValidator()
    {
        RuleFor(x => x.InvoiceId)
            .NotEmpty()
            .WithMessage("Invoice ID is required.");
    }
}