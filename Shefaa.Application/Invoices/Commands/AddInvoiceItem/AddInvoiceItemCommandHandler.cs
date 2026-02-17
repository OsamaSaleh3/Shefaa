using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;

namespace Shefaa.Application.Invoices.Commands.AddInvoiceItem;

public class AddInvoiceItemCommandHandler : IRequestHandler<AddInvoiceItemCommand, ErrorOr<Success>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public AddInvoiceItemCommandHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }
    public async Task<ErrorOr<Success>> Handle(AddInvoiceItemCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdWithItemsAsync(request.InvoiceId);
        if (invoice is null) return Error.NotFound("Invoice.NotFound");

        var result = invoice.AddItem(request.Description, request.Quantity, request.UnitPrice);
        if (result.IsError) return result.Errors;

        await _invoiceRepository.UpdateAsync(invoice);
        return Result.Success;
    }
}
