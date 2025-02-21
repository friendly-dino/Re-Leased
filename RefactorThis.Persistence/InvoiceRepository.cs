using RefactorThis.Persistence.Interface;

namespace RefactorThis.Persistence {
	public class InvoiceRepository:IInvoiceRepository
	{
		private Invoice _invoice;
        public Invoice GetInvoice(string reference) => _invoice;
        public void Add(Invoice invoice) => _invoice = invoice;
        public void SaveInvoice(Invoice invoice)
        {
            //saves the invoice to the database
        }
    }
}