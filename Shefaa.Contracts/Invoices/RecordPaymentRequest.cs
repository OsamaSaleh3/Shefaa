using Shefaa.Domain.Invoices.enums;

namespace Shefaa.Contracts.Invoices;

public record RecordPaymentRequest(
    decimal Amount,
    PaymentMethod PaymentMethod
);
