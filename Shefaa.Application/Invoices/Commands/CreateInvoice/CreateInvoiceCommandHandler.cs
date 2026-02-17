using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Invoices;

namespace Shefaa.Application.Invoices.Commands.CreateInvoice;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, ErrorOr<Guid>>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPatientRepository _patientRepository;

    public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IPatientRepository patientRepository)
    {
        _invoiceRepository = invoiceRepository;
        _patientRepository = patientRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId);
        if (patient is null) return Error.NotFound("Patient.NotFound");

        var InvoiceNumber = await GenerateInvoiceNumber();

        if (await _invoiceRepository.ExistsAsync(InvoiceNumber))
            return Error.Conflict("Invoice.DuplicateNumber");

        var invoiceResult=Invoice.Create(InvoiceNumber, request.PatientId);
        if(invoiceResult.IsError)
            return invoiceResult.Errors;

        var invoice=invoiceResult.Value;

         await _invoiceRepository.AddAsync(invoice);
        return invoice.Id;

    }

    private async Task<string> GenerateInvoiceNumber()
    {
        var currentYear = DateTime.UtcNow.Year;
        var lastInvoice = await _invoiceRepository.GetLastInvoiceNumberForYearAsync(currentYear);
        int newSequence = 1;
        if (!string.IsNullOrEmpty(lastInvoice))
        {
            var parts = lastInvoice.Split('-');
            if (parts.Length == 3 && int.TryParse(parts[2], out int lastSeq))
            {
                newSequence = lastSeq + 1;
            }
        }
        return $"INV-{currentYear}-{newSequence:D4}";
    }
}