using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.Product;
using QuickCode.Demo.ProductCatalogModule.Application.Services.Product;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Api.Controllers
{
    public partial class ProductsController : QuickCodeBaseApiController
    {
        private readonly IProductService service;
        private readonly ILogger<ProductsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ProductsController(IProductService service, IServiceProvider serviceProvider, ILogger<ProductsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Product", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Product") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Product", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ProductDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Product") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ProductDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Product", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await service.DeleteItemAsync(id);
            if (HandleResponseError(response, logger, "Product", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-sku/{productSku}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBySkuResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetBySkuAsync(string productSku)
        {
            var response = await service.GetBySkuAsync(productSku);
            if (HandleResponseError(response, logger, "Product", $"ProductSku: '{productSku}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-by-seller/{productSellerId:int}/{productStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetActiveBySellerResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveBySellerAsync(int productSellerId, ProductStatus productStatus, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetActiveBySellerAsync(productSellerId, productStatus, page, size);
            if (HandleResponseError(response, logger, "Product", $"ProductSellerId: '{productSellerId}', ProductStatus: '{productStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("search-products/{productName}/{productStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchProductsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SearchProductsAsync(string productName, ProductStatus productStatus, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.SearchProductsAsync(productName, productStatus, page, size);
            if (HandleResponseError(response, logger, "Product", $"ProductName: '{productName}', ProductStatus: '{productStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-pending-approval/{productStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetPendingApprovalResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetPendingApprovalAsync(ProductStatus productStatus, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetPendingApprovalAsync(productStatus, page, size);
            if (HandleResponseError(response, logger, "Product", $"ProductStatus: '{productStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-featured/{productStatus}/{productIsFeatured:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFeaturedResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFeaturedAsync(ProductStatus productStatus, bool productIsFeatured, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetFeaturedAsync(productStatus, productIsFeatured, page, size);
            if (HandleResponseError(response, logger, "Product", $"ProductStatus: '{productStatus}', ProductIsFeatured: '{productIsFeatured}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-products-with-details/{productsBrandId:int}/{productPrimaryCategoryId:int}/{categoryId:int}/{brandId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetProductsWithDetailsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductsWithDetailsAsync(int productsBrandId, int productPrimaryCategoryId, int categoryId, int brandId, int productsPrimaryCategoryId, int productBrandId, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetProductsWithDetailsAsync(productsBrandId, productPrimaryCategoryId, categoryId, brandId, productsPrimaryCategoryId, productBrandId, page, size);
            if (HandleResponseError(response, logger, "Product", $"ProductsBrandId: '{productsBrandId}', ProductPrimaryCategoryId: '{productPrimaryCategoryId}', CategoryId: '{categoryId}', BrandId: '{brandId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-count-by-status/{productStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCountByStatusAsync(ProductStatus productStatus)
        {
            var response = await service.GetCountByStatusAsync(productStatus);
            if (HandleResponseError(response, logger, "Product", $"ProductStatus: '{productStatus}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("approve/{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ApproveAsync(int productId, [FromBody] ApproveRequestDto updateRequest)
        {
            var response = await service.ApproveAsync(productId, updateRequest);
            if (HandleResponseError(response, logger, "Product", $"ProductId: '{productId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("reject/{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> RejectAsync(int productId, [FromBody] RejectRequestDto updateRequest)
        {
            var response = await service.RejectAsync(productId, updateRequest);
            if (HandleResponseError(response, logger, "Product", $"ProductId: '{productId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("set-featured/{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SetFeaturedAsync(int productId, [FromBody] SetFeaturedRequestDto updateRequest)
        {
            var response = await service.SetFeaturedAsync(productId, updateRequest);
            if (HandleResponseError(response, logger, "Product", $"ProductId: '{productId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}