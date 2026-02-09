using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.InvoiceItems
{
    public static class InvoiceItemErrors
    {
        public static readonly Error EmptyDescription = Error.Validation(
        code: "InvoiceItem.EmptyDescription",
        description: "Description cannot be empty or null.");

        public static readonly Error InvalidQuantity = Error.Validation(
            code: "InvoiceItem.InvalidQuantity",
            description: "Quantity must be greater than zero.");

        public static readonly Error NegativePrice = Error.Validation(
            code: "InvoiceItem.NegativePrice",
            description: "Unit price cannot be negative.");
    }
}
