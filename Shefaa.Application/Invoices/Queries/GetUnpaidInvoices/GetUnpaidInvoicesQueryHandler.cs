using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Invoices.Dtos;
using Shefaa.Application.Invoices.Queries.GetUnpaidInvoices;

public class GetUnpaidInvoicesQueryHandler : IRequestHandler<GetUnpaidInvoicesQuery, ErrorOr<List<InvoiceDto>>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetUnpaidInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<ErrorOr<List<InvoiceDto>>> Handle(GetUnpaidInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _invoiceRepository.GetUnpaidInvoicesAsync();

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