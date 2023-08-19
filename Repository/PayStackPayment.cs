using CrudApiWithAuthentication.Data;
using CrudApiWithAuthentication.Interface;
using CrudApiWithAuthentication.Models;
using PayStack.Net;

namespace CrudApiWithAuthentication.Repository
{
    public class PayStackPayment : IPayStackPayment
    {
        private PayStackApi paystack { get; set; }
        private readonly string token;
        private readonly IConfiguration _configuration;
        private readonly DbContextConn _db;
        public PayStackPayment(IConfiguration configuration, DbContextConn db)
        {
            _configuration = configuration;
            token = _configuration["PayStack:Key"];
            paystack = new PayStackApi(token);
            _db = db;
        }
        public async Task<string> MakePayment(PaymentDto payment)
        {
            TransactionInitializeRequest request = new()
            {
                AmountInKobo = payment.Amount * 100,
                Email = payment.Email,
                Reference = Generate().ToString(),
                Currency = "NGN",
                CallbackUrl = "https://winningkits.com/"
            };
            TransactionInitializeResponse response = paystack.Transactions.Initialize(request);
            if(response.Status == true)
            {
                var res = new User()
                {
                    Amount = payment.Amount,
                    Email = payment.Email,
                    TrxRef = request.Reference,
                    Name = payment.Name,
                    Password = "77",
                    Username = "kachi"
                    
                };
                _db.users.Add(res);
                await _db.SaveChangesAsync();
                return response.Data.AuthorizationUrl;                
            }
            return response.Message;
        }
        public static int Generate()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            return random.Next(1000000, 9999999);
        }

        public async Task<string> verifyPayment(string reference)
        {
            TransactionVerifyResponse response = paystack.Transactions.Verify(reference);
            if(response.Data.Status == "success")
            {
                var res = _db.users.Where(u => u.TrxRef == reference).FirstOrDefault();
                if(res != null)
                {
                    res.Status = true;
                    _db.users.Update(res);
                    await _db.SaveChangesAsync();
                    return response.Data.GatewayResponse;
                }
            }
            return response.Data.Message;
        }
    }
}
