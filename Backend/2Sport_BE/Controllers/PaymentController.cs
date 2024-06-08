using _2Sport_BE.DataContent;
using _2Sport_BE.Infrastructure.Services;
using _2Sport_BE.Repository.Interfaces;
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
        private readonly IOrderDetailService _orderDetailService;
        private readonly IUserService _userService;
        private readonly ICartItemService _cartItemService;
        private readonly ICartService _cartService;
        private readonly IShipmentDetailService _shipmentDetailService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentController(
            IPaymentService paymentService,
            IOrderService orderService,
            IUserService userService,
            ICartService cartService,
            ICartItemService cartItemService,
            IShipmentDetailService shipmentDetailService,
            IPaymentMethodService paymentMethodService,
            IProductService productService,
            IOrderDetailService orderDetailService,
            IUnitOfWork unitOfWork)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _userService = userService;
            _cartService = cartService;
            _cartItemService = cartItemService;
            _shipmentDetailService = shipmentDetailService;
            _paymentMethodService = paymentMethodService;
            _productService = productService;
            _orderDetailService = orderDetailService;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("checkout-orders")]
        public async Task<IActionResult> CreatePayOsLink([FromBody] OrderCM orderCM, int orderMethodId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            
            var user = await GetUserFromToken();
            var cart = await _cartService.GetCartByUserId(user.Id);
            Order order = null;
            ResponseModel<OrderVM> responseModel = null;
            OrderVM orderVM = null;
            if (user is null)
            {
                return Unauthorized("Invalid user!");
            }else if (cart == null || !cart.CartItems.Any())
            {
                return NotFound("Your Cart is empty");
            }
            else
            {
                
                if (orderMethodId == (int)OrderMethods.PayOS)
                {
                    order = await CreateOrder(orderCM, (int)OrderMethods.PayOS);
                    //Generate payment link
                    var paymentLink = await _paymentService.PaymentWithPayOs(order.Id);
                    var check = await DeleteCartItem(user.Id, orderCM.OrderDetails);
                    if (check)
                    {
                        orderVM = new OrderVM()
                        {
                            ShipmentDetailId = orderCM.ShipmentDetailId,
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
                    else
                    {
                        responseModel = new ResponseModel<OrderVM>
                        {
                            IsSuccess = false,
                            Message = "Somethings is wrong!",
                            Data = null
                        };
                        return NotFound(responseModel);
                    }
                }
                else if (orderMethodId== (int)OrderMethods.COD)
                {
                    order = await CreateOrder(orderCM, (int)OrderMethods.COD);

                    var check = await DeleteCartItem(user.Id, orderCM.OrderDetails);
                    if (check)
                    {
                        orderVM = new OrderVM()
                        {
                            ShipmentDetailId = orderCM.ShipmentDetailId,
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
                    else
                    {
                        responseModel = new ResponseModel<OrderVM>
                        {
                            IsSuccess = false,
                            Message = "Somethings is wrong!",
                            Data = null
                        };
                        return NotFound(responseModel);
                    }
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
            var status = (int?)OrderStatus.Order_Confirmation;
            var paymentMethod = await _paymentMethodService.GetPaymentMethodAsync(paymentMethodId);
            
            Order order = new Order()
            {
                OrderCode = GenerateOrderCode(),
                Status = status,
                TransportFee = orderCM.TransportFee,
                PaymentMethodId = paymentMethodId,
                PaymentMethod = paymentMethod,
                ShipmentDetailId = (int)orderCM.ShipmentDetailId,
                ReceivedDate = orderCM.ReceivedDate,
                UserId = GetCurrentUserIdFromToken(),
                User = user
            };
            decimal intoMoney = 0;
            decimal totalPrice = 0;
            order.OrderDetails = new List<OrderDetail>();
            for (int i = 0; i < orderCM.OrderDetails.Count; i++)
            {
                var product = await _productService.GetProductById((int)orderCM.OrderDetails[i].ProductId);
                if (product != null)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductId = product.Id,
                        Product = product,
                        Price = orderCM.OrderDetails[i].Price,
                        Quantity = orderCM.OrderDetails[i].Quantity,
                    };
                    order.OrderDetails.Add(orderDetail);
                    totalPrice += (decimal) (orderCM.OrderDetails[i].Price * orderCM.OrderDetails[i].Quantity); // Tính tổng giá trị đúng
                }
            }
            order.TotalPrice = totalPrice;
            order.IntoMoney = totalPrice + orderCM.TransportFee;
            await _orderService.AddOrderAsync(order);
           
            return order;
        }
        [NonAction]
        protected async Task<bool> DeleteCartItem(int userId, List<OrderDetailRequest> orderDetails)
        {
            var cart = await _cartService.GetCartByUserId(userId);
            if (cart != null)
            {
                var cartItems = cart.CartItems.ToList();
                for (int i = 0; i < cartItems.Count; i++)
                {
                    if (cartItems[i].ProductId == orderDetails[i].ProductId)
                    {
                        await _cartItemService.DeleteCartItem(cartItems[i].Id);
                    }
                }
                return true;
            }
            return false;
        }
    }
}