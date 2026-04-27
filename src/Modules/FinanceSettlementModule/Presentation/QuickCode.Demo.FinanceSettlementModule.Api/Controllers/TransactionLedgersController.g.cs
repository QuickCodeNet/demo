using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.FinanceSettlementModule.Application.Dtos.TransactionLedger;
using QuickCode.Demo.FinanceSettlementModule.Application.Services.TransactionLedger;
using QuickCode.Demo.FinanceSettlementModule.Domain.Enums;

namespace QuickCode.Demo.FinanceSettlementModule.Api.Controllers
{
    public partial class TransactionLedgersController : QuickCodeBaseApiController
    {
        private readonly ITransactionLedgerService service;
        private readonly ILogger<TransactionLedgersController> logger;
        private readonly IServiceProvider serviceProvider;
        public TransactionLedgersController(ITransactionLedgerService service, IServiceProvider serviceProvider, ILogger<TransactionLedgersController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionLedgerDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "TransactionLedger", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "TransactionLedger") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionLedgerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "TransactionLedger", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TransactionLedgerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(TransactionLedgerDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "TransactionLedger") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, TransactionLedgerDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "TransactionLedger", $"Id: '{id}'") is {} responseError)
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
            if (HandleResponseError(response, logger, "TransactionLedger", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-by-seller-id/{transactionLedgerSellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetBySellerIdResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetBySellerIdAsync(int transactionLedgerSellerId, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetBySellerIdAsync(transactionLedgerSellerId, page, size);
            if (HandleResponseError(response, logger, "TransactionLedger", $"TransactionLedgerSellerId: '{transactionLedgerSellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-seller-balance/{transactionLedgerSellerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSellerBalanceResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSellerBalanceAsync(int transactionLedgerSellerId)
        {
            var response = await service.GetSellerBalanceAsync(transactionLedgerSellerId);
            if (HandleResponseError(response, logger, "TransactionLedger", $"TransactionLedgerSellerId: '{transactionLedgerSellerId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-transactions-by-type-and-date/{transactionLedgerSellerId:int}/{transactionLedgerTransactionType}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetTransactionsByTypeAndDateResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetTransactionsByTypeAndDateAsync(int transactionLedgerSellerId, TransactionType transactionLedgerTransactionType, DateTime transactionLedgerTransactionDateFrom, DateTime transactionLedgerTransactionDateTo, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetTransactionsByTypeAndDateAsync(transactionLedgerSellerId, transactionLedgerTransactionType, transactionLedgerTransactionDateFrom, transactionLedgerTransactionDateTo, page, size);
            if (HandleResponseError(response, logger, "TransactionLedger", $"TransactionLedgerSellerId: '{transactionLedgerSellerId}', TransactionLedgerTransactionType: '{transactionLedgerTransactionType}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}