using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.Invoices
{
    public static class InvoiceErrors
    {
        public static readonly Error InvalidInvoiceNumber = Error.Validation(
            code: "Invoice.InvalidInvoiceNumber",
            description: "Invoice number cannot be empty.");

        public static readonly Error InvalidPaymentAmount = Error.Validation(
            code: "Invoice.InvalidPaymentAmount",
            description: "Payment amount must be greater than zero.");

        public static readonly Error PaymentExceedsRemaining = Error.Validation(
            code: "Invoice.PaymentExceedsRemaining",
            description: "Payment amount cannot be greater than the remaining balance.");

        public static readonly Error AlreadyPaid = Error.Validation(
            code: "Invoice.AlreadyPaid",
            description: "This invoice is already fully paid.");

        public static readonly Error CancelledInvoicePayment = Error.Validation(
            code: "Invoice.CancelledInvoicePayment",
            description: "Cannot record payment for a cancelled invoice.");
    }
}
