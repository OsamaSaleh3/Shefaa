using Shefaa.Domain.Invoices;
using System;
using System.Collections.Generic;

namespace Shefaa.Domain.InvoiceItems;

public partial class InvoiceItem:BaseEntity
{
    public int InvoiceId { get; private set; }

    public string Description { get; private set; } = null!;

    public int? Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal? TotalPrice { get; private set; }

    public Invoice Invoice { get; private set; } = null!;

    public InvoiceItem( string description, int? quantity, decimal unitPrice)
    {
        if (string.IsNullOrEmpty(description))
        {

        }
        if (quantity <= 0)
        {

        }
        if (unitPrice < 0)
        {

        }

        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;

        CalculateTotal();
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
        {

        }
        Quantity = newQuantity;
        CalculateTotal();
        MarkAsUpdated();
    }

    public void UpdatePrice(decimal newPrice)
    {
        if(newPrice < 0)
        {

        }
        UnitPrice = newPrice;
        CalculateTotal();
        MarkAsUpdated();
    }


    private void CalculateTotal()
    {
        TotalPrice = Quantity * UnitPrice;
    }
}
