using RefactorThis.Persistence.Interface;
using RefactorThis.Persistence.Constant;
using System.Linq;
using System;
using RefactorThis.Persistence.Enum;

namespace RefactorThis.Persistence
{
    public class PaymentProcessor : IPaymentProcessor
    {
        public string ProcessPayment(Invoice inv, Payment payment)
        {
            var responseMessage = String.Empty;
            if (inv == null) 
                throw new InvalidOperationException(ResponseMessage.NoInvMatch);

            if (inv.Amount == 0)
                responseMessage = (inv.Payments == null || !inv.Payments.Any()) ? ResponseMessage.NoPayNeed : throw new InvalidOperationException(ResponseMessage.InvalidState);
            else if (inv.Payments != null && inv.Payments.Any())
            {
                var sumAmount = inv.Payments.Sum(x => x.Amount);
                if (sumAmount != 0 && inv.Amount == sumAmount)
                    responseMessage = ResponseMessage.InvWasFullyPaid;
                else if (sumAmount != 0 && payment.Amount > (inv.Amount - inv.AmountPaid))
                    responseMessage = ResponseMessage.GreaterPartialAmt;
                else
                    return ProcessPartialPayment(inv, payment);
            }
            else
                responseMessage = payment.Amount > inv.Amount ? ResponseMessage.GreaterInvAmt : ProcessFullPayment(inv, payment);

            inv.Save();
            return responseMessage;
        }
        private string ProcessPartialPayment(Invoice inv, Payment payment)
        {
            inv.AmountPaid += payment.Amount;
            inv.Payments.Add(payment);
            if (inv.Type == InvoiceType.Commercial)
                inv.TaxAmount += payment.Amount * 0.14m;
            return (inv.Amount - inv.AmountPaid) == payment.Amount ? ResponseMessage.FinPayRecv : ResponseMessage.PartialPayRecv;
        }
        private string ProcessFullPayment(Invoice inv, Payment payment)
        {
            inv.AmountPaid = payment.Amount;
            inv.TaxAmount = payment.Amount * 0.14m;
            inv.Payments.Add(payment);
            return inv.Amount == payment.Amount ? ResponseMessage.InvIsFullyPaid : ResponseMessage.InvPartiallyPaid;
        }
    }
}
