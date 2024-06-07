using _2Sport_BE.Infrastructure.Services;
using _2Sport_BE.Repository.Models;
using _2Sport_BE.Service.Enums;
using _2Sport_BE.Service.Services;
using _2Sport_BE.ViewModels;
using MailKit.Search;
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
        private readonly IUserService _userService;
        private readonly ICartItemService _cartItemService;
        private readonly ICartService _cartService;
        public PaymentController(IPaymentService paymentService, IOrderService orderService, IUserService userService, ICartService cartService, ICartItemService cartItemService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _userService = userService;
            _cartService = cartService;
            _cartItemService = cartItemService;
        }
        [HttpPost("payOs-payment-link")]
        public async Task<IActionResult> CreatePayOsLink([FromBody] OrderCM orderCM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }

            var order = await CreateOrder(orderCM, (int)OrderMethods.PayOS);
            int userId = GetCurrentUserIdFromToken();
            //Xoa caritem khi add vo
            await DeleteCartItem(userId, orderCM.OrderDetails);
            //Generate ra link để thanh toán
            var paymentLink =await _paymentService.PaymentWithPayOs(order.Id);
            return Ok(paymentLink);
        }
        [HttpPost("COD-payment-link")]
        public async Task<IActionResult> CreatePaymentLink([FromBody] OrderCM orderCM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            var order = await CreateOrder(orderCM, (int)OrderMethods.COD);
            int userId = GetCurrentUserIdFromToken();
            //Xoa caritem khi add vo
            await DeleteCartItem(userId, orderCM.OrderDetails);
            return Ok(order);
        }

        [HttpGet("GetPaymentLinkInformation/{orderId}")]
        public async Task<IActionResult> GetPaymentLinkInformation(int orderId)
        {
            try
            {
                var paymentLinkInfo = await _paymentService.GetPaymentLinkInformationAsync(orderId);
                if (paymentLinkInfo == null)
                {
                    return NotFound();
                }
                return Ok(paymentLinkInfo);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("CancelPaymentLink")]
        public async Task<IActionResult> CancelPaymentLink([FromBody] CancelPaymentRequest request)
        {
            try
            {
                var checkOrderExist = await _orderService.GetOrderByIdFromUserAsync(request.OrderId, GetCurrentUserIdFromToken());
                if (checkOrderExist == null){
                    return BadRequest("You don't have permission in this function");
                }
                checkOrderExist.Status = (int) OrderStatus.Deleted;
                var cancelledPaymentLinkInfo = await _paymentService.CancelPaymentLink(request.OrderId, request.Reason);
                return Ok(cancelledPaymentLinkInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [NonAction]
        public string GenerateOrderCode()
        {
            Random random = new Random();
            int randomDigits = random.Next(100000, 1000000);
            return randomDigits.ToString();
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
        [NonAction]
        private async Task<User> GetUserFromToken()
        {
            var user = await _userService.GetAsync(_ => _.Id == GetCurrentUserIdFromToken());
            return user.FirstOrDefault();
        }
        [NonAction]
        protected async Task<Order> CreateOrder(OrderCM orderCM, int paymentMethodId)
        {
            Order order = new Order()
            {
                OrderCode = GenerateOrderCode(),
                Status = (int?)OrderStatus.Active,
                TransportFee = orderCM.TransportFee,
                PaymentMethodId = paymentMethodId,
                ShipmentDetailId = orderCM.ShipmentDetailId,
                ReceivedDate = orderCM.ReceivedDate,
                UserId = GetCurrentUserIdFromToken(),
                User = await GetUserFromToken()
            };
            decimal intoMoney = 0;
            decimal totalPrice = 0;
            for (int i = 0; i < orderCM.OrderDetails.Count; i++)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = orderCM.OrderDetails[i].ProductId,
                    Price = orderCM.OrderDetails[i].Price,
                    Quantity = orderCM.OrderDetails[i].Quantity
                });
                totalPrice += orderCM.OrderDetails[i].Price;
            }
            order.TotalPrice = totalPrice;
            order.IntoMoney = totalPrice + orderCM.TransportFee;
            await _orderService.AddOrderAsync(order);
            return order;
        }
        [NonAction]
        protected async Task DeleteCartItem(int userId, List<OrderDetailRequest> orderDetails)
        {
            var cart = await _cartService.GetCartByUserId(userId);
            if (cart != null)
            {
                var cartItems = cart.CartItems.ToList();
                for(int i = 0; i< cartItems.Count; i++)
                {
                    if (cartItems[i].ProductId == orderDetails[i].ProductId) {
                        await _cartItemService.DeleteCartItem(cartItems[i].Id);
                    }          
                }
            }
        }
    }
}