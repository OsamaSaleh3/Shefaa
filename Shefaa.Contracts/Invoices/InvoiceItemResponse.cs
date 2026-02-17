namespace Shefaa.Contracts.Invoices;

public record InvoiceItemResponse(
    Guid Id,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);
