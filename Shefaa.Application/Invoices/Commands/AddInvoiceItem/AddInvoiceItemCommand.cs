using ErrorOr;
using MediatR;

namespace Shefaa.Application.Invoices.Commands.AddInvoiceItem;

public record AddInvoiceItemCommand(
    Guid InvoiceId,
    string Description,
    int Quantity,
    decimal UnitPrice
) : IRequest<ErrorOr<Success>>;