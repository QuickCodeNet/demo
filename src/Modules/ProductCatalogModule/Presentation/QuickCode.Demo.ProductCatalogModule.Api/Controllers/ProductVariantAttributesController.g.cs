using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.ProductVariantAttribute;
using QuickCode.Demo.ProductCatalogModule.Application.Services.ProductVariantAttribute;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Api.Controllers
{
    public partial class ProductVariantAttributesController : QuickCodeBaseApiController
    {
        private readonly IProductVariantAttributeService service;
        private readonly ILogger<ProductVariantAttributesController> logger;
        private readonly IServiceProvider serviceProvider;
        public ProductVariantAttributesController(IProductVariantAttributeService service, IServiceProvider serviceProvider, ILogger<ProductVariantAttributesController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductVariantAttributeDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "ProductVariantAttribute", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "ProductVariantAttribute") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{variantId:int}/{attributeValueId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductVariantAttributeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int variantId, int attributeValueId)
        {
            var response = await service.GetItemAsync(variantId, attributeValueId);
            if (HandleResponseError(response, logger, "ProductVariantAttribute", $"VariantId: '{variantId}', AttributeValueId: '{attributeValueId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductVariantAttributeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ProductVariantAttributeDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "ProductVariantAttribute") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { variantId = response.Value.VariantId, attributeValueId = response.Value.AttributeValueId }, response.Value);
        }

        [HttpPut("{variantId:int}/{attributeValueId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int variantId, int attributeValueId, ProductVariantAttributeDto model)
        {
            if (!(model.VariantId == variantId && model.AttributeValueId == attributeValueId))
            {
                return BadRequest($"VariantId: '{variantId}', AttributeValueId: '{attributeValueId}' must be equal to model.VariantId: '{model.VariantId}', model.AttributeValueId: '{model.AttributeValueId}'");
            }

            var response = await service.UpdateAsync(variantId, attributeValueId, model);
            if (HandleResponseError(response, logger, "ProductVariantAttribute", $"VariantId: '{variantId}', AttributeValueId: '{attributeValueId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{variantId:int}/{attributeValueId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int variantId, int attributeValueId)
        {
            var response = await service.DeleteItemAsync(variantId, attributeValueId);
            if (HandleResponseError(response, logger, "ProductVariantAttribute", $"VariantId: '{variantId}', AttributeValueId: '{attributeValueId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-variant-id/{productVariantAttributeVariantId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetByVariantIdResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetByVariantIdAsync(int productVariantAttributeVariantId)
        {
            var response = await service.GetByVariantIdAsync(productVariantAttributeVariantId);
            if (HandleResponseError(response, logger, "ProductVariantAttribute", $"ProductVariantAttributeVariantId: '{productVariantAttributeVariantId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("remove-attribute/{productVariantAttributeVariantId:int}/{productVariantAttributeAttributeValueId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> RemoveAttributeAsync(int productVariantAttributeVariantId, int productVariantAttributeAttributeValueId)
        {
            var response = await service.RemoveAttributeAsync(productVariantAttributeVariantId, productVariantAttributeAttributeValueId);
            if (HandleResponseError(response, logger, "ProductVariantAttribute", $"ProductVariantAttributeVariantId: '{productVariantAttributeVariantId}', ProductVariantAttributeAttributeValueId: '{productVariantAttributeAttributeValueId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}