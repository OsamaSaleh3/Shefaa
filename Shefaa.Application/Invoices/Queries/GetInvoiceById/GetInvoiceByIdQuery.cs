using ErrorOr;
using MediatR;
using Shefaa.Application.Invoices.Dtos;

namespace Shefaa.Application.Invoices.Queries.GetInvoiceById;

public record GetInvoiceByIdQuery(Guid InvoiceId) : IRequest<ErrorOr<InvoiceDto>>;