using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorThis.Persistence.Interface
{
    public interface IPaymentProcessor
    {
        string ProcessPayment(Invoice inv, Payment payment);
    }
}
