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
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, 
                                IBrandService brandService, 
                                ICategoryService categoryService,
                                IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _brandService = brandService;
            _categoryService = categoryService;
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
                    var category = await _categoryService.GetCategoryById(product.CategoryId);
                    product.Category = category;
                }
                var result = products.Select(_ => _mapper.Map<Product, ProductVM>(_)).ToList();
                return Ok(new { total = result.Count, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("filter-sort-products")]
        public async Task<IActionResult> FilterSortProducts([FromQuery]DefaultSearch defaultSearch, int sportId, int brandId, int categoryId)
        {
            try
            {
                var query = await _productService.GetProducts(_ => _.Status == true, "", defaultSearch.currentPage, defaultSearch.perPage);
                if (sportId != 0 || brandId != 0 || categoryId != 0)
                {
                    query = await _productService.GetProducts(_ => _.CategoryId == categoryId || _.SportId == sportId || _.BrandId == brandId,
                                                        "", defaultSearch.currentPage, defaultSearch.perPage);
                }
                
                var result = query.Sort(defaultSearch.sortBy, defaultSearch.isAscending)
                                  .Select(_ => _mapper.Map<Product, ProductVM>(_))
                                  .ToList();
                return Ok(new { total = result.Count, data = result });
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //[HttpGet]
        //[Route("sort-products-by-price")]
        //public async Task<IActionResult> SortProductsByPrice([FromQuery] DefaultSearch defaultSearch)
        //{
        //    try
        //    {
        //        var query = await _productService.GetProducts(_ => _.Status == true, null, "", defaultSearch.currentPage, defaultSearch.perPage);
        //        var products = query.ToList();
        //        foreach (var product in products)
        //        {
        //            var brand = await _brandService.GetBrandById(product.BrandId);
        //            product.Brand = brand.FirstOrDefault();
        //            var category = await _categoryService.GetCategoryById(product.CategoryId);
        //            product.Category = category;
        //        }
        //        var result = products.Select(_ => _mapper.Map<Product, ProductVM>(_)).ToList();
        //        return Ok(new { total = result.Count, data = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpGet]
        //[Route("filter-products-by-category/{categoryId}")]
        //public async Task<IActionResult> FilterProductsByCategory([FromQuery] DefaultSearch defaultSearch, int categoryId)
        //{
        //    try
        //    {
        //        var query = await _productService.GetProducts(_ => _.Status == true && _.CategoryId == categoryId, null, "", 
        //                                        defaultSearch.currentPage, defaultSearch.perPage);
        //        var products = query.ToList();
        //        foreach (var product in products)
        //        {
        //            var brand = await _brandService.GetBrandById(product.BrandId);
        //            product.Brand = brand.FirstOrDefault();
        //            var category = await _categoryService.GetCategoryById(product.CategoryId);
        //            product.Category = category;
        //        }
        //        var result = products.Select(_ => _mapper.Map<Product, ProductVM>(_)).ToList();
        //        return Ok(new { total = result.Count, data = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPut]
        [Route("update-product/{productId}")]
        public async Task<IActionResult> UpdateProduct([FromQuery] int productId, ProductUM productUM)
        {
            try
            {
                var updatedProduct = await _productService.GetProductById(productId);
                if (updatedProduct != null)
                {
                    updatedProduct.ProductName = productUM.ProductName;
                    updatedProduct.ListedPrice = productUM.ListedPrice;
                    updatedProduct.Price = productUM.Price;
                    updatedProduct.Size = productUM.Size;
                    updatedProduct.Description = productUM.Description;
                    updatedProduct.Status = productUM.Status;
                    updatedProduct.Color = productUM.Color;
                    updatedProduct.Offers = productUM.Offers;
                    updatedProduct.MainImageName = productUM.MainImageName;
                    updatedProduct.MainImagePath = productUM.MainImagePath;
                    updatedProduct.CategoryId = productUM.CategoryId;
                    updatedProduct.BrandId = productUM.BrandId;
                    updatedProduct.SportId = productUM.SportId;
                    await _productService.UpdateProduct(updatedProduct);
                    return Ok(updatedProduct);
                } else
                {
                    return BadRequest("Update failed!");
                }
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
