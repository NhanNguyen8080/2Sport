using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Sport_BE.Service.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<IEnumerable<Order>> ListAllOrderByUseIdAsync(int userId);
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> GetOrderByIdFromUserAsync(int id, int userId);
        Task<Order> AddOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(int orderId, int status);
        Task<bool> DeleteOrderAsync(int id);
    }
    public class OrderService : IOrderService
    {
        private readonly TwoSportDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(TwoSportDBContext context, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _context = context;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _unitOfWork.OrderRepository.GetAllAsync();
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await _unitOfWork.OrderRepository.InsertAsync(order);
            return order;
        }

        public async Task<bool> UpdateOrderAsync(int orderId, int status)
        {
            var checkExist = await _unitOfWork.OrderRepository.GetObjectAsync(_ => _.Id == orderId);
            if(checkExist != null)
            {
                checkExist.Status = status;
                await _unitOfWork.OrderRepository.UpdateAsync(checkExist);
                return true;
            }
            return false;
           
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            await _unitOfWork.OrderRepository.DeleteAsync(id);
            return true;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _unitOfWork.OrderRepository.GetObjectAsync(_ => _.Id == id);
        }

        public async Task<Order> GetOrderByIdFromUserAsync(int id, int userId)
        {
            return await _unitOfWork.OrderRepository.GetObjectAsync(_ => _.Id == id && _.UserId == userId);
        }

        public async Task<IEnumerable<Order>> ListAllOrderByUseIdAsync(int userId)
        {
            var result = await _unitOfWork.OrderRepository.GetAsync(_ => _.UserId ==  userId);
            return result.OrderBy(_ => _.ReceivedDate);
        }

    }

}
