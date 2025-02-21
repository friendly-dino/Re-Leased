
namespace RefactorThis.Persistence.Interface
{
    public interface IPaymentProcessor
    {
        string ProcessPayment(Invoice inv, Payment payment);
    }
}
