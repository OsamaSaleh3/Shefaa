using ErrorOr;
using MediatR;
using Shefaa.Domain.Invoices.enums;

namespace Shefaa.Application.Invoices.Commands.RecordPayment;

public record RecordPaymentCommand(
    Guid InvoiceId,
    decimal Amount,
    PaymentMethod PaymentMethod
) : IRequest<ErrorOr<Success>>;