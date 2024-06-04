using _2Sport_BE.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace _2Sport_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("payOs-payment-link")]
        public async Task<IActionResult> CreatePayOsLink(int orderId)
        {
            if (orderId == null)
            {
                return BadRequest("Invalid request data.");
            }

            var paymentLink = await _paymentService.PaymentWithPayOs(orderId);

            return Ok(new { PaymentLink = paymentLink });
        }
        [HttpPost("COD-payment-link")]
        public async Task<IActionResult> CreatePaymentLink(int orderId)
        {
            if (orderId == null)
            {
                return BadRequest("Invalid request data.");
            }

            var paymentLink = await _paymentService.PaymentWithPayOs(orderId);

            return Ok(new { PaymentLink = paymentLink });
        }
    }
}