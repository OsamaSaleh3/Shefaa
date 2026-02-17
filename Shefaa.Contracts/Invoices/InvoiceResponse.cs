namespace Shefaa.Contracts.Invoices;

public record InvoiceResponse(
    Guid Id,
    string InvoiceNumber,
    string PatientName,
    DateTime Date,
    decimal TotalAmount,
    decimal PaidAmount,
    decimal RemainingAmount,
    string Status,
    string? Notes,
    List<InvoiceItemResponse> Items
);
