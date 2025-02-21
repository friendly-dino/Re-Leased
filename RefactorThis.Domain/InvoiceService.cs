using System;
using RefactorThis.Persistence;
using RefactorThis.Persistence.Interface;

namespace RefactorThis.Domain
{
	public class InvoiceService
	{
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPaymentProcessor _paymentProcessor;
        public InvoiceService(IInvoiceRepository invoiceRepository, IPaymentProcessor paymentProcessor)
        {
            _invoiceRepository = invoiceRepository;
            _paymentProcessor = paymentProcessor;
        }
        public string ProcessPayment(Payment payment)
        {
            try
            {
                var inv = _invoiceRepository.GetInvoice(payment.Reference);
                return _paymentProcessor.ProcessPayment(inv, payment);
            }
            catch (InvalidOperationException ex)
            {
                //error logger
                Console.WriteLine(ex.Message);
                //throw or return
                return "An error occurred while processing your payment.";
            }
           
        }
    }
}