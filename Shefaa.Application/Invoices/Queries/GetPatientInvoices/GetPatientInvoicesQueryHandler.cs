using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Invoices.Dtos;
using Shefaa.Application.Invoices.Queries.GetPatientInvoices;


public class GetPatientInvoicesQueryHandler : IRequestHandler<GetPatientInvoicesQuery, ErrorOr<List<InvoiceDto>>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetPatientInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<ErrorOr<List<InvoiceDto>>> Handle(GetPatientInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _invoiceRepository.GetByPatientIdAsync(request.PatientId);
        return invoices.Select(i => new InvoiceDto(
            i.Id,
            i.InvoiceNumber,
            $"{i.Patient.FirstName} {i.Patient.LastName}",
            i.InvoiceDate,
            i.TotalAmount,
            i.PaidAmount,
            i.RemainingAmount,
            i.Status.ToString(),
            i.Notes,
            i.InvoiceItems.Select(it => new InvoiceItemDto(
                it.Id,
                it.Description,
                it.Quantity,
                it.UnitPrice,
                it.TotalPrice
                )).ToList())).ToList();
    }
}