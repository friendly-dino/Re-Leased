using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorThis.Persistence.Constant
{
    public static class ResponseMessage
    {
        /// <summary>
        /// No payment needed.
        /// </summary>
        public static string NoPayNeed = "No payment needed.";
        /// <summary>
        /// There is no invoice matching this payment.
        /// </summary>
        public static string NoInvMatch = "There is no invoice matching this payment.";
        /// <summary>
        /// The invoice is in an invalid state; it has an amount of 0 and it has payments.
        /// </summary>
        public static string InvalidState = "The invoice is in an invalid state; it has an amount of 0 and it has payments.";
        /// <summary>
        /// Invoice was already fully paid.
        /// </summary>
        public static string InvWasFullyPaid = "Invoice was already fully paid.";
        /// <summary>
        /// The payment is greater than the partial amount remaining.
        /// </summary>
        public static string GreaterPartialAmt = "The payment is greater than the partial amount remaining.";
        /// <summary>
        /// Final partial payment received; invoice is now fully paid.
        /// </summary>
        public static string FinPayRecv = "Final partial payment received; invoice is now fully paid.";
        /// <summary>
        /// Another partial payment received; still not fully paid.
        /// </summary>
        public static string PartialPayRecv = "Another partial payment received; still not fully paid.";
        /// <summary>
        /// The payment is greater than the invoice amount.
        /// </summary>
        public static string GreaterInvAmt = "The payment is greater than the invoice amount.";
        /// <summary>
        /// Invoice is now fully paid.
        /// </summary>
        public static string InvIsFullyPaid = "Invoice is now fully paid.";
        /// <summary>
        /// Invoice is now partially paid.
        /// </summary>
        public static string InvPartiallyPaid = "Invoice is now partially paid.";
    }
}
