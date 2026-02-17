using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;

namespace Shefaa.Application.Invoices.Commands.RemoveInvoiceItem;

public class RemoveInvoiceItemCommandHandler : IRequestHandler<RemoveInvoiceItemCommand, ErrorOr<Success>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public RemoveInvoiceItemCommandHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<ErrorOr<Success>> Handle(RemoveInvoiceItemCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdWithItemsAsync(request.InvoiceId);
        if (invoice is null) return Error.NotFound("Invoice.NotFound");

        var result = invoice.RemoveItem(request.ItemId);
        if (result.IsError) return result.Errors;

        await _invoiceRepository.UpdateAsync(invoice);
        return Result.Success;
    }
}