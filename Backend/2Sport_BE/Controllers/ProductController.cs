using _2Sport_BE.Helpers;
using _2Sport_BE.Infrastructure.Services;
using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using _2Sport_BE.Service.Services;
using _2Sport_BE.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2Sport_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IBrandService brandService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _brandService = brandService;
        }

        [HttpGet]
        [Route("list-products")]
        public async Task<IActionResult> GetProducts([FromQuery] DefaultSearch defaultSearch)
        {
            try
            {
                var query = await _productService.GetProducts(_ => _.Status == true, null, "", defaultSearch.currentPage, defaultSearch.perPage);
                var products = query.ToList();
                foreach(var product in products)
                {
                    var brand = await _brandService.GetBrandById(product.BrandId);
                    product.Brand = brand.FirstOrDefault();
                }
                var result = products.Select(_ => _mapper.Map<Product, ProductVM>(_)).ToList();
                //var pr = await query.Select(_ => _mapper.Map<Category, CategoryVM>(_)).ToListAsync();
                return Ok(new { total = result.Count, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
