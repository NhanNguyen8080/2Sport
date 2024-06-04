using _2Sport_BE.Infrastructure.Services;
using _2Sport_BE.Repository.Models;
using _2Sport_BE.Service.Services;
using _2Sport_BE.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _2Shipment_BE.Controllers
{
    public class ShipmentDetailController : Controller
    {
        private readonly IShipmentDetailService _shipmentDetailService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ShipmentDetailController(IShipmentDetailService shipmentDetailService, IMapper mapper, IUserService userService)
        {
            _shipmentDetailService = shipmentDetailService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        [Route("list-shipment-details")]
        public async Task<IActionResult> GetShipmentDetails(int userId)
        {
            try
            {
                var query = await _shipmentDetailService.GetAllShipmentDetails(userId);
                var shipments = query.Select(_ => _mapper.Map<ShipmentDetail, ShipmentDetailVM>(_)).ToList();
                if (shipments.Count > 0)
                {
                return Ok(shipments);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("add-shipment-details")]
        public async Task<IActionResult> AddShipments(List<ShipmentDetail> newShipments)
        {
            try
            {
                await _shipmentDetailService.AddShipmentDetails(newShipments);
                return Ok("Add new Shipment Details successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("update-shipment-detail")]
        public async Task<IActionResult> UpdateShipment(ShipmentDetailUM shipmentDetailUM)
        {
            try
            {

                var updatedShipment = _mapper.Map<ShipmentDetailUM, ShipmentDetail>(shipmentDetailUM);
                await _shipmentDetailService.UpdateShipmentDetail(updatedShipment);
                return Ok(updatedShipment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("delete-shipment-detail")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            try
            {
                await _shipmentDetailService.DeleteShipmentDetailById(id);
                return Ok("Removed successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
