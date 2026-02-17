using FluentValidation;

namespace Shefaa.Application.Invoices.Commands.RecordPayment;

public class RecordPaymentCommandValidator : AbstractValidator<RecordPaymentCommand>
{
    public RecordPaymentCommandValidator()
    {
        RuleFor(x => x.InvoiceId)
            .NotEmpty();

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Payment amount must be greater than zero.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum()
            .WithMessage("Invalid payment method.");
    }
}