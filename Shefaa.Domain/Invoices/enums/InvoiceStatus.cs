using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.Invoices.enums
{
    public enum InvoiceStatus
    {
        Unpaid = 1,       
        PartiallyPaid = 2,
        Paid = 3,         
        Cancelled = 4
    }
}
