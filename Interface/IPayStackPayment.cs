using CrudApiWithAuthentication.Models;

namespace CrudApiWithAuthentication.Interface
{
    public interface IPayStackPayment
    {
        Task<string> MakePayment(PaymentDto payment);
        Task<string> verifyPayment(string reference);
    }
}
