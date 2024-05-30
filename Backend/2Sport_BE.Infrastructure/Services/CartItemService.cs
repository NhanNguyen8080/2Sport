using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Sport_BE.Service.Services
{
    public interface ICartItemService
    {
        Task<IQueryable<CartItem>> GetCartItems(int userId, int pageIndex, int pageSize);
        Task<CartItem> AddCartItem(int userId, CartItem cartItem);
    }
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TwoSportDBContext _dbContext;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Cart> _cartRepository;
        private IGenericRepository<CartItem> _cartItemRepository;
        private IGenericRepository<Product> _productRepository;

        public CartItemService(IUnitOfWork unitOfWork, TwoSportDBContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _userRepository = _unitOfWork.UserRepository;
            _cartRepository = _unitOfWork.CartRepository;
            _cartItemRepository = _unitOfWork.CartItemRepository;
            _productRepository = _unitOfWork.ProductRepository;
        }

        public async Task<CartItem> AddCartItem(int userId, CartItem cartItem)
        {
            var user = (await _userRepository.GetAsync(_ => _.Id == userId)).FirstOrDefault();
            if (user == null)
            {
                return null;
            } else
            {
                var cart = (await _cartRepository.GetAsync(_ => _.UserId == userId)).FirstOrDefault();
                try
                {
                    if (cart != null)
                    {
                        cartItem = await AddCartItem(cart, cartItem);
                        return cartItem;
                    }
                    else
                    {
                        var newCart = new Cart()
                        {
                            UserId = userId,
                            User = user
                        };
                        await _cartRepository.InsertAsync(newCart);
                        try
                        {
                            _unitOfWork.Save();
                        } catch (Exception ex)
                        {

                        }
                        cartItem.CartId = newCart.Id;
                        cartItem.Cart = newCart;
                        cartItem = await AddCartItem(newCart, cartItem);
                        return cartItem;
                    }

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            
            
        }

        public async Task<CartItem> AddCartItem(Cart cart, CartItem cartItem)
        {
            var currentItem = (await _cartItemRepository.GetAsync(_ => _.ProductId == cartItem.ProductId)).FirstOrDefault();
            var product = (await _productRepository.GetAsync(_ => _.Id == cartItem.ProductId)).FirstOrDefault();
            if (currentItem != null)
            {
                currentItem.Quantity += cartItem.Quantity;
                var totalPrice = product.Price * cartItem.Quantity;
                currentItem.TotalPrice += totalPrice;
                currentItem.CartId = cart.Id;
                currentItem.Cart = cart;
                try
                {
                    await _cartItemRepository.UpdateAsync(currentItem);
                    return currentItem;
                }
                catch (Exception ex)
                {
                    return null;
                }
            } else
            {
                cartItem.CartId = cart.Id;
                cartItem.ProductId = product.Id;
                var totalPrice = product.Price * cartItem.Quantity;
                cartItem.TotalPrice = totalPrice;
                try
                {
                    await _cartItemRepository.InsertAsync(cartItem);
                    return cartItem;
                } catch (Exception ex)
                {
                    return null;
                }
            }

        }

        public async Task<IQueryable<CartItem>> GetCartItems(int userId, int pageIndex, int pageSize)
        {
            var queryCart = await _cartRepository.GetAsync(_ => _.UserId == userId);
            var cart = queryCart.FirstOrDefault();
            if (cart != null)
            {
                var cartItems = await _cartItemRepository.GetAsync(_ => _.CartId == cart.Id, null, "", pageIndex, pageSize);
                return cartItems.AsQueryable();
            }
            else
            {
                return null;
            }
        }
    }
}
