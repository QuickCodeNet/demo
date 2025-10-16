using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos.InfoType;

namespace QuickCode.Demo.EmailManagerModule.Application.Services.InfoType
{
    public partial interface IInfoTypeService
    {
        Task<Response<InfoTypeDto>> InsertAsync(InfoTypeDto request);
        Task<Response<bool>> DeleteAsync(InfoTypeDto request);
        Task<Response<bool>> UpdateAsync(int id, InfoTypeDto request);
        Task<Response<List<InfoTypeDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<InfoTypeDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetInfoMessagesForInfoTypesResponseDto>>> GetInfoMessagesForInfoTypesAsync(int infoTypesId);
        Task<Response<GetInfoMessagesForInfoTypesResponseDto>> GetInfoMessagesForInfoTypesDetailsAsync(int infoTypesId, int infoMessagesId);
    }
}