using _2Sport_BE.Repository.Implements;
using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using _2Sport_BE.Service.Services;
using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Sport_BE.Infrastructure.Services
{
    public class PayOSSettings
    {
        public string ClientId { get; set; }
        public string ApiKey { get; set; }
        public string ChecksumKey { get; set; }
    }
    public interface IPaymentService
    {
        //Ship COD
        Task PaymentWithShipCod(int orderId);
        //PAYOS
        Task<string> PaymentWithPayOs(int orderId);
        //VNPay
        Task PaymentWithVnPay(int orderId);
    }
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _orderService;
        private PayOS _payOs;
        private readonly IConfiguration _configuration;
        private PayOSSettings payOSSettings;

        public PaymentService(IUnitOfWork unitOfWork, IOrderService orderService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _configuration = configuration;
            payOSSettings = new PayOSSettings()
            {
                ClientId = _configuration["PayOSSettings:ClientId"],
                ApiKey = _configuration["PayOSSettings:ApiKey"],
                ChecksumKey = _configuration["PayOSSettings:ChecksumKey"]
            };
            _payOs = new PayOS(payOSSettings.ClientId, payOSSettings.ApiKey, payOSSettings.ChecksumKey);
        }

        public async Task<string> PaymentWithPayOs(int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);
            if(order != null)
            {
                List<ItemData> orders = new List<ItemData>();
                var listOrderDetail = order.OrderDetails.ToList();
                for(int i = 0; i < listOrderDetail.Count; i++)
                {
                    var name = listOrderDetail[i].Id.ToString();
                    var soluong = listOrderDetail[i].Quantity ?? 0;
                    var thanhtien = Convert.ToInt32(listOrderDetail[i].Price.ToString());
                    ItemData item = new ItemData(name, soluong, thanhtien);
                    orders.Add(item);
                }
                string content = $"Thanh toan hoa don {order.Id}";
                PaymentData data = new PaymentData(order.Id, Int32.Parse(order.IntoMoney.ToString()), content, orders, "cancelUrl", "returnUrl");
                var createPayment = await _payOs.createPaymentLink(data);
                return createPayment.checkoutUrl;
            }
            return String.Empty;
        }

        public Task PaymentWithShipCod(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task PaymentWithVnPay(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
