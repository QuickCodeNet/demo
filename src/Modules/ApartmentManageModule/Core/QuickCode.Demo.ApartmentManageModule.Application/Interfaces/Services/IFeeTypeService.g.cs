using System;
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
    public partial interface IFeeTypeService
    {
        Task<Response<FeeTypeDto>> InsertAsync(FeeTypeDto request);
        Task<Response<bool>> DeleteAsync(FeeTypeDto request);
        Task<Response<bool>> UpdateAsync(int id, FeeTypeDto request);
        Task<Response<List<FeeTypeDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<FeeTypeDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetActiveFeeTypesResponseDto>>> GetActiveFeeTypesAsync(bool feeTypesIsActive);
        Task<Response<List<GetFlatPaymentsForFeeTypesResponseDto>>> GetFlatPaymentsForFeeTypesAsync(int feeTypesId);
        Task<Response<GetFlatPaymentsForFeeTypesResponseDto>> GetFlatPaymentsForFeeTypesDetailsAsync(int feeTypesId, int flatPaymentsId);
    }
}