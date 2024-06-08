using _2Sport_BE.DataContent;
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
        private readonly IShipmentDetailService _shipmentDetailService;
        private readonly IPaymentMethodService _paymentMethodService;


        public PaymentController(
            IPaymentService paymentService,
            IOrderService orderService,
            IUserService userService,
            ICartService cartService,
            ICartItemService cartItemService,
            IShipmentDetailService shipmentDetailService,
            IPaymentMethodService paymentMethodService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _userService = userService;
            _cartService = cartService;
            _cartItemService = cartItemService;
            _shipmentDetailService = shipmentDetailService;
            _paymentMethodService = paymentMethodService;
        }
        [HttpPost("checkout-orders")]
        public async Task<IActionResult> CreatePayOsLink([FromBody] OrderCM orderCM, int orderMethodId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            var user = await GetUserFromToken();
            Order order = null;
            ResponseModel<OrderVM> responseModel = null;
            OrderVM orderVM = null;
            if (user is null)
            {
                return Unauthorized("Invalid user!");
            }
            else
            {
                //Delete cartItem
                await DeleteCartItem(user.Id, orderCM.OrderDetails);
                if (orderMethodId.ToString().Trim().Equals(OrderMethods.PayOS))
                {
                    order = await CreateOrder(orderCM, (int)OrderMethods.PayOS);
                    //Generate payment link
                    var paymentLink = await _paymentService.PaymentWithPayOs(order.Id);
                    orderVM = new OrderVM()
                    {
                        FullName = order.ShipmentDetail.FullName,
                        Address = order.ShipmentDetail.Address,
                        PhoneNumber = order.ShipmentDetail.PhoneNumber,
                        PaymentMethod = order.PaymentMethod.PaymentMethodName,
                        ReceivedDate = order.ReceivedDate,
                        TransportFee = order.TransportFee,
                        IntoMoney = order.IntoMoney,
                        Status = order.Status,
                        PaymentLink = paymentLink
                    };
                    responseModel = new ResponseModel<OrderVM>
                    {
                        IsSuccess = true,
                        Message = "Query successfully!",
                        Data = orderVM
                    };
                    return Ok(responseModel);
                }
                else if (orderMethodId.ToString().Trim().Equals(OrderMethods.COD))
                {
                    order = await CreateOrder(orderCM, (int)OrderMethods.COD);

                    orderVM = new OrderVM()
                    {
                        FullName = order.ShipmentDetail.FullName,
                        Address = order.ShipmentDetail.Address,
                        PhoneNumber = order.ShipmentDetail.PhoneNumber,
                        PaymentMethod = order.PaymentMethod.PaymentMethodName,
                        ReceivedDate = order.ReceivedDate,
                        TransportFee = order.TransportFee,
                        IntoMoney = order.IntoMoney,
                        Status = order.Status
                    };
                    responseModel = new ResponseModel<OrderVM>
                    {
                         IsSuccess = true,
                         Message = "Query successfully!",
                         Data = orderVM
                    };
                    return Ok(responseModel);
                }

                responseModel = new ResponseModel<OrderVM>
                {
                    IsSuccess = false,
                    Message = "Somethings is wrong!",
                    Data = null
                };
                return NotFound(responseModel);
            }   
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
                checkOrderExist.Status = (int) OrderStatus.Canceled;
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
            var user = await GetUserFromToken();
            var shipmentDetail = await _shipmentDetailService.GetShipmentDetailById((int)orderCM.ShipmentDetailId);
            var status = (int?)OrderStatus.Order_Confirmation;
            var paymentMethod = await _paymentMethodService.GetPaymentMethodAsync(paymentMethodId);
            Order order = new Order()
            {
                OrderCode = GenerateOrderCode(),
                Status = status,
                TransportFee = orderCM.TransportFee,
                PaymentMethodId = paymentMethodId,
                PaymentMethod = paymentMethod,
                ShipmentDetailId = shipmentDetail.Id,
                ShipmentDetail = shipmentDetail,
                ReceivedDate = orderCM.ReceivedDate,
                UserId = GetCurrentUserIdFromToken(),
                User = user
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