using ErrorOr;
using Shefaa.Domain.Invoices;
using System;
using System.Collections.Generic;

namespace Shefaa.Domain.InvoiceItems;

public partial class InvoiceItem: BaseEntity
{
    public Guid InvoiceId { get; private set; }

    public string Description { get; private set; } = null!;

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal TotalPrice { get; private set; }

    public Invoice Invoice { get; private set; } = null!;

    internal InvoiceItem()
    {
    }

    private InvoiceItem(Guid invoiceId, string description, int? quantity, decimal unitPrice)
    {
        InvoiceId = invoiceId;
        Description = description;
        Quantity = quantity ?? 1;
        UnitPrice = unitPrice;
        CalculateTotal();
    }

    internal static ErrorOr<InvoiceItem> Create(Guid invoiceId, string description, int? quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(description))
            return InvoiceItemErrors.EmptyDescription;

        if (unitPrice < 0)
            return InvoiceItemErrors.InvalidUnitPrice;

        if (quantity.HasValue && quantity <= 0)
            return InvoiceItemErrors.InvalidQuantity;

        return new InvoiceItem(invoiceId, description, quantity, unitPrice);
    }

    internal ErrorOr<Success> UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            return InvoiceItemErrors.InvalidQuantity;

        Quantity = newQuantity;
        CalculateTotal();
        MarkAsUpdated();
        return Result.Success;
    }

    internal ErrorOr<Success> UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            return InvoiceItemErrors.InvalidUnitPrice;

        UnitPrice = newPrice;
        CalculateTotal();
        MarkAsUpdated();
        return Result.Success;
    }

    private void CalculateTotal()
    {
        TotalPrice = (Quantity) * UnitPrice;
    }
}
