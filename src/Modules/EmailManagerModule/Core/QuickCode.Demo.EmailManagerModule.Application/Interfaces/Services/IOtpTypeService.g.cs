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
    public partial interface IOtpTypeService
    {
        Task<Response<OtpTypeDto>> InsertAsync(OtpTypeDto request);
        Task<Response<bool>> DeleteAsync(OtpTypeDto request);
        Task<Response<bool>> UpdateAsync(int id, OtpTypeDto request);
        Task<Response<List<OtpTypeDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<OtpTypeDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetOtpMessagesForOtpTypesResponseDto>>> GetOtpMessagesForOtpTypesAsync(int otpTypesId);
        Task<Response<GetOtpMessagesForOtpTypesResponseDto>> GetOtpMessagesForOtpTypesDetailsAsync(int otpTypesId, int otpMessagesId);
    }
}