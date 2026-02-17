using ErrorOr;
using MediatR;

namespace Shefaa.Application.Invoices.Commands.CreateInvoice;

public record CreateInvoiceCommand(
    Guid PatientId
) : IRequest<ErrorOr<Guid>>;