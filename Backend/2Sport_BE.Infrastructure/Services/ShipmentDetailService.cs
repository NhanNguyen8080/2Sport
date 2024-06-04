using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Sport_BE.Service.Services
{
    public interface IShipmentDetailService
    {
        Task<IQueryable<ShipmentDetail>> GetAllShipmentDetails(int userId);
        Task AddShipmentDetails(IEnumerable<ShipmentDetail> shipmentDetails);
        Task DeleteShipmentDetailById(int id);
        Task UpdateShipmentDetail(ShipmentDetail shipmentDetail);
    }
    public class ShipmentDetailService : IShipmentDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShipmentDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddShipmentDetails(IEnumerable<ShipmentDetail> shipmentDetails)
        {
           await _unitOfWork.ShipmentDetailRepository.InsertRangeAsync(shipmentDetails);
           await _unitOfWork.SaveChanges();
        }

        public async Task DeleteShipmentDetailById(int id)
        {
           await _unitOfWork.ShipmentDetailRepository.DeleteAsync(id);
           await _unitOfWork.SaveChanges();
        }

        public async Task<IQueryable<ShipmentDetail>> GetAllShipmentDetails(int userId)
        {
            var result = await _unitOfWork.ShipmentDetailRepository.GetAsync(_ => _.UserId == userId);
            return result.AsQueryable();
        }

        public async Task UpdateShipmentDetail(ShipmentDetail shipmentDetail)
        {
            await _unitOfWork.ShipmentDetailRepository.UpdateAsync(shipmentDetail);
            await _unitOfWork.SaveChanges();
        }
    }
}
