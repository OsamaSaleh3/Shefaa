using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Invoices.Commands.RecordPayment;

public class RecordPaymentCommandHandler : IRequestHandler<RecordPaymentCommand, ErrorOr<Success>>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public RecordPaymentCommandHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<ErrorOr<Success>> Handle(RecordPaymentCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdWithItemsAsync(request.InvoiceId);
        if (invoice is null) return Error.NotFound("Invoice.NotFound");

        var result = invoice.RecordPayment(request.Amount, request.PaymentMethod);
        if (result.IsError) return result.Errors;

        await _invoiceRepository.UpdateAsync(invoice);
        return Result.Success;
    }
}