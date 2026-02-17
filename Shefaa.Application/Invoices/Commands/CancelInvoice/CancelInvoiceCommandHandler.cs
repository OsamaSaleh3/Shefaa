using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;

namespace Shefaa.Application.Invoices.Commands.CancelInvoice;


public class CancelInvoiceCommandHandler : IRequestHandler<CancelInvoiceCommand, ErrorOr<Success>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public CancelInvoiceCommandHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<ErrorOr<Success>> Handle(CancelInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdWithItemsAsync(request.InvoiceId);
        if (invoice is null) return Error.NotFound("Invoice.NotFound");

        invoice.CancelInvoice(); 

        await _invoiceRepository.UpdateAsync(invoice);
        return Result.Success;
    }
}