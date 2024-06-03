using _2Sport_BE.Helpers;
using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using _2Sport_BE.Service.Services;
using _2Sport_BE.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _2Sport_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartItemService _cartItemService;
        private readonly IMapper _mapper;

        public CartController(IUnitOfWork unitOfWork, ICartItemService cartItemService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cartItemService = cartItemService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("get-cart")]
        public async Task<IActionResult> GetCarts(DefaultSearch defaultSearch)
        {
            try
            {
				var userId = GetCurrentUserIdFromToken();

				if (userId == 0)
				{
					return Unauthorized();
				}

				var query = await _cartItemService.GetCartItems(userId, defaultSearch.currentPage, defaultSearch.perPage);
                if (query != null)
                {
					var cartItems = query.Select(_ => _mapper.Map<CartItem, CartItemVM>(_)).ToList();
					if (cartItems != null)
					{
						foreach (var carItem in cartItems)
						{
							carItem.ProductName = (await _unitOfWork.ProductRepository.FindAsync(carItem.ProductId)).ProductName;
						}
						return Ok(new { total = cartItems.Count(), data = cartItems });
					}
					return BadRequest();
				}
                
				return BadRequest();
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("add-to-cart")]
        public async Task<IActionResult> AddToCart(CartItemCM cartItemCM)
        {
            try
            {
                var userId = GetCurrentUserIdFromToken();

                if (userId == 0)
                {
                    return Unauthorized();
                }

				var newCartItem = _mapper.Map<CartItemCM, CartItem>(cartItemCM);

                var addedCartItem = await _cartItemService.AddCartItem(userId, newCartItem);
                if (addedCartItem != null)
                {
                    return Ok(addedCartItem);
                } else
                {
                    return BadRequest("Add to cart failed");
                }
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

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
