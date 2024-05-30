using _2Sport_BE.Helpers;
using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using _2Sport_BE.Service.Services;
using _2Sport_BE.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [Route("get-cart/{userId}")]
        public async Task<IActionResult> GetCarts(int userId, DefaultSearch defaultSearch)
        {
            try
            {
                var query = await _cartItemService.GetCartItems(userId, defaultSearch.currentPage, defaultSearch.perPage);
                var cartItems = query.Select(_ => _mapper.Map<CartItem, CartItemVM>(_)).ToList();
                foreach (var carItem in cartItems)
                {
                    carItem.ProductName = (await _unitOfWork.ProductRepository.FindAsync(carItem.ProductId)).ProductName;
                }
                return Ok(new { total = cartItems.Count(), data = cartItems });
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        [Route("add-to-cart/{userId}")]
        public async Task<IActionResult> AddToCart(int userId, CartItemCM cartItemCM)
        {
            try
            {
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
    }
}
