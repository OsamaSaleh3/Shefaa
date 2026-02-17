namespace Shefaa.Application.Invoices.Dtos;

public record InvoiceDto(
    Guid Id,
    string InvoiceNumber,
    string PatientName,
    DateTime Date,
    decimal TotalAmount,
    decimal PaidAmount,
    decimal RemainingAmount,
    string Status,
    string? Notes,
    List<InvoiceItemDto> Items
);
