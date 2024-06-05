using _2Sport_BE.Infrastructure.Services;
using _2Sport_BE.Repository.Models;
using _2Sport_BE.Service.Enums;
using _2Sport_BE.Service.Services;
using _2Sport_BE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _2Sport_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;

        public PaymentController(IPaymentService paymentService, IOrderService orderService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
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
        /*
          public int Id { get; set; }
        public string OrderCode { get; set; }
        public int? OrderDetailId { get; set; }
        public bool? Status { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TransportFee { get; set; }
        public decimal? IntoMoney { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? ShipmentDetailId { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public int? TransportUnitId { get; set; }
        public int? UserId { get; set; }
         */
        [HttpPost("COD-payment-link")]
        public async Task<IActionResult> CreatePaymentLink([FromBody] OrderCM orderCM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            if(orderCM.ShipmentDetailId != 1)
            {
                return BadRequest("Invalid Shipment Detail data.");
            }
            int userId = GetCurrentUserIdFromToken();
            Order order = new Order()
            {
                OrderCode = GenerateOrderCode(),
                Status = true,
                TotalPrice = orderCM.TotalPrice,
                TransportFee = orderCM.TransportFee,
                IntoMoney = orderCM.IntoMoney,
                PaymentMethodId = (int?)OrderMethods.COD,
                ShipmentDetailId = orderCM.ShipmentDetailId,
                ReceivedDate = orderCM.ReceivedDate,
                UserId = userId
            };
            for (int i = 0; i < orderCM.OrderDetails.Count; i++)
            {
                order.OrderDetails.Add(new OrderDetail { OrderId =order.Id,
                                                        ProductId = orderCM.OrderDetails[i].ProductId,
                                                        Price = orderCM.OrderDetails[i].Price,
                                                        Quantity = orderCM.OrderDetails[i].Quantity
                });
            }
            await _orderService.AddOrderAsync(order);


            return Ok(order);

        }

        [NonAction]
        public string GenerateOrderCode()
        {
            return $"ORD-{DateTime.Now:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }
        [NonAction]
        protected int GetCurrentUserIdFromToken()
        {
            int UserId = 0;
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        string strUserId = identity.FindFirst("UserId").Value;
                        int.TryParse(strUserId, out UserId);

                    }
                }
                return UserId;
            }
            catch
            {
                return UserId;
            }
        }
    }
}