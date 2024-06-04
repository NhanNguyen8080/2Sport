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
    public class PayOSSettings
    {
        public string ClientId { get; set; }
        public string ApiKey { get; set; }
        public string ChecksumKey { get; set; }
    }
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderAsync(int id);
        Task<Order> AddOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
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

        public async Task<bool> UpdateOrderAsync(Order order)
        {
           await _unitOfWork.OrderRepository.UpdateAsync(order);
           return true;
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

        public async Task<Order> GetOrderAsync(int id)
        {
            return await _unitOfWork.OrderRepository.GetObjectAsync(_ => _.Id == id);
        }
    }

}
