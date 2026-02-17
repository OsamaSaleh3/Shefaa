using ErrorOr;
using MediatR;
using Shefaa.Application.Invoices.Dtos;

namespace Shefaa.Application.Invoices.Queries.GetPatientInvoices;

public record GetPatientInvoicesQuery(Guid PatientId) : IRequest<ErrorOr<List<InvoiceDto>>>;