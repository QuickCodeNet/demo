using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.ProductCategory;
using QuickCode.Demo.ProductCatalogModule.Application.Services.ProductCategory;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Api.Controllers
{
    public partial class ProductCategoriesController : QuickCodeBaseApiController
    {
        private readonly IProductCategoryService service;
        private readonly ILogger<ProductCategoriesController> logger;
        private readonly IServiceProvider serviceProvider;
        public ProductCategoriesController(IProductCategoryService service, IServiceProvider serviceProvider, ILogger<ProductCategoriesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductCategoryDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "ProductCategory", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "ProductCategory") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productId:int}/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductCategoryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int productId, int categoryId)
        {
            var response = await service.GetItemAsync(productId, categoryId);
            if (HandleResponseError(response, logger, "ProductCategory", $"ProductId: '{productId}', CategoryId: '{categoryId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductCategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ProductCategoryDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "ProductCategory") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { productId = response.Value.ProductId, categoryId = response.Value.CategoryId }, response.Value);
        }

        [HttpPut("{productId:int}/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int productId, int categoryId, ProductCategoryDto model)
        {
            if (!(model.ProductId == productId && model.CategoryId == categoryId))
            {
                return BadRequest($"ProductId: '{productId}', CategoryId: '{categoryId}' must be equal to model.ProductId: '{model.ProductId}', model.CategoryId: '{model.CategoryId}'");
            }

            var response = await service.UpdateAsync(productId, categoryId, model);
            if (HandleResponseError(response, logger, "ProductCategory", $"ProductId: '{productId}', CategoryId: '{categoryId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{productId:int}/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int productId, int categoryId)
        {
            var response = await service.DeleteItemAsync(productId, categoryId);
            if (HandleResponseError(response, logger, "ProductCategory", $"ProductId: '{productId}', CategoryId: '{categoryId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-product-id/{productCategoryProductId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByProductIdResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByProductIdAsync(int productCategoryProductId)
        {
            var response = await service.GetByProductIdAsync(productCategoryProductId);
            if (HandleResponseError(response, logger, "ProductCategory", $"ProductCategoryProductId: '{productCategoryProductId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-category-id/{productCategoryCategoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByCategoryIdResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByCategoryIdAsync(int productCategoryCategoryId, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetByCategoryIdAsync(productCategoryCategoryId, page, size);
            if (HandleResponseError(response, logger, "ProductCategory", $"ProductCategoryCategoryId: '{productCategoryCategoryId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("remove-from-category/{productCategoryProductId:int}/{productCategoryCategoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> RemoveFromCategoryAsync(int productCategoryProductId, int productCategoryCategoryId)
        {
            var response = await service.RemoveFromCategoryAsync(productCategoryProductId, productCategoryCategoryId);
            if (HandleResponseError(response, logger, "ProductCategory", $"ProductCategoryProductId: '{productCategoryProductId}', ProductCategoryCategoryId: '{productCategoryCategoryId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}