using ErrorOr;
using MediatR;
using Shefaa.Application.Invoices.Dtos;

namespace Shefaa.Application.Invoices.Queries.GetUnpaidInvoices;

public record GetUnpaidInvoicesQuery() : IRequest<ErrorOr<List<InvoiceDto>>>;