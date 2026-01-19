using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.ProductGroup;
using QuickCode.Demo.OnlineShopModule.Application.Services.ProductGroup;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Api.Controllers
{
    public partial class ProductGroupsController : QuickCodeBaseApiController
    {
        private readonly IProductGroupService service;
        private readonly ILogger<ProductGroupsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ProductGroupsController(IProductGroupService service, IServiceProvider serviceProvider, ILogger<ProductGroupsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductGroupDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "ProductGroup", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "ProductGroup") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductGroupDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "ProductGroup", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductGroupDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ProductGroupDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "ProductGroup") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ProductGroupDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "ProductGroup", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "ProductGroup", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-groups-by-type/{productGroupsProductTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetGroupsByTypeResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetGroupsByTypeAsync(int productGroupsProductTypeId)
        {
            var response = await service.GetGroupsByTypeAsync(productGroupsProductTypeId);
            if (HandleResponseError(response, logger, "ProductGroup", $"ProductGroupsProductTypeId: '{productGroupsProductTypeId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-product-groups-by-type/{productGroupsProductTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetProductGroupsByTypeResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductGroupsByTypeAsync(int productGroupsProductTypeId)
        {
            var response = await service.GetProductGroupsByTypeAsync(productGroupsProductTypeId);
            if (HandleResponseError(response, logger, "ProductGroup", $"ProductGroupsProductTypeId: '{productGroupsProductTypeId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productGroupId}/product")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetProductsForProductGroupsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductsForProductGroupsAsync(int productGroupsId)
        {
            var response = await service.GetProductsForProductGroupsAsync(productGroupsId);
            if (HandleResponseError(response, logger, "ProductGroup", $"ProductGroupsId: '{productGroupsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{productGroupId}/product/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductsForProductGroupsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetProductsForProductGroupsDetailsAsync(int productGroupsId, int productsId)
        {
            var response = await service.GetProductsForProductGroupsDetailsAsync(productGroupsId, productsId);
            if (HandleResponseError(response, logger, "ProductGroup", $"ProductGroupsId: '{productGroupsId}', ProductsId: '{productsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}