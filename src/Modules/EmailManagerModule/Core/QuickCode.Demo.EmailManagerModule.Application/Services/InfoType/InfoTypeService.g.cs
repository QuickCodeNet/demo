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
    public partial class InfoTypeService : IInfoTypeService
    {
        private readonly ILogger<InfoTypeService> _logger;
        private readonly IInfoTypeRepository _repository;
        public InfoTypeService(ILogger<InfoTypeService> logger, IInfoTypeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<InfoTypeDto>> InsertAsync(InfoTypeDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(InfoTypeDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, InfoTypeDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<InfoTypeDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<InfoTypeDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetInfoMessagesForInfoTypesResponseDto>>> GetInfoMessagesForInfoTypesAsync(int infoTypesId)
        {
            var returnValue = await _repository.GetInfoMessagesForInfoTypesAsync(infoTypesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetInfoMessagesForInfoTypesResponseDto>> GetInfoMessagesForInfoTypesDetailsAsync(int infoTypesId, int infoMessagesId)
        {
            var returnValue = await _repository.GetInfoMessagesForInfoTypesDetailsAsync(infoTypesId, infoMessagesId);
            return returnValue.ToResponse();
        }
    }
}