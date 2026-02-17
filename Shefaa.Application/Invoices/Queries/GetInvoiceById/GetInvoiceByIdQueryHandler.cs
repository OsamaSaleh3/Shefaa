using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Invoices.Dtos;
using Shefaa.Application.Invoices.Queries.GetInvoiceById;
using Shefaa.Domain.Invoices;


public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, ErrorOr<InvoiceDto>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetInvoiceByIdQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<ErrorOr<InvoiceDto>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdWithDetailsAsync(request.InvoiceId);
        if (invoice is null) return Error.NotFound("Invoice.NotFound");

        return new InvoiceDto(
           invoice.Id,
           invoice.InvoiceNumber,
           $"{invoice.Patient.FirstName} {invoice.Patient.LastName}",
           invoice.InvoiceDate,
           invoice.TotalAmount,
           invoice.PaidAmount,
           invoice.RemainingAmount,
           invoice.Status.ToString(),
           invoice.Notes,
           invoice.InvoiceItems.Select(i => new InvoiceItemDto(i.Id, i.Description, i.Quantity, i.UnitPrice, i.TotalPrice)).ToList()
       );
    }

   
}