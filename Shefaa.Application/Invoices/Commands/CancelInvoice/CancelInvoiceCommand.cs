using ErrorOr;
using MediatR;

namespace Shefaa.Application.Invoices.Commands.CancelInvoice;

public record CancelInvoiceCommand(Guid InvoiceId) : IRequest<ErrorOr<Success>>;