using CrudApiWithAuthentication.Interface;
using CrudApiWithAuthentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudApiWithAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayStackController : ControllerBase
    {
        private readonly IPayStackPayment _pay;
        public PayStackController(IPayStackPayment pay)
        {
            _pay = pay;
        }

        [HttpPost]
        [Route("MakePayment")]
        public async Task<IActionResult> MakePayment(PaymentDto paymentDto)
        {
            var res = await _pay.MakePayment(paymentDto);
            return Ok(res);
        }
        [HttpGet]
        [Route("VerifyPayment")]
        public async Task<IActionResult> verifyPayment(string reference)
        {
            var res = await _pay.verifyPayment(reference);
            return Ok(res);
        }
    }
}
