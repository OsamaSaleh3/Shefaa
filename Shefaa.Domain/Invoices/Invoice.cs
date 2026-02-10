using ErrorOr;
using Shefaa.Domain.InvoiceItems;
using Shefaa.Domain.Invoices.enums;
using Shefaa.Domain.Patients;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Shefaa.Domain.Invoices;

public partial class Invoice: BaseEntity
{

    public string InvoiceNumber { get; private set; } = null!;

    public Guid PatientId { get; private set; }

    public DateTime? InvoiceDate { get; private set; }

    public decimal? TotalAmount { get; private set; }

    public decimal? PaidAmount { get; private set; }

    public decimal? RemainingAmount { get; private set; }

    public InvoiceStatus? Status { get; private set; }

    public PaymentMethod? PaymentMethod { get; private set; }

    public string? Notes { get; private set; }

    public virtual List<InvoiceItem> InvoiceItems { get; private set; } = new List<InvoiceItem>();

    public virtual Patient Patient { get; private set; } = null!;

    internal Invoice()
    {
    }

    private Invoice(string invoiceNumber, Guid patientId)
    {
        InvoiceNumber = invoiceNumber;
        PatientId = patientId;
        InvoiceDate = DateTime.Now;
        Status = InvoiceStatus.Unpaid;
    }

    public static ErrorOr<Invoice> Create(string invoiceNumber, Guid patientId)
    {
        if (string.IsNullOrWhiteSpace(invoiceNumber))
            return InvoiceErrors.InvalidInvoiceNumber;

        return new Invoice(invoiceNumber, patientId);
    }

    public ErrorOr<Success> RecordPayment(decimal amount, PaymentMethod method)
    {
        if (Status == InvoiceStatus.Cancelled)
            return InvoiceErrors.CancelledInvoicePayment;

        if (Status == InvoiceStatus.Paid)
            return InvoiceErrors.AlreadyPaid;

        if (amount <= 0)
            return InvoiceErrors.InvalidPaymentAmount;

        if (amount > RemainingAmount)
            return InvoiceErrors.PaymentExceedsRemaining;

        PaidAmount += amount;
        PaymentMethod = method;

        UpdateStatus();
        MarkAsUpdated();
        return Result.Success;
    }

    private void UpdateStatus()
    {
        if (PaidAmount == 0)
        {
            Status= InvoiceStatus.Unpaid;
        }
        else if (PaidAmount < TotalAmount)
        {
            Status = InvoiceStatus.PartiallyPaid;
        }
        else
        {
            Status = InvoiceStatus.Paid;
        }
    }
}
