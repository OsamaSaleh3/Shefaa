namespace Shefaa.Contracts.Invoices;

public record AddInvoiceItemRequest(
    string Description,
    int Quantity,
    decimal UnitPrice
);
