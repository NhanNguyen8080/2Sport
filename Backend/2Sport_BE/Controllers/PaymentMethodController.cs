using _2Sport_BE.Repository.Models;
using _2Sport_BE.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace _2Sport_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentMethodController : Controller
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            var paymentMethods = await _paymentMethodService.GetPaymentMethodsAsync();
            return Ok(paymentMethods);
        }

        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(int id)
        {
            var paymentMethod = await _paymentMethodService.GetPaymentMethodAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return paymentMethod;
        }

        // PUT: api/PaymentMethods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(int id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return BadRequest();
            }

            var result = await _paymentMethodService.UpdatePaymentMethodAsync(paymentMethod);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/PaymentMethods
        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            var createdPaymentMethod = await _paymentMethodService.AddPaymentMethodAsync(paymentMethod);
            return CreatedAtAction("GetPaymentMethod", new { id = createdPaymentMethod.Id }, createdPaymentMethod);
        }

        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var result = await _paymentMethodService.DeletePaymentMethodAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
