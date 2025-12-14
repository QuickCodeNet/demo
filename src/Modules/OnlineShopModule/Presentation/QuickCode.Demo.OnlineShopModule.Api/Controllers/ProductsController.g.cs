using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Product;
using QuickCode.Demo.OnlineShopModule.Application.Services.Product;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Api.Controllers
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

        [HttpGet("search-products/{productsProductGroupId:int}/{productsTitle}/{productsPrice:decimal}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchProductsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SearchProductsAsync(int productsProductGroupId, string productsTitle, decimal productsPrice)
        {
            var response = await service.SearchProductsAsync(productsProductGroupId, productsTitle, productsPrice);
            if (HandleResponseError(response, logger, "Product", $"ProductsProductGroupId: '{productsProductGroupId}', ProductsTitle: '{productsTitle}', ProductsPrice: '{productsPrice}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("list-low-stock")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ListLowStockResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListLowStockAsync()
        {
            var response = await service.ListLowStockAsync();
            if (HandleResponseError(response, logger, "Product", $"") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productId}/product-image")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetProductImagesForProductsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductImagesForProductsAsync(int productsId)
        {
            var response = await service.GetProductImagesForProductsAsync(productsId);
            if (HandleResponseError(response, logger, "Product", $"ProductsId: '{productsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productId}/product-image/{productImageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductImagesForProductsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductImagesForProductsDetailsAsync(int productsId, int productImagesId)
        {
            var response = await service.GetProductImagesForProductsDetailsAsync(productsId, productImagesId);
            if (HandleResponseError(response, logger, "Product", $"ProductsId: '{productsId}', ProductImagesId: '{productImagesId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productId}/product-review")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetProductReviewsForProductsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductReviewsForProductsAsync(int productsId)
        {
            var response = await service.GetProductReviewsForProductsAsync(productsId);
            if (HandleResponseError(response, logger, "Product", $"ProductsId: '{productsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productId}/product-review/{productReviewId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductReviewsForProductsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductReviewsForProductsDetailsAsync(int productsId, int productReviewsId)
        {
            var response = await service.GetProductReviewsForProductsDetailsAsync(productsId, productReviewsId);
            if (HandleResponseError(response, logger, "Product", $"ProductsId: '{productsId}', ProductReviewsId: '{productReviewsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productId}/cart-item")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetCartItemsForProductsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCartItemsForProductsAsync(int productsId)
        {
            var response = await service.GetCartItemsForProductsAsync(productsId);
            if (HandleResponseError(response, logger, "Product", $"ProductsId: '{productsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productId}/cart-item/{cartItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCartItemsForProductsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetCartItemsForProductsDetailsAsync(int productsId, int cartItemsId)
        {
            var response = await service.GetCartItemsForProductsDetailsAsync(productsId, cartItemsId);
            if (HandleResponseError(response, logger, "Product", $"ProductsId: '{productsId}', CartItemsId: '{cartItemsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productId}/order-item")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetOrderItemsForProductsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOrderItemsForProductsAsync(int productsId)
        {
            var response = await service.GetOrderItemsForProductsAsync(productsId);
            if (HandleResponseError(response, logger, "Product", $"ProductsId: '{productsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productId}/order-item/{orderItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderItemsForProductsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetOrderItemsForProductsDetailsAsync(int productsId, int orderItemsId)
        {
            var response = await service.GetOrderItemsForProductsDetailsAsync(productsId, orderItemsId);
            if (HandleResponseError(response, logger, "Product", $"ProductsId: '{productsId}', OrderItemsId: '{orderItemsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPatch("reduce-stock/{productsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ReduceStockAsync(int productsId, [FromBody] ReduceStockRequestDto updateRequest)
        {
            var response = await service.ReduceStockAsync(productsId, updateRequest);
            if (HandleResponseError(response, logger, "Product", $"ProductsId: '{productsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}