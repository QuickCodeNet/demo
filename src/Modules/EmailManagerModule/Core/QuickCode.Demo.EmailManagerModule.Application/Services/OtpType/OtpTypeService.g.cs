using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.OtpType;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.OtpType
{
    public partial class OtpTypeService : IOtpTypeService
    {
        private readonly ILogger<OtpTypeService> _logger;
        private readonly IOtpTypeRepository _repository;
        public OtpTypeService(ILogger<OtpTypeService> logger, IOtpTypeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<OtpTypeDto>> InsertAsync(OtpTypeDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(OtpTypeDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, OtpTypeDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<OtpTypeDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<OtpTypeDto>> GetItemAsync(int id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int id)
        {
            var deleteItem = await _repository.GetByPkAsync(id);
            if (deleteItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.DeleteAsync(deleteItem.Value);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> TotalItemCountAsync()
        {
            var returnValue = await _repository.CountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetOtpMessagesForOtpTypesResponseDto>>> GetOtpMessagesForOtpTypesAsync(int otpTypesId)
        {
            var returnValue = await _repository.GetOtpMessagesForOtpTypesAsync(otpTypesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetOtpMessagesForOtpTypesResponseDto>> GetOtpMessagesForOtpTypesDetailsAsync(int otpTypesId, int otpMessagesId)
        {
            var returnValue = await _repository.GetOtpMessagesForOtpTypesDetailsAsync(otpTypesId, otpMessagesId);
            return returnValue.ToResponse();
        }
    }
}