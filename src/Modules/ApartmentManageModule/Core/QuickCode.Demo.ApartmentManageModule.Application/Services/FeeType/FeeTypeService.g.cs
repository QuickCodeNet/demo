using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.FeeType;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Services.FeeType
{
    public partial class FeeTypeService : IFeeTypeService
    {
        private readonly ILogger<FeeTypeService> _logger;
        private readonly IFeeTypeRepository _repository;
        public FeeTypeService(ILogger<FeeTypeService> logger, IFeeTypeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<FeeTypeDto>> InsertAsync(FeeTypeDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(FeeTypeDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, FeeTypeDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<FeeTypeDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<FeeTypeDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetActiveFeeTypesResponseDto>>> GetActiveFeeTypesAsync(bool feeTypesIsActive)
        {
            var returnValue = await _repository.GetActiveFeeTypesAsync(feeTypesIsActive);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetFlatPaymentsForFeeTypesResponseDto>>> GetFlatPaymentsForFeeTypesAsync(int feeTypesId)
        {
            var returnValue = await _repository.GetFlatPaymentsForFeeTypesAsync(feeTypesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetFlatPaymentsForFeeTypesResponseDto>> GetFlatPaymentsForFeeTypesDetailsAsync(int feeTypesId, int flatPaymentsId)
        {
            var returnValue = await _repository.GetFlatPaymentsForFeeTypesDetailsAsync(feeTypesId, flatPaymentsId);
            return returnValue.ToResponse();
        }
    }
}