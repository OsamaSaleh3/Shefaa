using ErrorOr;
using MediatR;

namespace Shefaa.Application.Invoices.Commands.RemoveInvoiceItem;

public record RemoveInvoiceItemCommand(
    Guid InvoiceId,
    Guid ItemId
) : IRequest<ErrorOr<Success>>;