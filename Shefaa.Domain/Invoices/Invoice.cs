using ErrorOr;
using Shefaa.Domain.InvoiceItems;
using Shefaa.Domain.Invoices.enums;
using Shefaa.Domain.Patients;

namespace Shefaa.Domain.Invoices;

public partial class Invoice : BaseEntity
{
    public string InvoiceNumber { get; private set; } = null!;
    public Guid PatientId { get; private set; }
    public DateTime InvoiceDate { get; private set; }

    public decimal TotalAmount { get; private set; } = 0;
    public decimal PaidAmount { get; private set; } = 0;
    public decimal RemainingAmount { get; private set; } = 0;

    public InvoiceStatus Status { get; private set; }
    public PaymentMethod? PaymentMethod { get; private set; }
    public string? Notes { get; private set; }

    private readonly List<InvoiceItem> _invoiceItems = new();
    public IReadOnlyCollection<InvoiceItem> InvoiceItems => _invoiceItems.AsReadOnly();

    public Patient Patient { get; private set; } = null!;

    private Invoice() { }

    private Invoice(string invoiceNumber, Guid patientId)
    {
        InvoiceNumber = invoiceNumber;
        PatientId = patientId;
        InvoiceDate = DateTime.UtcNow;
        Status = InvoiceStatus.Unpaid;
        TotalAmount = 0;
        RemainingAmount = 0;
    }

    public static ErrorOr<Invoice> Create(string invoiceNumber, Guid patientId)
    {
        if (string.IsNullOrWhiteSpace(invoiceNumber))
            return InvoiceErrors.InvalidInvoiceNumber;

        return new Invoice(invoiceNumber, patientId);
    }

   
    public ErrorOr<Success> AddItem(string description, int quantity, decimal unitPrice)
    {
        if (Status == InvoiceStatus.Paid || Status == InvoiceStatus.Cancelled)
            return Error.Conflict("Invoice.Locked", "Cannot add items to a paid or cancelled invoice.");

        var itemResult = InvoiceItem.Create(this.Id, description, quantity, unitPrice);

        if (itemResult.IsError)
            return itemResult.Errors;

        _invoiceItems.Add(itemResult.Value);

        RecalculateTotals();

        return Result.Success;
    }

  
    public ErrorOr<Success> RemoveItem(Guid itemId)
    {
        if (Status == InvoiceStatus.Paid || Status == InvoiceStatus.Cancelled)
            return Error.Conflict("Invoice.Locked", "Cannot remove items from a paid or cancelled invoice.");

        var item = _invoiceItems.FirstOrDefault(i => i.Id == itemId); 

        if (item is null) return Error.NotFound("InvoiceItem.NotFound");

        _invoiceItems.Remove(item);

        RecalculateTotals();

        return Result.Success;
    }

  
    public ErrorOr<Success> RecordPayment(decimal amount, PaymentMethod method)
    {
        if (Status == InvoiceStatus.Cancelled) return InvoiceErrors.CancelledInvoicePayment;
        if (Status == InvoiceStatus.Paid) return InvoiceErrors.AlreadyPaid;
        if (amount <= 0) return InvoiceErrors.InvalidPaymentAmount;
        if (amount > RemainingAmount) return InvoiceErrors.PaymentExceedsRemaining;

        PaidAmount += amount;
        PaymentMethod = method;

        RecalculateTotals(); 

        return Result.Success;
    }

 
    public void CancelInvoice()
    {
        if (Status != InvoiceStatus.Paid) 
        {
            Status = InvoiceStatus.Cancelled;
            MarkAsUpdated();
        }
    }

  
    private void RecalculateTotals()
    {
        TotalAmount = _invoiceItems.Sum(i => i.TotalPrice);

        RemainingAmount = TotalAmount - PaidAmount;

        UpdateStatus();

        MarkAsUpdated();
    }

    private void UpdateStatus()
    {
        if (Status == InvoiceStatus.Cancelled) return;

        if (TotalAmount == 0)
            Status = InvoiceStatus.Unpaid;
        else if (RemainingAmount <= 0) 
            Status = InvoiceStatus.Paid;
        else if (PaidAmount > 0)
            Status = InvoiceStatus.PartiallyPaid;
        else
            Status = InvoiceStatus.Unpaid;
    }
}