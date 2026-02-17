namespace Shefaa.Application.Invoices.Dtos;

public record InvoiceItemDto(
    Guid Id,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);